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
        public async Task<IEnumerable<AnswerQuestion>> GetQuestions(int amount,string theme)
        {
            try
            {
                string connectionString = @"Data Source=207-3; Initial Catalog=QuizDb;Integrated Security=True;TrustServerCertificate=Yes";
                using (var db = new SqlConnection(connectionString))
                {               
                    DynamicParameters parametrs = new DynamicParameters();
                    parametrs.Add("amount",amount );
                    parametrs.Add("theme",theme);
                    var res = db.Query<AnswerQuestion>("pQuestion", commandType: System.Data.CommandType.StoredProcedure);
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
//create table Person(
//id int identity primary key,
//fio nvarchar(200))

//create table Victorina(
//id int identity primary key,
//personid int foreign key references Person,
//count int)

//create table Question(
//id int identity primary key,
//questionText nvarchar(max),
//theme nvarchar(max)
//)

//drop table Question

//insert into Question
//Values
//('Какой физический процесс связан со знаком - лямбда','Физика')



//insert into Question
//Values
//('Чему равно иррациональное число i?','Математика'),
//('Чему равно отношение прилежащего угла к гиппотенузе?', 'Математика'),
//('чему равно число в нулевой степени?', 'Математика')
//Insert into answer 
//values
//(4,'корень из одного',0),(4, 'два делить на 0', 1),(4, '0 в отрицательной степени', 0),(4, 'квадратный корень из минус одного', 1),
//(5, 'tg', 0),(5, 'ctg', 0),(5, 'sin', 0),(5, 'cos', 1),
//(6, '-1', 0),(6, '1', 1),(6, '0', 0),(6, '-0', 0)



//create table Answer(
//id int identity primary key,
//questionId int foreign key references Question,
//answerText nvarchar(max),
//isCorrect bit)

//Insert into answer 
//values
//(3,'Горение',0),(3, 'Плавление', 1),(3, 'Кипение', 0),(3, 'Затвердевание', 0)

//update Question set questionText =  'Какой физический процесс связан со знаком - лямбда' where id = 3









//create proc pPerson
//@name nvarchar(max)
//as
//insert into Person
//values (@name)

//alter proc pPerson; 2
//@id int ,
//@score int output
//as
//set
//@score = (select count from person as p join Victorina as v
//on @id = v.personid)

//declare @points int 
//exec pPerson; 2 1, @points output
//select @points


//create proc pVictorina
//@personid int,
//@count int 
//as
//insert into Victorina
//values(@personid, @count)

//insert into person ()

//select* from person

//create proc pPerson; 3
//as
//select* from Person

//create proc pPerson; 4
//as
//select fio, count from Person as p join Victorina as v on v.personid = p.id

//alter proc pQuestion
//@amount int,
//@theme nvarchar(max)
//as
//select top(@amount * 4) questionText, answerText, isCorrect from Question as q join Answer as a on a.questionId = q.id where theme like @theme


//pQuestion 3, 'Физика'

//select top(10) *questionText from Question as q
//                             join Answer as a on a.questionId = q.id where

