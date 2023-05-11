using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myfirstAPI.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

using myfirstAPI.Data;

namespace myfirstAPI.Data
{
    public class ConectingVaccine
    {

        private readonly string _connectionString;

        /// <summary>
        /// Constructor for the ConectingVaccine class.
        /// </summary>
        /// <param name="configuration">Configuration object containing the connection string for the database.</param>
        public ConectingVaccine(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        /// <summary>
        /// Retrieves a list of vaccine details for a specific member from the database.
        /// </summary>
        /// <param name="id">The ID of the member to retrieve vaccine details for.</param>
        /// <returns>An IEnumerable of VaccineDetails objects.</returns>
        public IEnumerable<VaccineDetails> GetVaccinefromdb(string id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            {
                    connection.Open();
                    string query = "SELECT * FROM VaccineDetails WHERE MemberID = @id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    IEnumerable<VaccineDetails> vaccineDetailsList = Enumerable.Empty<VaccineDetails>();
                    using (MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VaccineDetails vaccineDetail = new VaccineDetails
                            {
                                memberID = reader.GetString("MemberID"),
                                vaccDate = reader.GetDateTime("Date"),
                                vaccProducer = reader.GetString("VaccProducer")
                            };

                            vaccineDetailsList = vaccineDetailsList.Append(vaccineDetail);
                        }
                    }

                    if (vaccineDetailsList.Any())
                    {
                        return vaccineDetailsList;
                    }
                    else
                    {
                        return null;
                    }
            }
                
        }

        /// <summary>
        /// Adds a new vaccine detail to the database.
        /// </summary>
        /// <param name="vacDetail">The VaccineDetails object to add.</param>
        public void Post(VaccineDetails vacDetail)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {

                connection.Open();
                string query = "INSERT INTO VaccineDetails (MemberId, Date, VaccProducer) VALUES(@MemberId, @Date, @VaccProducer);";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@MemberId", vacDetail.memberID);
                command.Parameters.AddWithValue("@Date", vacDetail.vaccDate);
                command.Parameters.AddWithValue("@VaccProducer", vacDetail.vaccProducer);
                command.ExecuteNonQuery();

            }
        }
        
    }
}
