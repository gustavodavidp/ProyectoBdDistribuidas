using LetrasBlog.Client.Repositories;
using LetrasBlog.Infraestructure.Entities;
using LetrasBlog.Infraestructure;
using LetrasBlog.Infraestructure.Interfaces;
using LetrasBlog.Server.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LetrasBlog.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IRepository _RepositoryValidation;
        private readonly SqlConfigurationContext _dbContext;
        protected readonly ILogger<ArticlesController> _logger;

        public BooksController(IRepository Repository, [NotNull] ILogger<ArticlesController> logger)
        {
            _RepositoryValidation = Repository;
            _logger=logger;

        }

        [Route("InsertRespuesta")]
        [HttpGet]
        public async Task<IActionResult> InsertRespuesta()
        {
            var post = await _RepositoryValidation.InsertRespuesta();

            if (post == null)
            {
                throw new BusinessExceptions("Error en obtención");
            }
            return Ok(post);
        }

        [Route("GetBooks")]
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var post = await _RepositoryValidation.GetBooks();

            if (post == null)
            {
                throw new BusinessExceptions("Error en obtención");
            }
            return Ok(post);
        }

        [Route("DeleteBooks/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            var post = await _RepositoryValidation.DeleteBooks(id);

            if (post == null)
            {
                throw new BusinessExceptions("Error en eliminación");
            }
            return Ok(post);
        }

        [Route("UpdateBooks")]
        [HttpPost]
        public async Task<IActionResult> UpdateBooks(Books.BooksDetail book)
        {
            var post = await _RepositoryValidation.UpdateBooks(book);

            if (post == null)
            {
                throw new BusinessExceptions("Error en eliminación");
            }
            return Ok(post);
        }
    }
}
