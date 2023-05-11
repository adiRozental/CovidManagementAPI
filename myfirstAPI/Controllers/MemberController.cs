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
using System.Data.SqlTypes;
using System.Data.SqlClient;


namespace myfirstAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        // create instances of  ConectingMember  with the provided IConfiguration
        private ConectingMember myconn;

        public MemberController(IConfiguration configuration)
        {
            myconn = new ConectingMember(configuration);
        }


        //GET api/member
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // call the GetMemberfromdb method of myconn object to get al members
                var ans = myconn.GetAllMemberfromdb();
                if (ans != null) return Ok(ans);
                else return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            
        }

        //GET api/member/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                // call the GetMemberfromdb method of myconn object to get the members
                var ans = myconn.GetMemberfromdb(id);
                if (ans != null) return Ok(ans);
                else return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        // POST api/Member
        [HttpPost]
        public IActionResult Post([FromBody] Member new_member)
        {
            try
            {
                // validate input fields
                try
                {
                    Convert.ToInt32(new_member.id);
                    Convert.ToInt32(new_member.mobilePhone);
                    Convert.ToInt32(new_member.telephone);
                    Convert.ToInt32(new_member.buildingNumber);
                    Convert.ToInt32(new_member.id);
                    Convert.ToDateTime(new_member.dateOfBirth);
                }
                catch (Exception ex)
                {

                    return BadRequest("Error! input is invalid. Note: ID,telephone and mobilePhone should be a number.");
                }
                myconn.Post(new_member);
                // return the newly created vaccine details with status code 201 Created
                return CreatedAtAction(nameof(Get), new { id = new_member.id }, new_member);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            

        }
    }
}