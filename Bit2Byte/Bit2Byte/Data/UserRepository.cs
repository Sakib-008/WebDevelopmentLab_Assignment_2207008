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
                cmd.CommandText = @"INSERT INTO dbo.Users (Username, Email, PendingEmail, PasswordHash, AvatarPath, Bio, Interests, EmailChangeToken, EmailChangeTokenExpires, IsActive, Role)
                                     VALUES (@u,@e,@pe,@p,@ap,@b,@i,@token,@tokenExpires,@a,@r); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@pe", (object)user.PendingEmail ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@p", user.PasswordHash);
                cmd.Parameters.AddWithValue("@ap", (object)user.AvatarPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@b", (object)user.Bio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@i", (object)user.Interests ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@token", (object)user.EmailChangeToken ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tokenExpires", (object)user.EmailChangeTokenExpires ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@a", user.IsActive);
                cmd.Parameters.AddWithValue("@r", (object)user.Role ?? "member");

                var id = cmd.ExecuteScalar();
                return Convert.ToInt32(id);
            }
        }

        public User GetById(int id)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Username, Email, PendingEmail, PasswordHash, AvatarPath, Bio, Interests, EmailChangeToken, EmailChangeTokenExpires, Role, IsActive, CreatedAt FROM dbo.Users WHERE Id=@id";
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
                cmd.CommandText = "SELECT Id, Username, Email, PendingEmail, PasswordHash, AvatarPath, Bio, Interests, EmailChangeToken, EmailChangeTokenExpires, Role, IsActive, CreatedAt FROM dbo.Users WHERE Username=@u";
                cmd.Parameters.AddWithValue("@u", username);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                }
            }
            return null;
        }

        public User GetByEmail(string email)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Username, Email, PendingEmail, PasswordHash, AvatarPath, Bio, Interests, EmailChangeToken, EmailChangeTokenExpires, Role, IsActive, CreatedAt FROM dbo.Users WHERE Email=@e";
                cmd.Parameters.AddWithValue("@e", email);
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
                cmd.CommandText = "SELECT Id, Username, Email, PendingEmail, PasswordHash, AvatarPath, Bio, Interests, EmailChangeToken, EmailChangeTokenExpires, Role, IsActive, CreatedAt FROM dbo.Users ORDER BY CreatedAt DESC";
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public IEnumerable<User> GetActiveMembers()
        {
            var list = new List<User>();
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, Username, Email, PendingEmail, PasswordHash, AvatarPath, Bio, Interests,
                                           EmailChangeToken, EmailChangeTokenExpires, Role, IsActive, CreatedAt
                                    FROM dbo.Users
                                    WHERE IsActive = 1
                                    ORDER BY Username";
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
                cmd.CommandText = @"UPDATE dbo.Users
                                     SET Username=@u,
                                         Email=@e,
                                         PendingEmail=@pe,
                                         PasswordHash=@p,
                                         AvatarPath=@ap,
                                         Bio=@b,
                                         Interests=@i,
                                         EmailChangeToken=@token,
                                         EmailChangeTokenExpires=@tokenExpires,
                                         IsActive=@a,
                                         Role=@r
                                     WHERE Id=@id";
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@pe", (object)user.PendingEmail ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@p", user.PasswordHash);
                cmd.Parameters.AddWithValue("@ap", (object)user.AvatarPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@b", (object)user.Bio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@i", (object)user.Interests ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@token", (object)user.EmailChangeToken ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tokenExpires", (object)user.EmailChangeTokenExpires ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@a", user.IsActive);
                cmd.Parameters.AddWithValue("@r", (object)user.Role ?? "member");
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public bool IsEmailInUse(string email, int excludeUserId)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(1) FROM dbo.Users WHERE Email=@e AND Id<>@id";
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@id", excludeUserId);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool RequestEmailChange(int userId, string pendingEmail, string token, DateTime tokenExpiresUtc)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE dbo.Users
                                    SET PendingEmail=@pe,
                                        EmailChangeToken=@token,
                                        EmailChangeTokenExpires=@expires
                                    WHERE Id=@id";
                cmd.Parameters.AddWithValue("@pe", pendingEmail);
                cmd.Parameters.AddWithValue("@token", token);
                cmd.Parameters.AddWithValue("@expires", tokenExpiresUtc);
                cmd.Parameters.AddWithValue("@id", userId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool ConfirmEmailChange(string token)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE dbo.Users
                                    SET Email = PendingEmail,
                                        PendingEmail = NULL,
                                        EmailChangeToken = NULL,
                                        EmailChangeTokenExpires = NULL
                                    WHERE EmailChangeToken=@token
                                      AND EmailChangeTokenExpires IS NOT NULL
                                      AND EmailChangeTokenExpires > GETUTCDATE()
                                      AND PendingEmail IS NOT NULL";
                cmd.Parameters.AddWithValue("@token", token);
                return cmd.ExecuteNonQuery() > 0;
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
                PendingEmail = r.IsDBNull(r.GetOrdinal("PendingEmail")) ? null : r.GetString(r.GetOrdinal("PendingEmail")),
                PasswordHash = r.GetString(r.GetOrdinal("PasswordHash")),
                AvatarPath = r.IsDBNull(r.GetOrdinal("AvatarPath")) ? null : r.GetString(r.GetOrdinal("AvatarPath")),
                Bio = r.IsDBNull(r.GetOrdinal("Bio")) ? null : r.GetString(r.GetOrdinal("Bio")),
                Interests = r.IsDBNull(r.GetOrdinal("Interests")) ? null : r.GetString(r.GetOrdinal("Interests")),
                EmailChangeToken = r.IsDBNull(r.GetOrdinal("EmailChangeToken")) ? null : r.GetString(r.GetOrdinal("EmailChangeToken")),
                EmailChangeTokenExpires = r.IsDBNull(r.GetOrdinal("EmailChangeTokenExpires")) ? (DateTime?)null : r.GetDateTime(r.GetOrdinal("EmailChangeTokenExpires")),
                Role = r.IsDBNull(r.GetOrdinal("Role")) ? "member" : r.GetString(r.GetOrdinal("Role")),
                IsActive = r.GetBoolean(r.GetOrdinal("IsActive")),
                CreatedAt = r.GetDateTime(r.GetOrdinal("CreatedAt"))
            };
        }
    }
}
