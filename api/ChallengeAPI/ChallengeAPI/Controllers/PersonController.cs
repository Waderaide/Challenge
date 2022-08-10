using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ChallengeAPI.Models;

namespace ChallengeAPI.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class PersonController : Controller
        {
            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _env;
            public PersonController(IConfiguration configuration, IWebHostEnvironment env)
            {
                _configuration = configuration;
                _env = env;
            }


            [HttpGet]
            [Route("get-all")]
            public JsonResult Get()
            {
                string query = @"
                            select personId, personName, height from Person
                            ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("ChallengeDbConnectionString");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
                return new JsonResult(table);
            }
        }
}
