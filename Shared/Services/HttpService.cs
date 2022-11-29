using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using LetrasBlog.Infraestructure.Entities;
using LetrasBlog.Infraestructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LetrasBlog.Infraestructure.Services
{
    public class HttpServices : IHttpServices
    {
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient;

        public HttpServices(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration=configuration;
            _httpClient=httpClient;
        }
    }
}
