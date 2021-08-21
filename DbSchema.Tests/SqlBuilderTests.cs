using AO.Models.Models;
using AO.Models.Static;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbSchema.Tests
{
    [TestClass]
    public class SqlBuilderTests
    {
        [TestMethod]
        public void SqlGet()
        {
            var result = SqlBuilder.Get<Models.Employee>(identityColumn: "Id");
            Assert.IsTrue(result.Equals("SELECT * FROM [Employee] WHERE [Id]=@Id"));
        }

        [TestMethod]
        public void SqlGetWhere()
        {
            var result = SqlBuilder.GetWhere<Models.Employee>(new { firstName = "hello", lastName = "nobody" });
            Assert.IsTrue(result.Equals("SELECT * FROM [Employee] WHERE [firstName]=@firstName AND [lastName]=@lastName"));
        }

        [TestMethod]
        public void InsertWithColumns()
        {            
            var result = SqlBuilder.Insert<Models.Employee>(new string[] { "FirstName", "LastName" });            
            Assert.IsTrue(result.ReplaceWhitespace().Equals(
                @"INSERT INTO [Employee] (
                    [FirstName], [LastName]
                ) VALUES (
                    @FirstName, @LastName
                );".ReplaceWhitespace()));
        }

        [TestMethod]
        public void UpdateWithColumns()
        {            
            var result = SqlBuilder.Update<Models.Employee>(columnNames: new string[] { "FirstName", "LastName" });
            Assert.IsTrue(result.Equals(
                @"UPDATE [Employee] SET 
                    [FirstName]=@FirstName, [LastName]=@LastName 
                WHERE 
                    [Id]=@Id"));
        }

        [TestMethod]
        public void InsertStatementBase()
        {            
            string sql = SqlBuilder.Insert<Models.Employee>();
            const string result =
                @"INSERT INTO [Employee] (
                    [FirstName], [LastName], [HireDate], [TermDate], [IsExempt], [Timestamp], [Status], [Value]
                ) VALUES (
                    @FirstName, @LastName, @HireDate, @TermDate, @IsExempt, @Timestamp, @Status, @Value
                );";

            Assert.IsTrue(sql.ReplaceWhitespace().Equals(result.ReplaceWhitespace()));
        }

        [TestMethod]
        public void AtpyicalInsert()
        {
            string sql = SqlBuilder.Insert<Models.Role>();
            const string result =
                @"INSERT INTO [AspNetRoles] (
                    [Id], [Name], [NormalizedName], [ConcurrencyStamp]
                ) VALUES (
                    @RoleId, @Name, @NormalizedName, @ConcurrencyStamp
                );";
            Assert.IsTrue(sql.ReplaceWhitespace().Equals(result.ReplaceWhitespace()));
        }

        [TestMethod]
        public void AtypicalUpdate()
        {
            string sql = SqlBuilder.Update<Models.Role>(identityColumn: "Id", identityParam: "RoleId");
            const string result = 
                @"UPDATE [AspNetRoles] SET 
                    [Name]=@Name, [NormalizedName]=@NormalizedName, [ConcurrencyStamp]=@ConcurrencyStamp 
                WHERE 
                    [Id]=@RoleId";
            Assert.IsTrue(sql.ReplaceWhitespace().Equals(result.ReplaceWhitespace()));
        }

        [TestMethod]
        public void UpdateStatementBase()
        {
            var emp = new Employee();
            string sql = SqlBuilder.Update<Models.Employee>();
            const string result =
                @"UPDATE [Employee] SET 
                    [FirstName]=@FirstName, [LastName]=@LastName, [HireDate]=@HireDate, [TermDate]=@TermDate, [IsExempt]=@IsExempt, [Timestamp]=@Timestamp, [Status]=@Status, [Value]=@Value
                WHERE
                    [Id]=@Id";

            Assert.IsTrue(sql.ReplaceWhitespace().Equals(result.ReplaceWhitespace()));
        }


        [TestMethod]
        public void InsertExplicitTableName()
        {
            var ins = SqlBuilder.Insert<Models.Employee>(tableName: "dbo.Hello");
            Assert.IsTrue(ins.Equals(@"INSERT INTO [dbo].[Hello] (
                    [FirstName], [LastName], [HireDate], [TermDate], [IsExempt], [Timestamp], [Status], [Value]
                ) VALUES (
                    @FirstName, @LastName, @HireDate, @TermDate, @IsExempt, @Timestamp, @Status, @Value
                );"));
        }

        [TestMethod]
        public void UpdateExplicitTableName()
        {
            var upd = SqlBuilder.Update<Models.Employee>(tableName: "Hello");
            Assert.IsTrue(upd.Equals(@"UPDATE [Hello] SET 
                    [FirstName]=@FirstName, [LastName]=@LastName, [HireDate]=@HireDate, [TermDate]=@TermDate, [IsExempt]=@IsExempt, [Timestamp]=@Timestamp, [Status]=@Status, [Value]=@Value 
                WHERE 
                    [Id]=@Id"));
        }

        [TestMethod]
        public void UpdateExplicitTableAndIdentity()
        {
            var upd = SqlBuilder.Update<Models.Employee>(tableName: "Hello", identityColumn:"Yoohoo");
            Assert.IsTrue(upd.Equals(@"UPDATE [Hello] SET 
                    [FirstName]=@FirstName, [LastName]=@LastName, [HireDate]=@HireDate, [TermDate]=@TermDate, [IsExempt]=@IsExempt, [Timestamp]=@Timestamp, [Status]=@Status, [Value]=@Value 
                WHERE 
                    [Yoohoo]=@Yoohoo"));
        }

        [TestMethod]
        public void DeleteExplicitTableName()
        {
            var del = SqlBuilder.Delete<Models.Employee>(tableName: "Hello");
            Assert.IsTrue(del.Equals("DELETE [Hello] WHERE [Id]=@id"));
        }

        [TestMethod]
        public void InsertAvoidExplicitIdentity()
        {
            var ins = SqlBuilder.Insert<Models.AtypicalIdentity>(tableName: "dbo.Hello", identityColumn: "RoleId");
            Assert.IsTrue(ins.Equals(@"INSERT INTO [dbo].[Hello] (
                    [Name], [NormalizedName], [ConcurrencyStamp]
                ) VALUES (
                    @Name, @NormalizedName, @ConcurrencyStamp
                );"));
        }

        [TestMethod]
        public void UpdateAtypicalIdentity()
        {
            var upd = SqlBuilder.Update<Models.AtypicalIdentity>(tableName: "dbo.Hello", identityColumn: "RoleId");
            Assert.IsTrue(upd.Equals(@"UPDATE [dbo].[Hello] SET 
                    [Name]=@Name, [NormalizedName]=@NormalizedName, [ConcurrencyStamp]=@ConcurrencyStamp 
                WHERE 
                    [RoleId]=@RoleId"));
        }

        [TestMethod]
        public void InsertColumnsWhere()
        {
            var ins = SqlBuilder.Insert<Models.Employee>(propertiesWhere: (pi) => pi.PropertyType.Equals(typeof(string)));
            Assert.IsTrue(ins.Equals(@"INSERT INTO [Employee] (
                    [FirstName], [LastName]
                ) VALUES (
                    @FirstName, @LastName
                );"));
        }

        [TestMethod]
        public void UpdateColumnsWhere()
        {
            var upd = SqlBuilder.Update<Models.Employee>(propertiesWhere: (pi) => pi.PropertyType.Equals(typeof(string)));
            Assert.IsTrue(upd.Equals(@"UPDATE [Employee] SET 
                    [FirstName]=@FirstName, [LastName]=@LastName 
                WHERE 
                    [Id]=@Id"));
        }

        [TestMethod]
        public void UpdateWhereSimpleCase()
        {
            var upd = SqlBuilder.UpdateWhere<Models.Employee>(new[] { "FirstName", "LastName" });
            Assert.IsTrue(upd.Equals(@"UPDATE [Employee] SET 
                    [HireDate]=@HireDate, [TermDate]=@TermDate, [IsExempt]=@IsExempt, [Timestamp]=@Timestamp, [Status]=@Status, [Value]=@Value 
                WHERE 
                    [FirstName]=@FirstName AND [LastName]=@LastName"));
        }

        [TestMethod]
        public void UpdateWhereAdvanced()
        {
            var upd = SqlBuilder.UpdateWhere<Models.Employee>(
                new[] { "FirstName", "LastName" }, 
                new[] { "HireDate", "TermDate" }, tableName: "dbo.Whatever");

            Assert.IsTrue(upd.Equals(@"UPDATE [dbo].[Whatever] SET 
                    [HireDate]=@HireDate, [TermDate]=@TermDate 
                WHERE 
                    [FirstName]=@FirstName AND [LastName]=@LastName"));
        }

        [TestMethod]
        public void InsertUserProfile()
        {
            var ins = SqlBuilder.Insert<UserProfileBase>(new[]
            {
                "Id", "UserName", "Email", "EmailConfirmed", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "TimeZoneId"
            });
            Assert.IsTrue(ins.Equals(
                @"INSERT INTO [AspNetUsers] (
                    [Id], [UserName], [Email], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [TimeZoneId]
                ) VALUES (
                    @Id, @UserName, @Email, @EmailConfirmed, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @TimeZoneId
                );"));
        }
    }
}
