using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewBeanShop.Models;

namespace NewBeanShop.Controllers
{
    public class DataBaseController : Controller
    {

        [Authorize]
        public ActionResult EditBean(int ID)
        {
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();

            Item found = ORM.Items.Find(ID);

            return View(found);

        }


        [Authorize]
        public ActionResult ConfirmEdit(Item updatedBean)
        {
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();

            Item oldBean = ORM.Items.Find(updatedBean.ItemID);

            oldBean.Name = updatedBean.Name;
            oldBean.Description = updatedBean.Description;
            oldBean.Qauntity = updatedBean.Qauntity;
            oldBean.Price = updatedBean.Price;

            ORM.Entry(oldBean).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("Beans", "Home");
        }




        public ActionResult DeleteBean(int ID)
        {
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();

            Item found = ORM.Items.Find(ID);

            ORM.Items.Remove(found);
            ORM.SaveChanges();

            return RedirectToAction("Beans", "Home");

        }



        public ActionResult AddBean()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBean(Item newBean)
        {
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();



            ORM.Items.Add(newBean);
            ORM.SaveChanges();


            return RedirectToAction("Beans", "Home");
        }

        


    }
}