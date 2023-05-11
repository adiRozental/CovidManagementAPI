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
    public class VaccineDetailsController : ControllerBase
    {
        private readonly string _connectionString;
        private ConectingVaccine myconn;
        private ConectingMember other_conn;

        public VaccineDetailsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");

            // create instances of ConectingVaccine and ConectingMember classes with the provided IConfiguration
            myconn = new ConectingVaccine(configuration);
            other_conn = new ConectingMember(configuration);
        }

        // GET api/VaccineDetails/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                // call the GetVaccinefromdb method of myconn object to retrieve the vaccine details
                var ans = myconn.GetVaccinefromdb(id);
                if (ans != null)
                {
                    return Ok(ans);
                }
                else
                {
                    // return NotFound if no vaccine details are found
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST api/VaccineDetails
        [HttpPost]
        public IActionResult Post([FromBody] VaccineDetails vacDetail)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    // validate input fields
                    try
                    {
                        Convert.ToInt32(vacDetail.memberID);
                        Convert.ToDateTime(vacDetail.vaccDate);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest("Error! input is invalid. Note: memberID should be a number.");
                    }

                    // check if the member exists in the system using other_conn object
                    var member = other_conn.GetMemberfromdb(vacDetail.memberID);
                    if (member == null)
                    {
                        return NotFound("Member is not in system!");
                    }

                    IEnumerable<VaccineDetails> vaclist = myconn.GetVaccinefromdb(vacDetail.memberID);
                    // check if the member has already received 4 vaccines
                    if (vaclist is not null && vaclist.Count() >= 4)
                    {
                        return BadRequest("Can not add more than 4 vaccines!");
                    }
                    myconn.Post(vacDetail);
                    // return the newly created vaccine details with status code 201 Created
                    return CreatedAtAction(nameof(Get), new { id = vacDetail.memberID }, vacDetail);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
            }
        }
    }
}
