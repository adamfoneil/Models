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

    public class EmployeeTable : DbTable<Employee>, IDbGetRelated
    {
        public EmployeeTable(Employee model) : base(model)
        {
        }

        public async Task GetRelatedAsync(IDbConnection connection, IDbTransaction txn = null)
        {
            Model.Reports = await connection.QueryAsync<int>("SELECT [Id] FROM [dbo].[Employee] WHERE [ManagerId]=@id", new { id = Model.Id });
        }        
    }    
}
