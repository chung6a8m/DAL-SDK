﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using App.Services.Domain.BussinessMangers.Classes;
using App.Services.Domain.DBContext;
using App.Services.Domain.Models;
using App.Services.Domain.Repository.Interfaces;
using App.Services.Domain.UnitOfWork;
using EmpPayroll.Services.Domain.Repository.Classes;

namespace EmpPayroll.Services.Controllers
{
    public class DepartmentsController : ApiController
    {
        private readonly DepartmentBussinessManger<DepartmentRepository> _departmentBussinessManger;

        public DepartmentsController()
        {
            var uow = new UnitOfWork<DbContext>();
            _departmentBussinessManger= new
                DepartmentBussinessManger<DepartmentRepository>(uow);
        }

        // GET: api/Departments
         [EnableQuery]
        public IQueryable<Department> Get()
        {
            return _departmentBussinessManger.Get();
        }

        // GET: api/Departments/5
        public Department Get(int id)
        {
            return _departmentBussinessManger.GetById(id);
        }

        // POST: api/Departments
        public Department Post([FromBody]Department department)
        {
            return _departmentBussinessManger.Save(department);

        }

        // PUT: api/Departments/5
        public Department Put( [FromBody] Department department)
        {
          return _departmentBussinessManger.Update(department);
        }

        // DELETE: api/Departments/5
        public Department Delete(int id)
        {
           return _departmentBussinessManger.Delete(id);
        }
    }
}
