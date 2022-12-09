using LetrasBlog.Client.Repositories;
using LetrasBlog.Infraestructure.Data;
using LetrasBlog.Infraestructure.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetrasBlog.Infraestructure.Services
{
    public class RepositoryServices : IRepository
    {
        private readonly SqlConfigurationContextDev _configuration;
        protected readonly ILogger<string> _logger;
        private IRepository _repository;
        public string TAG_API = "ApiLetrasBlog";
        public string TAG_VERB = "GET";
        public IConfiguration Configuration { get; }

        public RepositoryServices(SqlConfigurationContextDev configuration, [NotNull] ILogger<string> logger,IConfiguration _Configuration)
        {
            _logger = logger;
            
            _configuration = configuration;
            Configuration=_Configuration;

            _repository = new Repository(_configuration.ConnectionString);
        }

        public Task<Articles.response> GetArticles()
        {
            _logger.LogInformation("Request to endpoint {endpoint} {verb}", "/api/" + TAG_API + "", TAG_VERB);
            var response = _repository.GetArticles();
            if(response.Result == null)
            {
                _logger.LogError("Error Request from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB +
                    " - " + "[ No data available ]");
            }
            _logger.LogInformation("Response from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB  + " - OK");

            return response;
        }
        /*
        public Task<Books.response> InsertBooks()
        {
            _logger.LogInformation("Request to endpoint {endpoint} {verb}", "/api/" + TAG_API + "", TAG_VERB);
            var response = _repository.InsertBooks();
            if (response.Result == null)
            {
                _logger.LogError("Error Request from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB +
                    " - " + "[ No data available ]");
            }
            _logger.LogInformation("Response from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB  + " - OK");

            return response;
        }*/
        public Task<Books.response> InsertRespuesta()
        {
            _logger.LogInformation("Request to endpoint {endpoint} {verb}", "/api/" + TAG_API + "", TAG_VERB);
            var response = _repository.InsertRespuesta();
            if (response.Result == null)
            {
                _logger.LogError("Error Request from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB +
                    " - " + "[ No data available ]");
            }
            _logger.LogInformation("Response from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB  + " - OK");

            return response;
        }
        public Task<Books.response> GetBooks()
        {
            _logger.LogInformation("Request to endpoint {endpoint} {verb}", "/api/" + TAG_API + "", TAG_VERB);
            var response = _repository.GetBooks();
            if (response.Result == null)
            {
                _logger.LogError("Error Request from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB +
                    " - " + "[ No data available ]");
            }
            _logger.LogInformation("Response from endpoint {endpoint} {verb}", "/api/" + TAG_API + "", " - " + TAG_VERB  + " - OK");

            return response;
        }
    }
}
