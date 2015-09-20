using AmazonSearch.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmazonSearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.CurrencyModel currencyModel = new Models.CurrencyModel();
            currencyModel.currencyCodes = Models.CurrencyService.GetCurrencyCodes();
            currencyModel.baseCurrency = Globals.BASE_CURRENCY;
            currencyModel.selectedCurrency = currencyModel.baseCurrency;

            return View(currencyModel);
        }

        [HttpPost]
        public ActionResult Index(string search, int pageNum = 1)
        {
            ProductContainer data = ProductContainer.getProducts(search, pageNum);
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About page";

            return View();
        }
       

        [HttpPost]
        public JsonResult GetSearchResults()
        {
            string errorMsg = "";
            Models.ProductContainer resultPageModel = new Models.ProductContainer();

            try
            {
                int page = 0;
                Int32.TryParse(Request.Form["page"], out page);
                string searchValue = Request.Form["search_value"];

                if (searchValue != null && searchValue.Length > 0 && page >= 1)
                {
                    resultPageModel = ProductContainer.getProducts(searchValue, page);
                }
            }
            catch (Exception e)
            {
                errorMsg = "An error occured: "+e.ToString();
            }

            return Json(new { pageData = resultPageModel, errorMsg = errorMsg });
        }

        [HttpPost]
        public JsonResult GetCurrencyRate()
        {
            string error = "";
            double rate = 1.0;

            try
            {
                var currency = Models.CurrencyService.ConvertStringToCurrencyConvertor(Request.Form["selected_currency"]);
                rate = Models.CurrencyService.GetRate(Globals.BASE_CURRENCY, currency);
            }
            catch
            {
                error = "No rate found";
            }

            return Json(new {rate = rate, errorMsg = error });
        }
    }
}