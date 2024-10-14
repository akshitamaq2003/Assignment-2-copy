using MySql.Data.MySqlClient;
using PortfolioAPI.Models;


namespace PortfolioAPI.Services
{
    public class ContactInfoService
    {
        private readonly DatabaseContext _databaseContext;

        public ContactInfoService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> AddContactAsync(ContactInfo contactInfo)
        {
            try
            {
                using (var connection = _databaseContext.CreateConnection())
                {
                    var query = "INSERT INTO ContactInfo (Name, Email, Message) VALUES (@Name, @Email, @Message)";
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Name", contactInfo.Name);
                        command.Parameters.AddWithValue("@Email", contactInfo.Email);
                        command.Parameters.AddWithValue("@Message", contactInfo.Message);

                        // Execute the query and check the result
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        // If rowsAffected > 0, it means the insert was successful
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Log MySQL-specific exceptions (e.g., connectivity issues, query errors)
                Console.WriteLine($"MySQL Error: {ex.Message}");
                return false; // Optionally, throw a custom exception or return false
            }
            catch (Exception ex)
            {
                // Log general exceptions
                Console.WriteLine($"General Error: {ex.Message}");
                return false; // Optionally, rethrow the exception or return false
            }
        }
    }
}
