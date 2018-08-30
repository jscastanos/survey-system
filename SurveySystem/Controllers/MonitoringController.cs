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

        public ActionResult Overall()
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
                            totalSynch = db.tSurveys.Where(u => u.brgyCode.StartsWith(a.munCityCode)).GroupBy(u1 => u1.brgyCode).Select(u2 => 
                                u2.Select(u3 => u3.sampleSize).Distinct().Count()
                            ).ToList(),
                            totalNumRef = db.tBrgies.Where(v => v.munCityCode == a.munCityCode).Select( v1 => v1.numRef).Sum(),
                            brgyList = db.tBrgies.Where(b => b.munCityCode == a.munCityCode).Select(c => new { 
                                    c.brgyCode,
                                    c.brgyName,
                                    c.numRef,
                                    synchData = db.tSurveys.Where(x => x.brgyCode == c.brgyCode).Select(x1 => x1.sampleSize).Distinct().Count()

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