using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewBeanShop.Models;

namespace NewBeanShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult Beans()
        {
            CoffeeShopDBEntities beanMaster = new CoffeeShopDBEntities();

            ViewBag.BeanList = beanMaster.Items.ToList();

            return View();
        }
    }
}