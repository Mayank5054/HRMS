using HRMS.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class DirectorController : Controller
    {
        MayankEntities _db;
        public DirectorController()
        {
            _db = new MayankEntities();
        }
        // GET: Director
        public ActionResult AddEmployee()
        {
           List<SelectListItem> role = _db.Roles.Select(
               x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.RoleId.ToString()
               }).ToList();

            List<SelectListItem> reportingPersons= _db.Employees.Where(x=> (x.DepartmentId == 2 || x.DepartmentId == 1 )).Select(
    x => new SelectListItem
    {
        Text = x.FirstName +" " +x.LastName,
        Value = x.EmployeeId.ToString()
    }).ToList();
            ViewBag.roles = role;
            ViewBag.reportingPersons = reportingPersons;
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee _emp)
        {
            _emp.EmployeeCode = "SIT-433";
            _db.Employees.Add( _emp );
           _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public ActionResult AllEmployees()
        {
            List<Employee> _emp = _db.Employees
                .Include("Employee2")
                .ToList();
            return View(_emp);
        }

    }
}