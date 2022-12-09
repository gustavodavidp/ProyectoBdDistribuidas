using LetrasBlog.Client.Repositories;
using LetrasBlog.Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetrasBlog.Client.Repositories
{
    public interface IRepository
    {
        Task<Articles.response> GetArticles();
        //Task<Books.response> InsertBooks();
        Task<Books.response> InsertRespuesta();
        Task<Books.response> GetBooks();

    }
}