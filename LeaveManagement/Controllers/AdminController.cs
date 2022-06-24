using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    public class AdminController : Controller
    {
        DbContext db = new DbContext(); //created obj of DbContext class to perform database functionality.
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        //this method is used to call login functionality of admin
        [HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
           
                try
                {
                    if (ModelState.IsValid == true)
                    {
                        bool flag = db.validateLogin(UserName, Password);
                        if (flag == true)
                        {
                            HttpContext.Session.SetString("UserName", UserName);// strore value in a session 
                            var uname = HttpContext.Session.GetString("UserName");// get value from session
                            TempData["uname"] = uname;
                            return RedirectToAction("AdminDashboard");
                        }
                        else
                        {
                            TempData["message"] = "User name and password is incorrect";
                            ModelState.Clear();
                            return RedirectToAction("Login");

                        }
                    }
                }
                catch (Exception e)
                {
                    TempData["exception"] = "" + e;
                }

                return View();
            
        }

        //this method is used to display all employee record to admin dashboard.
        public IActionResult AdminDashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login");
            }
            else
            {


                try
                {
                    DbContext dbContext = new DbContext();
                    List<Employees> list = dbContext.getEmployee();
                    return View(list);

                }
                catch (Exception e)
                {
                    return View();
                }

            }
        }

        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login");
            }
          
                return View();
        }

        //this method is used by admindashboard to add employee 
        [HttpPost]
        public IActionResult Create(Employees emp)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login");
            }
            else
            {
                try
                {
                    if (ModelState.IsValid == true)
                    {
                        DbContext dbContext = new DbContext();
                        bool flag = dbContext.addEmployee(emp);
                        if (flag == true)
                        {
                            TempData["message1"] = "Employee added successfully";
                            ModelState.Clear();
                            return RedirectToAction("AdminDashboard");
                        }

                    }
                }
                catch (Exception e)
                {
                    TempData["message1"] = "User Name Already Exists";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception e) { return RedirectToAction("Login"); }
        }
    }
}
