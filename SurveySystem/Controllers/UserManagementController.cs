using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class UserManagementController : Controller
    {

        SurveyEntities db = new SurveyEntities();

        // GET: UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActiveUsers()
        {
            var users = (from a in db.tUsers
                         join b in db.tEnumerators on a.idNo equals b.idNo
                         where a.isActive == true

                         select new
                         {
                             a.idNo,
                             person = b.lastName + ", " + b.firstName
                         });


            return Json(users, JsonRequestBehavior.AllowGet);


        }
    }
}