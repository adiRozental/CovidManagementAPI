using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myfirstAPI.Models;
using myfirstAPI.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;


namespace myfirstAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IllnessDetailsController : ControllerBase
    {
        private readonly string _connectionString;
        private ConectIllness myconn;
        private ConectingMember other_conn;



        public IllnessDetailsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
            myconn = new ConectIllness(configuration);
            other_conn = new ConectingMember(configuration);
        }


        //GET api/IllnessDetails/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                // call the GetIllnessDetailsfromdb method of myconn object to gett the illness details
                MySqlConnection connection = new MySqlConnection(_connectionString);
                {
                    var ans = myconn.GetIllnessDetailsfromdb(id);
                    if (ans != null) return Ok(ans);
                    else return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        // POST api/IllnessDetails
        [HttpPost]
        public IActionResult Post([FromBody] IllnessDetails illDetail)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    // validate input fields
                    try
                    {
                        Convert.ToInt32(illDetail.memberID);
                        Convert.ToDateTime(illDetail.IllDate);
                        Convert.ToDateTime(illDetail.RecoveryDate);
                    }
                    catch (Exception ex)
                    {

                       return BadRequest("Error! input is invalid. Note: memberID should be a number.");
                    }
                    // check if the member exists in the system using other_conn object
                    var ans = other_conn.GetMemberfromdb(illDetail.memberID);
                    if (ans == null) { return NotFound("Member is not in system!"); }
                    myconn.Post(illDetail);
                    // return the newly created illness details with status code 201 Created
                    return CreatedAtAction(nameof(Get), new { id = illDetail.memberID }, illDetail);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
            }
        }
    }
}
