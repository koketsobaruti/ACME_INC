using ACME_INC_POE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace ACME_INC_POE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DB_VC_PROG7311_188028607_Entities d = new DB_VC_PROG7311_188028607_Entities();
            d.Database.Connection.Open();
            //try
            //{
                DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
                var item = (from x in dc.TBL_PRODUCTS
                            select x).Take(3);
            /*}
            catch (Exception e)
            {
                d.Database.Connection.Close();
            }*/
            d.Database.Connection.Close();
            return View(item);
        }

        /* [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult Index(TBL_PRODUCTS prod, string button)
         {
             if(button == "view")
             {
                 return RedirectToAction("Dresses", "Home");
             }
             return View();
         }*/

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

        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        //take credentials provided by the user and if they are valid add to the home controller
        [HttpPost]
        public ActionResult Login(UserLogin login, string ReturnUrl)
        {
            string username = "";
            string message = "";
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                #region //Check if the username exists in either the Customer and Employee table
                var isExistEmp = IsEmployeeUsernameExist(login.Username);
                var isExistCust = IsCustomerUsernameExist(login.Username);
                Console.WriteLine("Customer state: " + isExistCust);
                #endregion

                #region//if username is found 
                if (isExistCust)
                {
                    var vc = dc.TBL_CUSTOMER.Where(a => a.CUST_USERNAME == login.Username).FirstOrDefault();
                    if (vc != null)
                    {
                        //compare passwords
                        if (string.Compare(Crypto.Hash(login.Password), vc.CUST_PASSWORD) == 0)
                        {
                            //check if the user has selected the 'Remember Me' option or not
                            int timeout = login.RememberMe ? 525600 : 20; //525600 min = 1 yr
                            var ticket = new FormsAuthenticationTicket(login.Username, login.RememberMe, timeout);
                            //encrypt it
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            //assign a coockie
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            //add security
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                Session["UserId"] = login.Username;
                                username = login.Username;
                                return RedirectToAction("Homepage", "Customer");
                            }

                        }
                        else
                        {
                            message = "Invalid credentials provided";
                        }

                    }
                    else
                    {
                        message = "Enter valid credentials";
                    }
                }
                #region //Check if the user is an employee
                else if (isExistEmp)
                {
                    var ve = dc.TBL_EMPLOYEE.Where(a => a.EMP_USERNAME == login.Username).FirstOrDefault();
                    if (ve != null)
                    {
                        //compare passwords
                        if (string.Compare(Crypto.Hash(login.Password), ve.EMP_PASSWORD) == 0)
                        {
                            //check if the user has selected the 'Remember Me' option or not
                            int timeout = login.RememberMe ? 525600 : 20; //525600 min = 1 yr
                            var ticket = new FormsAuthenticationTicket(login.Username, login.RememberMe, timeout);
                            //encrypt it
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            //assign a coockie
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            //add security
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                username = login.Username;
                                return RedirectToAction("Index", "Home");
                            }

                        }

                    }
                    else
                    {
                        message = "Enter valid credentials";
                    }
                }
                #endregion
                else
                {
                    message = "Invalid credentials provided";
                }
                #endregion
                //check if username exists in the database
                /*var v = dc.TBL_EMPLOYEE.Where(a => a.EMP_USERNAME == login.Username).FirstOrDefault();
                if (v != null)
                {
                    //compare passwords
                    if (string.Compare(Crypto.Hash(login.Password), v.EMP_PASSWORD) == 0)
                    {
                        //check if the user has selected the 'Remember Me' option or not
                        int timeout = login.RememberMe ? 525600 : 20; //525600 min = 1 yr
                        var ticket = new FormsAuthenticationTicket(login.Username, login.RememberMe, timeout);
                        //encrypt it
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        //assign a coockie
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        //add security
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }

                }
                else
                {
                    message = "Invalid credentials provided";
                }*/
            }
            ViewBag.Username = username;
            ViewBag.Message = message;
            return View(username);

        }

        [NonAction]
        public bool IsEmployeeUsernameExist(string userID)
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

        [NonAction]
        public bool IsCustomerUsernameExist(string userID)
        {
            DB_VC_PROG7311_188028607_Entities d = new DB_VC_PROG7311_188028607_Entities();
            d.Database.Connection.Open();

            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_CUSTOMER.Where(a => a.CUST_USERNAME == userID).FirstOrDefault();
                //IF IT IS EQUALS TO NULL THEN THE RESULT IS FALSE ELSE IT IS TRUE
                //return v == null ? false : true;
                //if it is not equals to null then do not return. Return if it is true
                return v != null;
            }
        }
    }
}