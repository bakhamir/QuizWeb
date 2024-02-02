using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using QuizWeb.Models;
using System.Data;

namespace QuizWeb.Controllers
{
    public class ValuesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/CreatePerson")]
        public async Task<bool> CreateUser(string name)
        {
            try
            {
                string connectionString = @"Data Source=207-3; Initial Catalog=QuizDb;Integrated Security=True;TrustServerCertificate=Yes";
                using (var db = new SqlConnection(connectionString))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@name", name);
                    var res =  db.Query<Person>("pPerson", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (res != null)
                        return true;
                    else
                        return false;
                  }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpPost("/CreateVictorina")]
        public async Task<bool> CreateVictorina(int personid,int count)
        {
            try
            {
                string connectionString = @"Data Source=207-3; Initial Catalog=QuizDb;Integrated Security=True;TrustServerCertificate=Yes";
                using (var db = new SqlConnection(connectionString))
                {
                    DynamicParameters parametrs = new DynamicParameters();
                    parametrs.Add("personid", personid);
                    parametrs.Add("count", count);
                    var res = db.Query<Person>("pVictorina", parametrs, commandType: System.Data.CommandType.StoredProcedure);
                    if (res != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet("/GetCountById")]
        public async Task<int> GetCountById(int  id)
        {
            try
            {
                string connectionString = @"Data Source=207-3; Initial Catalog=QuizDb;Integrated Security=True;TrustServerCertificate=Yes";
                using (var db = new SqlConnection(connectionString))
                {
                    DynamicParameters parametrs = new DynamicParameters();
                    parametrs.Add("id", id);
                    parametrs.Add("score", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var res = db.Query<Person>("pPerson;2", parametrs, commandType: System.Data.CommandType.StoredProcedure);
                    return parametrs.Get<int>("score");
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        [HttpGet("/GetAllPerson")]
        public async Task<IEnumerable<Person>> GetAllPerson()
        {
            try
            {
                string connectionString = @"Data Source=207-3; Initial Catalog=QuizDb;Integrated Security=True;TrustServerCertificate=Yes";
                using (var db = new SqlConnection(connectionString))
                { 
                    var res = db.Query<Person>("pPerson;3" ,commandType: System.Data.CommandType.StoredProcedure);
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("/GetAllPersonScore")]
        public async Task<IEnumerable<PersonScore>> GetAllPersonScore()
        {
            try
            {
                string connectionString = @"Data Source=207-3; Initial Catalog=QuizDb;Integrated Security=True;TrustServerCertificate=Yes";
                using (var db = new SqlConnection(connectionString))
                { 
                    var res = db.Query<PersonScore>("pPerson;4" ,commandType: System.Data.CommandType.StoredProcedure);
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("/GetQuestions")]
        public async Task<IEnumerable<PersonScore>> GetQuestions(int amount,string theme)
        {
            try
            {
                string connectionString = @"Data Source=207-3; Initial Catalog=QuizDb;Integrated Security=True;TrustServerCertificate=Yes";
                using (var db = new SqlConnection(connectionString))
                {               
                    DynamicParameters parametrs = new DynamicParameters();
                    parametrs.Add("amount",amount );
                    parametrs.Add("theme",theme);
                    var res = db.Query<PersonScore>("pQuestion", commandType: System.Data.CommandType.StoredProcedure);
                    return res;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }






    }
}
