using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;

namespace SurveySystem.Controllers
{
    public class MonitoringController : Controller
    {

        SurveyEntities db = new SurveyEntities();

        // GET: Monitoring
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Individual()
        {
            return View();
        }

        public ActionResult GetEnumDetails(string idNo)
        {
            return Json(idNo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Location()
        {
            return View();
        }

        public ActionResult GetMunDetails(string munCityCode)
        {

            try{

                var data = (from a in db.tMunCities
                            where a.munCityCode == munCityCode

                            select new
                            {
                                a.munCityCode,
                                totalSynch = db.tSurveys.Where(aa => aa.brgyCode.StartsWith(munCityCode) && aa.catCode == "cat001").Select(aaa => aaa.recNo).Count(),
                                totalNumRef = db.tBrgies.Where(v => v.munCityCode == a.munCityCode).Select(v1 => v1.numRef).Sum(),
                                brgyList = db.tBrgies.Where(b => b.munCityCode == a.munCityCode).Select(c => new
                                {
                                    c.brgyCode,
                                    c.brgyName,
                                    c.numRef,
                                    synchData = db.tSurveys.Where(xx => xx.brgyCode == c.brgyCode && xx.catCode == "cat001").Select(bbb => bbb.recNo).Count()

                                })
                            }).ToList();
                
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch( Exception e){
                var data = e;

                return Json(data, JsonRequestBehavior.AllowGet);

            }

            }
    }
}