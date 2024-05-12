using AutoMapper;
using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepository _bookRepository;
        private IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById(string bookId)
        {
            var book = await _bookRepository.GetBookById(bookId);
            var mappedBook = _mapper.Map<BookDto>(book);
            return Ok(mappedBook);
        }
    }
}
