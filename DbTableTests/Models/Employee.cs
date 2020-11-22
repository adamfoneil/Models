using AO.Models.Classes;
using AO.Models.Enums;
using AO.Models.Interfaces;
using Dapper;
using DbTableTests.Models.Conventions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbTableTests.Models
{
    public class Employee : BaseTable
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TermDate { get; set; }
        public int ManagerId { get; set; }

        public IEnumerable<int> Reports { get; set; }
    }

    public class EmployeeTable : ConventionalTable<Employee>, IGetRelated, IAudit
    {
        public EmployeeTable(Employee model) : base(model)
        {
        }

        public async Task GetRelatedAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            Model.Reports = await connection.QueryAsync<int>("SELECT [Id] FROM [dbo].[Employee] WHERE [ManagerId]=@id", new { id = Model.Id });
        }

        public void Stamp(SaveAction saveAction, IUserBase user) => BaseTable.Stamp(Model, saveAction, user);
        
    }    

    public class ConventionalTable<TModel> : DbTable<TModel> where TModel : BaseTable
    {
        public ConventionalTable(TModel model) : base(model)
        {
        }
    }
}
