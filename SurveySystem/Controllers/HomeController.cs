using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveySystem.Models;
using Newtonsoft.Json;

namespace SurveySystem.Controllers
{
    public class HomeController : Controller
    {

        SurveyEntities db = new SurveyEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult getUser(string username, string password, int num)
        {
            return Json(db.vAssignedLocs.Where(x => x.username == username && x.password == password && x.tag == num), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getMunCity()
        {
            return Json(db.tMunCities.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getBrgy()
        {
            return Json(db.tBrgies.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getData(String idNo, int num)
        {
            return Json(db.vAssignedLocs.Where(x => x.idNo == idNo && x.tag == num), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllCategory()
        {
            return Json(db.tCategories.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllChoices()
        {
            return Json(db.tChoices.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getSurveyData()
        {

            var data =
             (from a in db.tCategories

              select new
              {

                  a.catCode,
                  a.batchId,
                  a.catName,
                  a.catSortNum,
                  a.isActive,
                  choice = (
                  from c in db.tChoices
                  where c.catCode == a.catCode

                  select new
                  {
                      c.quesCode,
                      c.quesName,
                      c.value
                  })


              }).OrderBy(a => a.catCode).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);


        }

        public ActionResult getSurveyDatas()
        {

            var data =
             (from a in db.tCategoryByDists

              select new
              {

                  a.catCode,
                  a.district,
                  a.catName,
                  a.catSortNum,
                  choice = (
                  from c in db.tChoices
                  where c.catCode == a.catCode

                  select new
                  {
                      c.quesCode,
                      c.quesName,
                      c.value
                  })


              }).OrderBy(a => a.catSortNum).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);


        }

        public ActionResult getSurveyDatass()
        {

            var data =
             (from a in db.tCategoryByMunCities

              select new
              {

                  a.catCode,
                  a.munCityCode,
                  a.catName,
                  a.catSortNum,
                  choice = (
                  from c in db.tChoices
                  where c.catCode == a.catCode

                  select new
                  {
                      c.quesCode,
                      c.quesName,
                      c.value
                  })


              }).OrderBy(a => a.catSortNum).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);


        }

        //public ActionResult synchronizeData(IEnumerable<tSurvey> survey)
        public ActionResult synchronizeData(string survey)
        {
            try
            {
                List<tSurvey> obj = new List<tSurvey>();


                obj = JsonConvert.DeserializeObject<List<tSurvey>>(survey);

                foreach (var s in obj)
                {
                    db.tSurveys.Add(s);
                    db.SaveChanges();
                }
                return Json("Synchronized Success!", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getDatas()
        {
            var d = (from s in db.tSurveys
                     select new
                     {
                         s.idNo,
                         s.brgyCode,
                         s.catCode,
                         s.value,
                         s.sampleSize,
                         s.tag,
                         s.wave
                     }).ToList();

            return Json(d, JsonRequestBehavior.AllowGet);
        }
    }
}