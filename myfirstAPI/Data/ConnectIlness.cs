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
    public class ConectIllness
    {

        private readonly string _connectionString;

        public ConectIllness(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // This method returns a list of illness details from the database for a given member id
        public IEnumerable<IllnessDetails> GetIllnessDetailsfromdb(string id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            {
                connection.Open();
                // Define the SQL query to return all records from the IllnessDetails table where the MemberID is equal to the provided id parameter
                string query = "SELECT * FROM IllnessDetails WHERE MemberID = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                IEnumerable<IllnessDetails> illnessDetailsList = Enumerable.Empty<IllnessDetails>();
                using (MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Create a new IllnessDetails object and set its properties with data from the current record
                        var illnessDetail = new IllnessDetails
                        {
                            memberID = reader.GetString("MemberID"),
                            IllDate = reader.GetDateTime("IllDate"),
                            RecoveryDate = reader.GetDateTime("RecoveryDate"),
                        };
                        // Add the new IllnessDetails object to the list of illness details
                        illnessDetailsList = illnessDetailsList.Append(illnessDetail);
                    }
                    if (illnessDetailsList.Any())
                    {
                        return illnessDetailsList;
                    }
                    else
                    {
                        return null;
                    }
                }
                    

            }

        }

        // This method adds a new illness detail record to the database
        public void Post(IllnessDetails illDetail)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                // Check if the IllDate has been set to a valid date (not null)
                if (illDetail.IllDate == DateTime.MinValue)
                    throw new Exception("One field is missing!");
                connection.Open();
                string query = "INSERT INTO IllnessDetails (MemberId, IllDate, RecoveryDate) VALUES(@MemberId, @IllDate, @RecoveryDate);";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@MemberId", illDetail.memberID);
                command.Parameters.AddWithValue("@IllDate", illDetail.IllDate);
                command.Parameters.AddWithValue("@RecoveryDate", illDetail.RecoveryDate);
                command.ExecuteNonQuery();
                }
        }
    }
}
