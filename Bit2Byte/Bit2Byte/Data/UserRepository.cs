using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bit2Byte.Data.Models;

namespace Bit2Byte.Data
{
    public class UserRepository
    {
        public int Create(User user)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO dbo.Users (Username, Email, PasswordHash, IsActive)
                                     VALUES (@u,@e,@p,@a); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.PasswordHash);
                cmd.Parameters.AddWithValue("@a", user.IsActive);

                var id = cmd.ExecuteScalar();
                return Convert.ToInt32(id);
            }
        }

        public User GetById(int id)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Username, Email, PasswordHash, IsActive, CreatedAt FROM dbo.Users WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return Map(r);
                    }
                }
            }
            return null;
        }

        public User GetByUsername(string username)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Username, Email, PasswordHash, IsActive, CreatedAt FROM dbo.Users WHERE Username=@u";
                cmd.Parameters.AddWithValue("@u", username);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                }
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            var list = new List<User>();
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Username, Email, PasswordHash, IsActive, CreatedAt FROM dbo.Users ORDER BY CreatedAt DESC";
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public void Update(User user)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE dbo.Users SET Username=@u, Email=@e, PasswordHash=@p, IsActive=@a WHERE Id=@id";
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.PasswordHash);
                cmd.Parameters.AddWithValue("@a", user.IsActive);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM dbo.Users WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private User Map(SqlDataReader r)
        {
            return new User
            {
                Id = r.GetInt32(r.GetOrdinal("Id")),
                Username = r.GetString(r.GetOrdinal("Username")),
                Email = r.GetString(r.GetOrdinal("Email")),
                PasswordHash = r.GetString(r.GetOrdinal("PasswordHash")),
                IsActive = r.GetBoolean(r.GetOrdinal("IsActive")),
                CreatedAt = r.GetDateTime(r.GetOrdinal("CreatedAt"))
            };
        }
    }
}
