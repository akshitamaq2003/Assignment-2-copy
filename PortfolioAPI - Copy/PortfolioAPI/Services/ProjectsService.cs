using MySql.Data.MySqlClient;
using PortfolioAPI.Models;
using System.Data;
 // Ensure you have the right namespace for DatabaseContext

namespace PortfolioAPI.Services
{
    public class ProjectsService
    {
        private readonly DatabaseContext _databaseContext;

        public ProjectsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // Get all projects
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var projects = new List<Project>();

            try
            {
                using (var connection = _databaseContext.CreateConnection())
                {
                    var query = "SELECT * FROM Projects";
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                projects.Add(new Project
                                {
                                    // Id = reader.GetInt32("Id"),
                                    Title = reader.GetString("Title"),
                                    Description = reader.GetString("Description"),
                                    Technologies = reader.GetString("Technologies"),
                                    ImageUrl = reader.GetString("ImageUrl"),
                                    Date = reader.GetDateTime("Date")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
                throw;  // Optionally rethrow the exception to handle in the controller
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;  // Optionally rethrow the exception to handle in the controller
            }

            return projects;
        }

        // Add a new project
        public async Task<bool> AddProjectAsync(Project project)
        {
            try
            {
                using (var connection = _databaseContext.CreateConnection())
                {
                    var query = "INSERT INTO Projects (Title, Description, Technologies, ImageUrl, Date) VALUES (@Title, @Description, @Technologies, @ImageUrl, @Date)";
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", project.Title);
                        command.Parameters.AddWithValue("@Description", project.Description);
                        command.Parameters.AddWithValue("@Technologies", project.Technologies);
                        command.Parameters.AddWithValue("@ImageUrl", project.ImageUrl);
                        command.Parameters.AddWithValue("@Date", project.Date);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return false;
            }
        }

        // Update an existing project
        public async Task<bool> UpdateProjectAsync(int id, Project project)
        {
            try
            {
                using (var connection = _databaseContext.CreateConnection())
                {
                    var query = "UPDATE Projects SET Title = @Title, Description = @Description, Technologies = @Technologies, ImageUrl = @ImageUrl, Date = @Date WHERE Id = @Id";
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(query, connection))
                    {
                         command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Title", project.Title);
                        command.Parameters.AddWithValue("@Description", project.Description);
                        command.Parameters.AddWithValue("@Technologies", project.Technologies);
                        command.Parameters.AddWithValue("@ImageUrl", project.ImageUrl);
                        command.Parameters.AddWithValue("@Date", project.Date);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return false;
            }
        }

        // Delete a project
        public async Task<bool> DeleteProjectAsync(int id)
        {
            try
            {
                using (var connection = _databaseContext.CreateConnection())
                {
                    var query = "DELETE FROM Projects WHERE Id = @Id";
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return false;
            }
        }
    }
}
