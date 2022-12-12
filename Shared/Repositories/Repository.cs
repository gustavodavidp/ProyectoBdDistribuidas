using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LetrasBlog.Infraestructure.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace LetrasBlog.Client.Repositories
{
    public class Repository : IRepository
    {
        private string ConnectionString;
        public IConfiguration Configuration { get;}
        public string path = null;
        private static readonly HttpClient client = new HttpClient();

        public Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConection()
        {
            return new SqlConnection(ConnectionString);
        }
        public async Task<Articles.response> GetArticles()
        {
            Articles.response ArticlesData = new Articles.response();
            Articles.Data responseDetail = new Articles.Data();

            var db = dbConection();
            var procedure = "[dbo].[GET_ARTICLES]";

            var Data = await db.QueryAsync<Articles.Data>(procedure, commandType: CommandType.StoredProcedure); ;

            ArticlesData = new Articles.response
            {
                Code = 0,
                Message = "Success",
                Detail = Data.Select(c => new Articles.Data()
                {
                    IdArticle = c.IdArticle,
                    IdCategory = c.IdCategory,
                    IdUser = c.IdUser,
                    Title = c.Title,
                    Body = c.Body,
                    Created = c.Created,
                    Updated = c.Updated
                }).ToList()
            };

            return ArticlesData;
        }
        
        public async Task<Books.response> InsertRespuesta()
        {
            Books.response BooksData = new Books.response();
            Books.Data responseDetail = new Books.Data();
            List<Books.BooksDetail> _BooksList = new List<Books.BooksDetail>();
            string response;

            var content = await client.GetAsync("https://api.itbook.store/1.0/search/new");
            //Imprimir en consola
            response = content.Content.ReadAsStringAsync().ToString();
            Console.WriteLine(await content.Content.ReadAsStringAsync());

            //Procesar respuesta
            var responseString = await content.Content.ReadAsStringAsync();
            responseDetail = JsonConvert.DeserializeObject<Books.Data>(responseString);
            

            if(responseDetail != null)
            {
                //Asignación de libros en variable tipo lista
                _BooksList = responseDetail.Books;

                //Procedimiento para Insertar respuesta del servidor
                var db = dbConection();
                var procedure = "[dbo].[INSERT_RESPUESTA_SERVIDOR]";
                var parameters = new
                {
                    error = responseDetail.Error,
                    total_registros = responseDetail.Total
                };

                var Data = await db.QueryAsync<Books.Data>(procedure, parameters, commandType: CommandType.StoredProcedure);

                //Procedimiento para Insertar Datos de libros
                foreach(var libro in responseDetail.Books)
                {
                    var procedure_books = "[dbo].[INSERT_BOOKS]";
                    var price = libro.Price.Split('$');
                    var price2 = Convert.ToDecimal(price[1]);
                    var parameters_books = new
                    {
                        titulo = libro.Title,
                        subtitulo = libro.Subtitle,
                        precio = Convert.ToDecimal(price2.ToString("0.00")),
                        imagen = libro.Image,
                        url = libro.Url,
                        isbn13 = libro.Isbn13
                    };

                    var Data_Books = await db.QueryAsync<Books.Data>(procedure_books, parameters_books, commandType: CommandType.StoredProcedure);
                }
                


                BooksData = new Books.response
                {
                    Code = 0,
                    Message = "Satisfactorio",
                    Detail = Data.Select(c => new Books.Data()
                    {
                        ID_LOG = c.ID_LOG,
                        Error = 0,
                        Total = responseDetail.Total,
                        Message = "Inserción de datos de libros finalizado."
                    }).ToList().Last()
                };
            }
            

            return BooksData;
        }

        public async Task<Books.response> GetBooks()
        {
            Books.response ArticlesData = new Books.response();
            Books.BooksDetail responseDetail = new Books.BooksDetail();

            var db = dbConection();
            var procedure = "[dbo].[GET_BOOKS]";

            var Data = await db.QueryAsync<Books.BooksDetail>(procedure, commandType: CommandType.StoredProcedure); 

            ArticlesData = new Books.response
            {
                Code = 0,
                Message = "Success",
                Books = Data.Select(c => new Books.BooksDetail()
                {
                    ID_LIBRO = c.ID_LIBRO,
                    Title = c.Title,
                    Subtitle = c.Subtitle,
                    Price = c.Price,
                    Image = c.Image,
                    Isbn13 = c.Isbn13,
                    Url = c.Url
                }).ToList()

                
            };

            return ArticlesData;
        }

        public async Task<Books.response> DeleteBooks(int id)
        {
            Books.response ArticlesData = new Books.response();
            Books.BooksDetail responseDetail = new Books.BooksDetail();

            var db = dbConection();
            var procedure = "[dbo].[DELETE_BOOKS]";
            var parameters = new
            {
                ID_LIBRO = id
            };

            var Data = await db.QueryAsync<Books.Data>(procedure, parameters, commandType: CommandType.StoredProcedure);

            ArticlesData = new Books.response
            {
                Code = 0,
                Message = "Success Deleted"


            };

            return ArticlesData;
        }

        public async Task<Books.response> UpdateBooks(Books.BooksDetail book)
        {
            Books.response ArticlesData = new Books.response();
            Books.BooksDetail responseDetail = new Books.BooksDetail();

            var db = dbConection();
            var procedure = "[dbo].[UPDATE_BOOKS]";
            var parameters = new
            {
                ID_LIBRO = book.ID_LIBRO,
                Title = book.Title,
                Subtitle = book.Subtitle,
                Price = Convert.ToDecimal(book.Price),
                Image = book.Image,
                Url = book.Url,
                Isbn13 = book.Isbn13
            };

            var Data = await db.QueryAsync<Books.Data>(procedure, parameters, commandType: CommandType.StoredProcedure);

            ArticlesData = new Books.response
            {
                Code = 0,
                Message = "Success Updated"


            };

            return ArticlesData;
        }
    }
}