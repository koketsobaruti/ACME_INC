using ACME_INC_POE.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ACME_INC_POE.Controllers
{
    public class EmployeeController : Controller
    {

        #region//Registration
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        //Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(TBL_EMPLOYEE employee)
        {
            bool status = false;
            string message = "";

            //Model Validaton
            if (ModelState.IsValid)
            {
                //Check if username already exists
                var isExist = IsUsernameExist(employee.EMP_USERNAME);

                if (isExist)
                {
                    ModelState.AddModelError("UsernameExist", "Username is taken.");
                    return View(employee);
                }
                

                //Password hashing
                employee.EMP_PASSWORD = Crypto.Hash(employee.EMP_PASSWORD);
                employee.ConfirmPassword = Crypto.Hash(employee.ConfirmPassword);
                

                //Save to database
                using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
                {
                    dc.TBL_EMPLOYEE.Add(employee);
                    try
                    {
                        dc.SaveChanges();
                        message = "Registration succesfully done!";
                        status = true;
                    }
                    catch (DbEntityValidationException e)
                    {
                        Console.WriteLine(e);
                    }


                }
                
            }
            else
            {
                message = "Invalid request.";
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View(employee);
        }
        #endregion

        #region //Display data on homepage to authorised users only
        [Authorize]
        public ActionResult Homepage(string name, string username)
        {

            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            var item = (from d in dc.TBL_PRODUCTS
                        select d).ToList();
            ViewBag.Username = username;
            return View(item);
        }
        #endregion

        #region //Display sales history
        public ActionResult History()
        {
            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            var item = dc.TBL_PURCHASES.OrderBy(a => a.PRODUCT_ID).ToList();
            return View(item);
        }
        #endregion

        #region //Display data for each category
        public ActionResult Warm_Wear()
        {
            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            var item = dc.TBL_PRODUCTS.Where(a => a.PRODUCT_TYPE == "Coat" || a.PRODUCT_TYPE == "Sweater").ToList();
            return View(item);
        }

        public ActionResult Tops()
        {
            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            var item = dc.TBL_PRODUCTS.Where(a => a.PRODUCT_TYPE == "Tops").ToList();
            return View(item);
        }

        public ActionResult Shoes()
        {
            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            var item = dc.TBL_PRODUCTS.Where(a => a.PRODUCT_TYPE == "Heels" || a.PRODUCT_TYPE == "Sneakers").ToList();
            return View(item);
        }

        public ActionResult Dresses()
        {
            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            var item = dc.TBL_PRODUCTS.Where(a => a.PRODUCT_TYPE == "Dress").ToList();
            return View(item);
        }
        #endregion

        #region//display product
        [HttpPost]
        public ActionResult ViewProduct(int id)
        {
            //string prodName = name;
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_PRODUCTS.Where(a => a.PRODUCT_ID == id).FirstOrDefault();
                Products p = new Products();
                p.ProductId = v.PRODUCT_ID;
                p.ProductName = v.PRODUCT_NAME;
                p.ProductType = v.PRODUCT_TYPE;
                p.Price = v.PRODUCT_PRICE;
                p.Image = v.PRODUCT_IMAGE;
                p.Description = v.PRODUCT_DESCRIPTION;
                return View(p);
            }
        }
        #endregion

        #region//Add logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            //Sign out the user once they are authenticated
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Employee");
        }
        #endregion

        //Check if the username exists in the database
        [NonAction]
        public bool IsUsernameExist(string userID)
        {
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_EMPLOYEE.Where(a => a.EMP_USERNAME == userID).FirstOrDefault();
                //IF IT IS EQUALS TO NULL THEN THE RESULT IS FALSE ELSE IT IS TRUE
                //return v == null ? false : true;
                //if it is not equals to null then do not return. Return if it is true
                return v != null;
            }
        }
    }

}