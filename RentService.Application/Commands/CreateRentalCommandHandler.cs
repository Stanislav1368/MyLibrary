using RentService.Domain.Entities;
using RentService.Domain.Interfaces;
using System.Text.Json;
using MediatR;
using System.Text.Json.Serialization;

namespace RentService.Application.Commands
{
    public record CreateRentalCommand(
           int ExternalBookId,
           int RenterId,
           int LibrarianId,
           DateTime StartDate,
           DateTime EndDate) : IRequest<Rental>;

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IRenterRepository _renterRepository;
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly HttpClient _httpClient;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IStatusRepository statusRepository, ILibrarianRepository librarianRepository, IRenterRepository renterRepository, HttpClient httpClient)
        {
            _rentalRepository = rentalRepository;
            _renterRepository = renterRepository;
            _librarianRepository = librarianRepository;
            _statusRepository = statusRepository;
            _httpClient = httpClient;
        }

        public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            // Получаем bookId из внешнего сервиса
            var bookId = await GetBookIdFromExternalService(request.ExternalBookId);

            if (await _rentalRepository.IsBookRentedAsync(bookId))
            {
                throw new Exception("Книга уже арендована");
            }

            var renter = await _renterRepository.GetByIdAsync(request.RenterId);
            if (renter == null)
            {
                throw new Exception("Арендатор с таким Id не найден");
            }


            var librarian = await _librarianRepository.GetByIdAsync(request.LibrarianId);
            if (librarian == null)
            {
                throw new Exception("Библиотекарь с таким Id не найден");
            }


            var status = await _statusRepository.GetByIdAsync(1);
            if (status == null)
            {
                throw new Exception("Статус с таким Id не найден");
            }

            var rental = new Rental
            {
                BookId = bookId,
                RenterId = request.RenterId,
                Renter = renter,
                LibrarianId = request.LibrarianId,
                Librarian = librarian,
                StatusId = status.Id,
                Status = status,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            await _rentalRepository.AddAsync(rental);
            return rental;
        }

        private async Task<int> GetBookIdFromExternalService(int externalBookId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7055/api/books/{externalBookId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var book = JsonSerializer.Deserialize<BookResponse>(content);

            return book?.Id ?? throw new Exception("Книга с таким Id не найдена");
        }

        private record BookResponse([property: JsonPropertyName("id")] int Id);
    }

}
