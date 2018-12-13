using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NewBeanShop.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace NewBeanShop.Controllers
{
    public class AccountController : Controller
    {

        //set up instance of usermanager
        public UserManager<IdentityUser> UserManager =>
            HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();





        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerUser)
        {
            if (ModelState.IsValid)
            {

                var IdentityResult = await UserManager.CreateAsync(new IdentityUser(registerUser.UserName), registerUser.Password);

                if (IdentityResult.Succeeded)
                {

                    //if the model is valid and it passes identity, we add the user to our User table as well...

                    CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();

                    //create the user based on our user model and assign the properties from the identity user to it...
                    var newUser = new User();

                    newUser.FirstName = registerUser.FirstName;
                    newUser.LastName = registerUser.LastName;
                    newUser.Email = registerUser.UserName;
                    newUser.Address = registerUser.Address;

                    //add the user to the ORM and save changes...
                    ORM.Users.Add(newUser);
                    ORM.SaveChanges();

                    //return home view
                    return RedirectToAction("Index", "Home");


                }

                ModelState.AddModelError("", IdentityResult.Errors.FirstOrDefault());

                return View();

            }

            return View();

        }




        
        public ActionResult Login ()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {

                var authenticationManager = HttpContext.GetOwinContext().Authentication;

                IdentityUser user = UserManager.Find(loginModel.UserName, loginModel.Password);

                if (user != null)
                {

                    var ident = UserManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created.
                    authenticationManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Beans", "Home");

                }




            }

            ModelState.AddModelError("", "Invalid login");


            return View(loginModel);
        }




        [Authorize]
        public ActionResult Summary()
        {
            //grab the currently logged in in user's email address
            string userEmail = User.Identity.Name;

            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();


            //pull the user from the ORM based on email
            User currentUser = ORM.Users.FirstOrDefault(i => i.Email == userEmail);


            //pass that user to the view
            return View(currentUser);
        }




    }
}