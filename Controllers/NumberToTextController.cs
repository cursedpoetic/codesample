using CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTest.Helpers;

namespace CodeTest.Controllers
{
    public class NumberToTextController : Controller
    {
        // GET: NumberToText
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Convert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Convert(NumberToTextModel model)
        {
            NumberToTextHelper helper = new NumberToTextHelper();

            model.NumberAsText = helper.ConvertNumberToTextString(model.InputNumber);

            return PartialView("Result", model);
        }
    }
}