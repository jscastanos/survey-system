using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;
using System.Web.Script.Serialization;
using System.Data.Entity;

namespace SurveySystem.Controllers
{
    public class UtilitiesController : Controller
    {

        SurveyEntities db = new SurveyEntities();

        // GET: Utilities
        public ActionResult Index()
        {
            return View();
        }

        // BRGY UTIL

        public ActionResult BrgyRef()
        {
            return View();
        }

        public ActionResult GetBrgyList(string munCode)
        {
            var brgy = (from a in db.tBrgies
                        where a.munCityCode == munCode

                        select new
                        {
                            a.munCityCode,
                            a.brgyCode,
                            a.brgyName,
                            a.numRef
                        }).ToList();

            return Json(brgy, JsonRequestBehavior.AllowGet);
        }

        public string addBrgyDetails(string data)
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            var newData = s.Deserialize<tBrgy>(data);

            db.tBrgies.Add(newData);
            db.SaveChanges();

            return "success";
        }

        public string updateBrgyDetails(string data)
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            var newData = s.Deserialize<tBrgy>(data);

            db.Entry(newData).State = EntityState.Modified;
            db.SaveChanges();


            return "success";
        }


        // ASSIGN PLACE UTIL

        public ActionResult AssignPlace()
        {
            return View();
        }


        public ActionResult GetUser()
        {

            var data = (from a in db.tUsers
                        join b in db.tEnumerators on a.idNo equals b.idNo

                        select new
                        {
                            a.idNo,
                            enumerator = b.lastName + ", " + b.firstName
                        });

            return Json(data, JsonRequestBehavior.AllowGet);


        }

        public ActionResult GetAssignList(string idNo)
        {

            var data = (from a in db.tMunCities
                        select new
                        {

                            a.munCityCode,
                            a.munCityName,
                            assignBrgy = (
                                from b in db.tAssignedPlaces
                                join c in db.tBrgies on b.brgyCode equals c.brgyCode
                                where b.idNo == idNo && c.munCityCode == a.munCityCode

                                select new
                                {

                                    c.brgyCode,
                                    brgyName = db.tBrgies.Where(x => x.brgyCode == c.brgyCode).Select(y => y.brgyName).FirstOrDefault(),
                                    b.numRef,
                                    synchData = db.tSurveys.Where(x => x.idNo == idNo && x.brgyCode == b.brgyCode).Select(y => y.sampleSize).Distinct().Count()



                                })




                        }).GroupBy(x => new { x.munCityCode })
                        .Select(y => y.FirstOrDefault())
                        .ToList();



            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public string SaveAssignPlace(string data)
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
            var newData = s.Deserialize<tAssignedPlace>(data);

            db.tAssignedPlaces.Add(newData);
            db.SaveChanges();

            return "success";

        }
    }
}