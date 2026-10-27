using Microsoft.Data.SqlClient;
using System.Drawing;

namespace HW_260311_ToDoItems.Models
{
    public class ToDoItemsDbManager
    {
        private string _connectionString;

        public ToDoItemsDbManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Item> GetActiveItems()
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Items i
                                    JOIN Categories c
                                    ON c.CategoryId = i.CategoryId 
                                    WHERE IsCompleted = 0";
            connection.Open();
            var items = new List<Item>();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Item
                {
                    ItemId = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    IsCompleted = (bool)reader["IsCompleted"],
                    DateCompleted = reader.GetOrNull<DateTime?>("DateCompleted"),
                    Name = (string)reader["Name"]
                });                
            }
            return items;
        }

        public void MarkAsCompleted(int itemId)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Items SET IsCompleted = 1, DateCompleted = @dateCompleted WHERE Id = @itemId";
            command.Parameters.AddWithValue("@dateCompleted", DateTime.Now);
            command.Parameters.AddWithValue("@itemId", itemId);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Item> GetCompletedItems()
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Items i
                                    JOIN Categories c
                                    ON c.CategoryId = i.CategoryId 
                                    WHERE IsCompleted = 1";
            connection.Open();
            var items = new List<Item>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Item
                {
                    ItemId = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    IsCompleted = (bool)reader["IsCompleted"],
                    DateCompleted = reader.GetOrNull<DateTime?>("DateCompleted"),
                    CategoryId = (int)reader["CategoryId"],
                    Name = (string)reader["Name"]
                });
            }
            return items;
        }

        public List<Category> GetAllCategories()
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Categories";
            connection.Open();
            var categories = new List<Category>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    CategoryId = (int)reader["CategoryId"],
                    Name = (string)reader["Name"]
                });
            }
            return categories;
        }

        public void AddItem(Item item)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Items 
                                    (Title, DueDate, IsCompleted, CategoryId) 
                                    VALUES (@title, @dueDate, 0, @categoryId);";
            command.Parameters.AddWithValue("@title", item.Title);
            command.Parameters.AddWithValue("@dueDate", item.DueDate);
            command.Parameters.AddWithValue("@categoryId", item.CategoryId);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void AddCategory(string name)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Categories (Name) VALUES (@name)";
            command.Parameters.AddWithValue("@name", name);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public Category GetCategoryById(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Categories
                                    WHERE categoryId = @categoryId";
            command.Parameters.AddWithValue("@categoryId", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;                
            }
            else
            {
                return new Category
                {
                    CategoryId = (int)reader["CategoryId"],
                    Name = (string)reader["Name"]
                };
            }
        }

        public void UpdateCategory(Category category)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Categories 
                                    SET Name = @name
                                    WHERE CategoryId = @categoryId";
            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@categoryId", category.CategoryId);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Item> GetByCategory(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Items i
                                    JOIN Categories c
                                    ON c.CategoryId = i.CategoryId 
                                    WHERE i.CategoryId = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var items = new List<Item>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Item
                {
                    ItemId = (int)reader["Id"],
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    IsCompleted = (bool)reader["IsCompleted"],
                    DateCompleted = reader.GetOrNull<DateTime?>("DateCompleted"),
                    CategoryId = (int)reader["CategoryId"],
                    Name = (string)reader["Name"]
                });
            }
            return items;
        }
    }
}
