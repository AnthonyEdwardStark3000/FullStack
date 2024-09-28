using System.Data;
using blog_backend.Models;
using blog_backend.Repository.Interface;
using Dapper;
using MySql.Data.MySqlClient;
using NLog;

namespace blog_backend.Repository{
    public class UserRepository:IUserRepository
    {
        private readonly IDbConnection _db;
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
    }
}