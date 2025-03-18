using BookService.Domain.Entities;
using BookService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Application.Queries
{
    public record SearchBooksQuery(
     string? Title,
     List<string>? Genres,
     List<string>? Authors,
     int? StartYear,
     int? EndYear, 
     bool? IsAccess,
     int Page,
     int PageSize,
     string SortBy,
     string SortOrder) : IRequest<IEnumerable<Book>>;

    public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public SearchBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.SearchBooksAsync(request.Title, request.Genres, request.Authors, request.StartYear, request.EndYear, request.IsAccess, request.Page, request.PageSize, request.SortBy, request.SortOrder);
        }
    }
}
