using ACME_INC_POE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ACME_INC_POE.Controllers
{
    public class CustomerController : Controller
    {

        #region//Registration
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


         //Register a new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "TBL_CUSTOMER_PAYMENT_DETAILS, TBL_CUSTOMER_PURCHASES")] TBL_CUSTOMER cust)
        {
            bool status = false;
            string message = "";

            if (ModelState.IsValid)
            {
                //Check if username already exists
                var isExist = IsUsernameExist(cust.CUST_USERNAME);

                if (isExist)
                {
                    ModelState.AddModelError("UsernameExist", "Username is taken.");
                    return View(cust);
                }

                //Password hashing
                cust.CUST_PASSWORD = Crypto.Hash(cust.CUST_PASSWORD);
                cust.ConfirmPassword = Crypto.Hash(cust.ConfirmPassword);

                //Save to database
                using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
                {
                    dc.TBL_CUSTOMER.Add(cust);
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
                //Send error messag if the user enteres invalid data
                message = "Invalid request.";
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View(cust);
        }
        #endregion

        #region //Display history
        public ActionResult History()
        {
            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            //get all the products and the purchase details
            var purchases = dc.TBL_PURCHASES.OrderBy(a => a.PRODUCT_ID).ToList();
            var products = dc.TBL_PRODUCTS.OrderBy(a => a.PRODUCT_ID).ToList();

            History h = new History();
            List<History> list = new List<History>();

            //loop through the number of purchases and products to find a matching id
            foreach(var pur in purchases)
            {
                foreach(var pro in products)
                {
                    //if the id's match add the items to a list of history
                    if(pur.PRODUCT_ID == pro.PRODUCT_ID)
                    {
                        h.DeliveryDate = pur.DELIVERY_DATE;
                        h.Image = pro.PRODUCT_IMAGE;
                        h.Name = pro.PRODUCT_NAME;
                        h.PurchaseDate = pur.PURCHASE_DATE;
                        h.Price = pro.PRODUCT_PRICE;
                        h.Username = pur.CUST_USERNAME;
                        h.Id = pro.PRODUCT_ID;
                        list.Add(h);
                    }
                }
                
            }
            return View(list);
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

        #region//Add logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            //Sign out the user once they are authenticated
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region //Display data on homepage to authorised users only
        [Authorize]
        public ActionResult Homepage(string username)
        {

            DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities();
            var item = (from d in dc.TBL_PRODUCTS
                        select d).ToList();
            ViewBag.Username = username;
            return View(item);
        }
        #endregion

        #region//display product
        [HttpPost]
        public ActionResult ViewProduct(int id)
        {
            using(DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
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

        #region //Display products added to the cart
        [HttpPost]
        public ActionResult ViewCart(int id)
        {
            //get the parameter passed
            int i = id;
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_PRODUCTS.Where(a => a.PRODUCT_ID == id).FirstOrDefault();
                //create new object of products to display product in the cart
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

        #region //Add a delivery address
        public ActionResult DeliveryInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeliveryInfo([Bind(Exclude = "TBL_CUSTOMER")] TBL_CUST_ADDRESSES ca, int prodId, string name) {
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                string add = ca.BuildingName + "," + ca.CUST_ADDRESS;
                //check if the user wants to save the address as default
                if (ca.Default)
                {
                    //check if the user has an address
                    var v = IsAddressExist(ca.CUST_USERNAME, add);
                    if (!v)
                    {
                        using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
                        {
                            TBL_CUST_ADDRESSES pd = new TBL_CUST_ADDRESSES();
                            ca.CUST_ADDRESS = ca.BuildingName + "," + ca.CUST_ADDRESS;
                            pd.CUST_USERNAME = ca.CUST_USERNAME;

                            dc.TBL_CUST_ADDRESSES.Add(ca);
                            try
                            {
                                dc.SaveChanges();
                                status = true;
                                return RedirectToAction("PaymentDetails", "Customer");
                            }
                            catch (DbEntityValidationException e)
                            {
                                Console.WriteLine(e);
                            }

                        }
                    }
                    
                }
                //forward to the payment details if the user doesnt want to save the address
                return RedirectToAction("PaymentDetails", "Customer");
            }
            else
            {
                message = "Values are invalid";
                
            }
            ViewBag.Username = name;
            ViewBag.ProdId = prodId;
            ViewBag.Message = message;

            ViewBag.Status = status;

            return View();
        }
        #endregion

        #region //Add payment details
        public ActionResult PaymentDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PaymentDetails([Bind(Exclude = "TBL_CUSTOMER")] TBL_CUSTOMER_PAYMENT_DETAILS details , int prodId, string name)
        {
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                //if the account is not in the database add it
                var v = IsAccountExist(name, details.CUST_CARD_NUMBER);
                if (!v)
                {
                    //Add to tbl_purchases table
                    using (DB_VC_PROG7311_188028607_Entities a = new DB_VC_PROG7311_188028607_Entities())
                    {
                        TBL_CUSTOMER_PAYMENT_DETAILS pd = new TBL_CUSTOMER_PAYMENT_DETAILS();
                        pd.CUST_USERNAME = name;
                        pd.CUST_CARD_NUMBER = details.CUST_CARD_NUMBER;
                        pd.CUST_CVV = details.CUST_CVV;
                        details.CUST_USERNAME = name;
                        a.TBL_CUSTOMER_PAYMENT_DETAILS.Add(pd);
                        try
                        {
                            a.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }

                //Add purchase to TBL_PURCHASES
                using(DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
                {
                    DateTime today = DateTime.Today;
                    TBL_PURCHASES purch = new TBL_PURCHASES();
                    purch.PRODUCT_ID = prodId;
                    purch.CUST_USERNAME = name;
                    purch.PURCHASE_DATE = today;
                    purch.DELIVERY_DATE = today.AddDays(3);

                    dc.TBL_PURCHASES.Add(purch);
                    try
                    {
                        dc.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                status = true;
            }
            else
            {
                message = "Values are invalid";

            }
            ViewBag.ProdId = prodId;
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View();
        }
        #endregion

        #region //Display payment confirmation
        [HttpPost]
        public ActionResult PaymentConfirmation(int prodId)
        {
            int i = prodId;
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_PRODUCTS.Where(a => a.PRODUCT_ID == prodId).FirstOrDefault();
                Products p = new Products();
                p.ProductName = v.PRODUCT_NAME;
                p.ProductType = v.PRODUCT_TYPE;
                p.Price = v.PRODUCT_PRICE;
                p.Image = v.PRODUCT_IMAGE;
                p.Description = v.PRODUCT_DESCRIPTION;

                return View(p);
            }
        }
        #endregion


        //Check if the username exists in the database
        [NonAction]
        public bool IsUsernameExist(string userID)
        {
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_CUSTOMER.Where(a => a.CUST_USERNAME == userID).FirstOrDefault();
                //IF IT IS EQUALS TO NULL THEN THE RESULT IS FALSE ELSE IT IS TRUE
                //return v == null ? false : true;
                //if it is not equals to null then do not return. Return if it is true
                return v != null;
            }
        }

        //check if the user has already added their address to the database
        [NonAction]
        public bool IsAddressExist(string userID, string address)
        {
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_CUST_ADDRESSES.Where(a => a.CUST_USERNAME == userID).FirstOrDefault();
                //IF IT IS EQUALS TO NULL THEN THE RESULT IS FALSE ELSE IT IS TRUE
                //return v == null ? false : true;
                //if it is not equals to null then do not return. Return if it is true
                return v != null;
            }
        }

        //check if the user has already added the card details to the database
        [NonAction]
        public bool IsAccountExist(string userID, int cardNo)
        {
            using (DB_VC_PROG7311_188028607_Entities dc = new DB_VC_PROG7311_188028607_Entities())
            {
                var v = dc.TBL_CUSTOMER_PAYMENT_DETAILS.Where(a => (a.CUST_USERNAME == userID) && (a.CUST_CARD_NUMBER == cardNo)).FirstOrDefault();
                //IF IT IS EQUALS TO NULL THEN THE RESULT IS FALSE ELSE IT IS TRUE
                //return v == null ? false : true;
                //if it is not equals to null then do not return. Return if it is true
                return v != null;
            }
        }
    }
}