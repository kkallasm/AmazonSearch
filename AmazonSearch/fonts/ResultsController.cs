using AmazonSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazonSearch.Controllers
{
    public class ResultsController : Controller
    {
        [HttpPost]
        public ActionResult Show(string search, int pageNum)
        {
            ProductContainer data = ProductContainer.getProducts(search, pageNum);
            return View("Index",data);
        }
    }
}