using Day_8.Constants;
using Day_8.Dto;
using Day_8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Day_8.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        private readonly EmployessContext _context;

        public LoggingFilter(EmployessContext context)
        {
            _context = context;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var employeId = filterContext.HttpContext.Session.GetInt32("Id");
            if (employeId != null)
            {
                var employe = _context.Employes.Find((int)employeId);
                if (employe == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Login/GuestPage");
                }
                var employeRoleDetail = new EmployeDetail(employe);


                employeRoleDetail.Roles = (from ur in _context.EmployeRoles
                                           join r in _context.Roles on ur.RoleId equals r.RoleId
                                           where ur.Id == employeId
                                           select new EmployeRoleDto
                                           {
                                               Id = ur.Id,
                                               Name = r.RoleName,
                                               Action = r.Action,
                                               Controller = r.Controller,
                                               Status = ur.Status
                                           }).ToList();


                Console.WriteLine($"(Logging Filter)Action Executing: {filterContext.ActionDescriptor.DisplayName}");
                var s = filterContext.HttpContext.Session.GetString(Session.ID);
                string s1 = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;
                string s2 = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
                string check = "0";
                foreach (var item in employeRoleDetail.Roles)
                {

                    if (item.Controller == s2 && item.Controller == s1)
                    {
                        check = "1";

                    }

                }
                while (s1 != "Login" && s2 != "Login")
                {
                    if (check == "1")
                    {
                        base.OnActionExecuting(filterContext);
                        break;
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/GuestPage");
                        break;
                    }
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                Console.WriteLine("(Logging Filter)Exception thrown");
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
