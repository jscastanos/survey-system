using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class ReportingController : Controller
    {

        SurveyEntities db = new SurveyEntities();

        // GET: Reporting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Distribution()
        {
            return View();
        }

        public ActionResult DistributionPerLGU(string munCityCode)
        {
            var data = (from a in db.tBrgies
                        join a1 in db.tAssignedPlaces on a.brgyCode equals a1.brgyCode
                        where a.munCityCode == munCityCode && a1.tag == 1

                        select new
                        {
                            a.brgyName,
                            enumList = (from b in db.tAssignedPlaces
                                        join c in db.tEnumerators on b.idNo equals c.idNo
                                        where b.brgyCode == a.brgyCode 

                                        select new
                                        {
                                            name = c.lastName + ", " + c.firstName,
                                            assignedPrk = (from d in db.tAssignedPlaces
                                                           join e in db.tPrks on d.prkCode equals e.prkCode
                                                           where d.idNo == b.idNo
                                                               
                                                           select new {
                                                               e.prkName
                                                           })

                                        })
                                        .GroupBy(w => new { w.name })
                                        .Select(v => v.FirstOrDefault())



                        }).GroupBy(x => new { x.brgyName })
                        .Select(y => y.FirstOrDefault())
                        .ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}