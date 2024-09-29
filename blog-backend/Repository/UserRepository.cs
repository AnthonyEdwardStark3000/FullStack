using System.Data;
using blog_backend.Models;
using blog_backend.Repository.Interface;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MySql.Data.MySqlClient;
using NLog;

namespace blog_backend.Repository{
    public class UserRepository:IUserRepository
    {
        private readonly IDbConnection _db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public UserRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionString:DefaultConnection");
            try{
             _db = new MySqlConnection(connectionString);
             logger.Info("connection established with the database");
            }
            catch(Exception e){
                logger.Error($"connection with db failed with an exception | {e}");
            }
            
        }

        public int Add(User user)
        {
            var queryParam = new{
                @name = user.Name,
                @email = user.Email,
            };
            string query = @"INSERT INTO sample_table (name,email) VALUES(@name,@email);";
            var result = 0;
            try{
                result = _db.Execute(query,queryParam);
                logger.Info($"Query {query} executed successully for the params : {queryParam}");
            }catch(Exception e){
                logger.Error($"Execution of query failed with exception {e}");
            }
            return result;
        }

        public List<User> GetAllUsers()
        {
            string query = @"SELECT * FROM sample_table;";
            var result = _db.Query<User>(query).ToList();
            logger.Info($"Query {query} executed successully");
            return result;
        }

        public async Task<List<Blog>> GetAllBlogs()
        {
            string query = @"
                SELECT 
                    b.Id AS BlogId,
                    b.Title,
                    b.Description,
                    b.Content,
                    b.Image,
                    b.IsFeatured,
                    c.Id AS CategoryId,
                    c.Name AS CategoryName
                FROM 
                    Blogs b
                JOIN 
                Categories c ON b.CategoryId = c.Id;";
            
            var result =  _db.Query<Blog>(query).ToList();
            logger.Info($"Query {query} executed successully");
            return result;
        }

        public Task<Blog> GetBlogById(string id)
        {
            string query = @"
                SELECT 
                    *
                FROM 
                    Blogs 
                WHERE Id = @id;";
            
            var result =  _db.Query<Blog>(query);
            logger.Info($"Query {query} executed successully");
            return (Task<Blog>)result;
        }

        public int AddBlog(Blog blog)
        {
            var queryParam = new{
                @Id = blog.Id,
                @title = blog.Title,
                @description = blog.Description,
                @content = blog.Content,
                @image = blog.Image,
                @isFeatured = blog.IsFeatured,
                @categoryId = blog.CategoryId,
                @categoryName = blog.category.Name,
            };
            string query = @"INSERT INTO Categories(Id,Name)VALUES(@categoryId,@categoryName);INSERT INTO Blogs (Id,Title,Description,Content,Image,IsFeatured,CategoryId) VALUES(@Id,@title,@description,@content,@image,@isFeatured,@categoryId);";
            var result = 0;
            try{
                result = _db.QueryMultiple(query,queryParam).ReadFirstOrDefault()!=null? _db.QueryMultiple(query,queryParam).ReadFirstOrDefault():1;
                logger.Info($"Query {query} executed successully for the params : {queryParam}");
            }catch(Exception e){
                logger.Error($"Execution of query failed with exception {e}");
            }
            return result;
        }

        public int UpdateBlog(string id,Blog b)
        {
            var queryParams = new{
                @title = b.Title,
                @description = b.Description,
                @content = b.Content,
                @image = b.Image,
                @isFeatured = b.IsFeatured,
                @categoryId = b.CategoryId,
                @id = id
            };

            string query = @"
               UPDATE Blogs
                SET 
                    Title = @title,
                    Description = @description,
                    Content = @content,
                    Image = @image,
                    IsFeatured = @isFeatured,
                    CategoryId = @categoryId
                WHERE 
                    Id = @id;";
            
            var result =  _db.Execute(query, queryParams);
            logger.Info($"Query {query} executed successully");
            return result;
        }

        public void DeleteBlog(string id)
        {
            var queryPar = new {
                @id = id
            };
            string query1 = @"SELECT CategoryId 
                FROM Blogs 
                WHERE Id = @id;";
            var CategoryId = string.Empty; 
            CategoryId =  _db.Query<string>(query1,queryPar).FirstOrDefault();

            var queryParams = new {
                @id = id,
                @CategoryId = CategoryId.ToString()
            };
          
            string query = @"
                DELETE FROM Blogs 
                WHERE Id = @id;

                DELETE FROM Categories 
                WHERE Id = @CategoryId 
                AND NOT EXISTS (SELECT 1 FROM Blogs WHERE CategoryId = @CategoryId);";
            
            var result =  _db.QueryMultiple(query,queryParams).ReadFirstOrDefault();
            logger.Info($"Query {query} executed successully");
       
        }
        public Task SaveBlogChanges()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllCategories()
        {
          string query = @"
                SELECT 
                   *
                FROM 
                    Categories;";
            
            var result =  _db.Query<Category>(query).ToList();
            logger.Info($"Query {query} executed successully");
            return result;  
        }

        public List<Blog> GetFeaturedBlog()
        {
             string query = @"
                SELECT 
                    *
                FROM 
                    Blogs 
                WHERE IsFeatured = true;";
            
            var result =  _db.Query<Blog>(query).ToList();
            logger.Info($"Query {query} executed successully");
            return result;
        }
    }
}