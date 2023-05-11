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
    public class ConectingMember
    {
        // The connection string for the MySQL database is retrieved
        // through dependency injection from the IConfiguration instance.
        private readonly string _connectionString;

        public ConectingMember(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // Return a single member from the database by their ID.
        public Member GetMemberfromdb(string id)
        {
            // Create a new MySqlConnection instance using the connection string.
            MySqlConnection connection = new MySqlConnection(_connectionString);
            {

                connection.Open();
                string query = "SELECT * FROM Member WHERE id = @id";   // Define a query string to select the member with the given ID.
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                // Execute the command and return the  Member object,
                // or return null if no result is found.
                using (MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader())
                    if (reader.Read())
                    {
                        var person = new Member
                        {
                            id = reader.GetString("id"),
                            firstName = reader.GetString("FirstName"),
                            lastName = reader.GetString("LastName"),
                            dateOfBirth = reader.GetDateTime("Birthdate"),
                            city = reader.GetString("city"),
                            street = reader.GetString("street"),
                            buildingNumber = reader.GetString("Number"),
                            mobilePhone = reader.GetString("Phone"),
                            telephone = reader.GetString("Smartphone"),
                        };

                        return person;
                    }
                    else
                    {
                        return null;
                    }
                
            }
        }

        // Insert a new member into the database.
        public void Post( Member person)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                // Define a query string to insert a new member into the Member table.
                string query = "INSERT INTO Member (id, FirstName, LastName, BirthDate, City, Street, Number, Phone, Smartphone) VALUES(@id, @FirstName, @LastName, @BirthDate, @City, @Street, @Number, @Phone, @Smartphone);";
                MySqlCommand command = new MySqlCommand(query, connection); // Create a new MySqlCommand instance with the query and connection.
                // Set the command parameters to the properties of the given Member object.
                command.Parameters.AddWithValue("@FirstName", person.firstName);
                command.Parameters.AddWithValue("@LastName", person.mobilePhone);
                command.Parameters.AddWithValue("@BirthDate", person.dateOfBirth);
                command.Parameters.AddWithValue("@City", person.city);
                command.Parameters.AddWithValue("@Street", person.street);
                command.Parameters.AddWithValue("@Number", person.buildingNumber);
                command.Parameters.AddWithValue("@Phone", person.telephone);
                command.Parameters.AddWithValue("@Smartphone", person.mobilePhone);
                command.Parameters.AddWithValue("@id", person.id);
                command.ExecuteNonQuery();
            }
        }

        // Method that updates an existing member in the database
        public void Update(Member person)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Member SET FirstName = @FirstName, LastName = @LastName, Birthdate = @Birthdate, City = @City, Street = @Street, Number = @Number, Phone = @Phone, Smartphone = @Smartphone WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", person.firstName);
                command.Parameters.AddWithValue("@LastName", person.mobilePhone);
                command.Parameters.AddWithValue("@Birthdate", person.dateOfBirth);
                command.Parameters.AddWithValue("@City", person.city);
                command.Parameters.AddWithValue("@Street", person.street);
                command.Parameters.AddWithValue("@Number", person.buildingNumber);
                command.Parameters.AddWithValue("@Phone", person.telephone);
                command.Parameters.AddWithValue("@Smartphone", person.mobilePhone);
                command.Parameters.AddWithValue("@id", person.id);
                command.ExecuteNonQuery();
            }
        }
        // Return all the members from the database.
        public IEnumerable<Member> GetAllMemberfromdb()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            {
                try
                {                
                    connection.Open();
                    string query = "SELECT * FROM Member";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    IEnumerable<Member> membersList = Enumerable.Empty<Member>();
                    using (MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader())
                    {
                        // Execute the command and return all the  Member object,
                        // or return null if no result is found
                        while (reader.Read())
                        {
                            var person = new Member
                            {
                                id = reader.GetString("id"),
                                firstName = reader.GetString("FirstName"),
                                lastName = reader.GetString("LastName"),
                                dateOfBirth = reader.GetDateTime("Birthdate"),
                                city = reader.GetString("city"),
                                street = reader.GetString("street"),
                                buildingNumber = reader.GetString("Number"),
                                mobilePhone = reader.GetString("Smartphone"),
                                telephone = reader.GetString("Phone"),
                            };

                            membersList = membersList.Append(person);
                        }
                    }

                    if (membersList.Any())
                    {
                        return membersList;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
