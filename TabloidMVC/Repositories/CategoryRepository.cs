using TabloidMVC.Models;
using TabloidMVC.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration config) : base(config) { }
        public List<Category> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Category ORDER BY name";
                    var reader = cmd.ExecuteReader();

                    var categories = new List<Category>();

                    while (reader.Read())
                    {
                        categories.Add(new Category()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return categories;
                }
            }
        }
        public Category GetById(int CategoryId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Category WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", CategoryId);
                    var reader = cmd.ExecuteReader();

                    Category category = null;

                    while (reader.Read())
                    {
                        if (category == null)
                        {
                            category = new Category
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        };
                        }
                    }

                    reader.Close();

                    return category;
                }
            }
        }
        public void Edit (Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE Category
                    SET Name = @Name
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.Parameters.AddWithValue("@Id", category.Id);
                    cmd.ExecuteNonQuery();
                }
                }
        }


    }
}