using MySql.Data.MySqlClient;
using PortfolioAPI.Models;

using System.Data; // Ensure you have the right namespace for DatabaseContext

namespace PortfolioAPI.Services
{
    public class SkillsService
    {
        private readonly DatabaseContext _databaseContext;

        public SkillsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            var skills = new List<Skill>();

            try
            {
                using (var connection = _databaseContext.CreateConnection())
                {
                    var query = "SELECT * FROM Skills";
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                skills.Add(new Skill
                                {
                                    Id = reader.GetInt32("Id"),
                                    SkillName = reader.GetString("SkillName"),
                                    Proficiency = reader.GetString("Proficiency")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
                throw; // Optionally rethrow the exception to handle in the controller
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw; // Optionally rethrow the exception to handle in the controller
            }

            return skills;
        }
    }
}
