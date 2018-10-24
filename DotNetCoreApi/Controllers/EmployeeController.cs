using System.Collections.Generic;
using DotNetCoreApi.Context;
using DotNetCoreApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly DbContext _dbContext;

        public EmployeeController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public void Post([FromBody]Employee employee)
        {
            _dbContext.Insert(employee);
        }

        [HttpPut]
        public void Put([FromBody] Employee employee)
        {
            _dbContext.Update(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            _dbContext.Delete<Employee>(id);
        }

        [HttpGet]
        [Route("{id}")]
        public Employee Get(int id)
        {
            return _dbContext.Get<Employee>(id);
        }

        [HttpGet]
        public List<Employee> Get()
        {
            return _dbContext.List<Employee>();
        }
    }
}
