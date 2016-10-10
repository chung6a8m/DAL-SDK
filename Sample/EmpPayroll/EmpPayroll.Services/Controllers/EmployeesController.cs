﻿using System.Collections.Generic;
using System.Web.Http;
using App.Services.Domain.BussinessMangers.Classes;
using App.Services.Domain.DBContext;
using App.Services.Domain.Models;
using App.Services.Domain.Repository;
using App.Services.Domain.Repository.Interfaces;
using App.Services.Domain.UnitOfWork;

namespace App.Services.Controllers
{
     [RoutePrefix("api/Employees")]
    
    public class EmployeesController : ApiController
    {
        private readonly EmployeeBussinessManger<EmployeeRepository> _employeeBussinessManger;
        private UnitOfWork<DbContext> _uow;
        public EmployeesController()
        {
            _uow = new UnitOfWork<DbContext>();
            _employeeBussinessManger = new
                EmployeeBussinessManger<EmployeeRepository>(_uow);
        }
        // GET: api/Departments
        public IEnumerable<Employee> Get()
        {
            return _employeeBussinessManger.Get();
        }

        // GET: api/Departments/5
        public Employee Get(int id)
        {
            return _employeeBussinessManger.GetById(id);
        }

        // POST: api/Departments
        public Employee Post([FromBody]Employee employee)
        {
            return _employeeBussinessManger.Save(employee);

        }

        // PUT: api/Departments/5
        public Employee Put([FromBody] Employee employee)
        {
            return _employeeBussinessManger.Update(employee);
        }

        // DELETE: api/Departments/5
        public Employee Delete(int id)
        {
            return _employeeBussinessManger.Delete(id);
        }

    }
}