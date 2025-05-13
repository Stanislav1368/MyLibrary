using RentService.Domain.Entities;
using RentService.Domain.Interfaces;
using System.Text.Json;
using MediatR;
using System.Text.Json.Serialization;
using MassTransit;
using SharedContracts;
using RentService.Application.Common.Exceptions;

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
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateRentalCommandHandler(
            IRentalRepository rentalRepository,
            IStatusRepository statusRepository,
            ILibrarianRepository librarianRepository,
            IRenterRepository renterRepository,
            HttpClient httpClient,
            IPublishEndpoint publishEndpoint) 
        {
            _rentalRepository = rentalRepository;
            _renterRepository = renterRepository;
            _librarianRepository = librarianRepository;
            _statusRepository = statusRepository;
            _httpClient = httpClient;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var bookId = await GetBookIdFromExternalService(request.ExternalBookId);

            if (await _rentalRepository.IsBookRentedAsync(bookId))
            {
                throw new ConflictException("Книга уже арендована");
            }

            var renter = await _renterRepository.GetByIdAsync(request.RenterId);
            if (renter == null)
            {
                throw new NotFoundException("Renter", request.RenterId);
            }

            var librarian = await _librarianRepository.GetByIdAsync(request.LibrarianId);
            if (librarian == null)
            {
                throw new NotFoundException("Librarian", request.LibrarianId);
            }

            var status = await _statusRepository.GetByIdAsync((int)StatusType.Active);
            if (status == null)
            {
                throw new NotFoundException("Status", (int)StatusType.Active);
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

        
            await _publishEndpoint.Publish(
                new BookRentedEvent(bookId, request.StartDate, request.EndDate),
                cancellationToken
            );

            return rental;
        }
        /// <summary>
        /// Получает внутренний идентификатор книги из внешнего сервиса по внешнему идентификатору.
        /// </summary>
        /// <param name="externalBookId">Внешний идентификатор книги.</param>
        /// <returns>Внутренний идентификатор книги.</returns>
        private async Task<int> GetBookIdFromExternalService(int externalBookId)
        {
            if (externalBookId <= 0)
            {
                throw new BadRequestException("Неверный ID книги. ID не может быть <= 0");
            }

            var response = await _httpClient.GetAsync($"https://localhost:7055/api/books/{externalBookId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var book = JsonSerializer.Deserialize<BookResponse>(content);

            return book?.Id ?? throw new NotFoundException("Book", externalBookId);
        }
        /// <summary>
        /// DTO для десериализации ответа от внешнего сервиса книг.
        /// </summary>
        /// <param name="Id">Внутренний идентификатор книги во внешней системе.</param>
        private record BookResponse(/// <summary>
                                    /// Уникальный идентификатор книги во внешней системе.
                                    /// </summary>
                                    [property: JsonPropertyName("id")] int Id);
    }

}
