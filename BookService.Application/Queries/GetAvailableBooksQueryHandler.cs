using BookService.Domain.Entities;
using BookService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookService.Application.Queries
{
   public record GetAvailableBooksQuery : IRequest<IEnumerable<Book>>;
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly HttpClient _httpClient;

        public GetAvailableBooksQueryHandler(IBookRepository bookRepository, HttpClient httpClient)
        {
            _bookRepository = bookRepository;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> Handle(GetAvailableBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();

            var availableBooks = new List<Book>();
            foreach (var book in books)
            {
                var response = await _httpClient.GetAsync($"https://localhost:7068/api/rentals/_bookId={book.Id}");
                response.EnsureSuccessStatusCode();
                var isAvailable = JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());

                if (isAvailable)
                {
                    availableBooks.Add(book);
                }
            }

            return availableBooks;
        }
    }
   
}
