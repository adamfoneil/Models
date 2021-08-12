using AO.Models.Static;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbSchema.Tests
{
    [TestClass]
    public class SqlBuilderTests
    {
        [TestMethod]
        public void SqlGet()
        {
            var result = SqlBuilder.Get<Models.Employee>("Id");
            Assert.IsTrue(result.Equals("SELECT * FROM [Employee] WHERE [Id]=@Id"));
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

    }
}
