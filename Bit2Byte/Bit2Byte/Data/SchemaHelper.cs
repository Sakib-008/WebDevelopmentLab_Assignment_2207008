using System;
using System.Data.SqlClient;

namespace Bit2Byte.Data
{
    public static class SchemaHelper
    {
        public static void EnsureSchema()
        {
            using (var conn = DatabaseHelper.GetOpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
IF OBJECT_ID('dbo.Users','U') IS NOT NULL
BEGIN
    IF COL_LENGTH('dbo.Users','PendingEmail') IS NULL ALTER TABLE dbo.Users ADD PendingEmail NVARCHAR(256) NULL;
    IF COL_LENGTH('dbo.Users','AvatarPath') IS NULL ALTER TABLE dbo.Users ADD AvatarPath NVARCHAR(260) NULL;
    IF COL_LENGTH('dbo.Users','Bio') IS NULL ALTER TABLE dbo.Users ADD Bio NVARCHAR(MAX) NULL;
    IF COL_LENGTH('dbo.Users','Interests') IS NULL ALTER TABLE dbo.Users ADD Interests NVARCHAR(MAX) NULL;
    IF COL_LENGTH('dbo.Users','EmailChangeToken') IS NULL ALTER TABLE dbo.Users ADD EmailChangeToken NVARCHAR(100) NULL;
    IF COL_LENGTH('dbo.Users','EmailChangeTokenExpires') IS NULL ALTER TABLE dbo.Users ADD EmailChangeTokenExpires DATETIME NULL;
    IF COL_LENGTH('dbo.Users','Role') IS NULL ALTER TABLE dbo.Users ADD Role NVARCHAR(50) NOT NULL CONSTRAINT DF_Users_Role DEFAULT('member');
END";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
