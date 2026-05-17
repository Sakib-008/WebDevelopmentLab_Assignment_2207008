using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Bit2Byte.Data.Models;

namespace Bit2Byte.Data
{
    public class EventRepository
    {
        public int Create(EventItem ev)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO dbo.Events (Title, Description, EventDate, CreatedByUserId)
                                     VALUES (@t,@d,@dt,@u); SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("@t", ev.Title);
                cmd.Parameters.AddWithValue("@d", (object)ev.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@dt", ev.EventDate);
                cmd.Parameters.AddWithValue("@u", (object)ev.CreatedByUserId ?? DBNull.Value);

                var id = cmd.ExecuteScalar();
                return Convert.ToInt32(id);
            }
        }

        public EventItem GetById(int id)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Title, Description, EventDate, CreatedByUserId, CreatedAt FROM dbo.Events WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                using (var r = cmd.ExecuteReader())
                {
                    if (r.Read()) return Map(r);
                }
            }
            return null;
        }

        public IEnumerable<EventItem> GetAll()
        {
            var list = new List<EventItem>();
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Title, Description, EventDate, CreatedByUserId, CreatedAt FROM dbo.Events ORDER BY EventDate DESC";
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read()) list.Add(Map(r));
                }
            }
            return list;
        }

        public void Update(EventItem ev)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE dbo.Events SET Title=@t, Description=@d, EventDate=@dt, CreatedByUserId=@u WHERE Id=@id";
                cmd.Parameters.AddWithValue("@t", ev.Title);
                cmd.Parameters.AddWithValue("@d", (object)ev.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@dt", ev.EventDate);
                cmd.Parameters.AddWithValue("@u", (object)ev.CreatedByUserId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", ev.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM dbo.Events WHERE Id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private EventItem Map(SqlDataReader r)
        {
            return new EventItem
            {
                Id = r.GetInt32(r.GetOrdinal("Id")),
                Title = r.GetString(r.GetOrdinal("Title")),
                Description = r.IsDBNull(r.GetOrdinal("Description")) ? null : r.GetString(r.GetOrdinal("Description")),
                EventDate = r.GetDateTime(r.GetOrdinal("EventDate")),
                CreatedByUserId = r.IsDBNull(r.GetOrdinal("CreatedByUserId")) ? (int?)null : r.GetInt32(r.GetOrdinal("CreatedByUserId")),
                CreatedAt = r.GetDateTime(r.GetOrdinal("CreatedAt"))
            };
        }
    }
}
