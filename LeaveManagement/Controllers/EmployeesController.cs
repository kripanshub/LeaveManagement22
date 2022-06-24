using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    public class EmployeesController : Controller
    {
        DbContext db = new DbContext();

     
        public IActionResult Login()
        {
            return View();
        }
       
        // by this method defined the functionality of HR Login.
        [HttpPost]
        public IActionResult Login(string UserName, string Password,string EmpType)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    int i = db.validateEmpLogin(UserName, Password,EmpType);
                  
                    HttpContext.Session.SetString("UserName", UserName);
                    HttpContext.Session.SetString("Empid", db.getEmpId());
                    HttpContext.Session.SetString("Empname", db.getEmpName());

                    if (i==1)
                    {
                        // TempData["message"] = "success";
                        return RedirectToAction("HRDashboard");
                    }
                    else if (i == 2)
                    {
                        // TempData["message"] = "success";
                        return RedirectToAction("EmpDashboard");
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
        //create view of employee dashboard and display his record on dashboard.
        public IActionResult EmpDashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login");
            }
            else
            {
                try
                {

                   
                    var uname = HttpContext.Session.GetString("UserName");

                    // string uname = TempData["UserName"].ToString();
                    List<Employees> list = db.getEmployeeByUname(uname);
                    return View(list);
                }
                catch (Exception) { return View(); }
            }
            
        }
        // create view of hr dashboard and display all employee record who applied for leave from emp login.
        public IActionResult HRDashboard()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    
                    List<EmpLeave> list = db.getHRDetails();
                    return View(list);
                }
            }
            catch (Exception e)
            {
                return View();

            }
        }

        //this is used to display view of approved list
        public IActionResult ApprovedList()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                   
                    List<EmpLeave> list = db.ApprovedList();
                    return View(list);
                }
            }
            catch(Exception e)
            {
                return View();
            }
        }

        //this is used to display view of rejected list
        public IActionResult RejectedList()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    
                    List<EmpLeave> list = db.RejectedList();
                    return View(list);
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        //this method is used to generate the view of leave approval by hr
        public IActionResult LeaveApprovalByHR(int Id)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    var row = db.getHRDetails().Find(model => model.Id == Id);
                    return View(row);
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // this this is used to leave approval by hr
        [HttpPost]
        public IActionResult LeaveApprovalByHR(EmpLeave el)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    if (ModelState.IsValid == true)
                    {
                        bool flag = db.ActionTekenOnLeave(el);
                        //TempData["message5"] = "Leave sanction by hr";
                        ModelState.Clear();
                        return RedirectToAction("HRDashboard");
                    }
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
       

        // by this method view is generated for apply leave
        public IActionResult ApplyForLeave()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
        //this method accept data to store who apply for leave from employee login
        [HttpPost]
        public IActionResult ApplyForLeave(string EmpId,string EmpName,string LeaveType,string StartDate,string EndDate,string Reasons)
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
                        int i = db.addLeaveDetails(EmpId, EmpName, LeaveType, StartDate, EndDate, Reasons);
                        if (i == 1)
                        {
                            TempData["message3"] = "Your leave applied successfully";
                            ModelState.Clear();
                            return RedirectToAction("ApplyForLeave");

                        }
                        else if (i == 2)
                        {
                            TempData["message4"] = "Your have already applied for leave";
                            ModelState.Clear();
                            return RedirectToAction("ApplyForLeave");
                        }

                    }
                }
                catch (Exception e)
                {
                    TempData["message2"] = "" + e;
                }
                return View();
            }
        }
        //by this method employee can status of applied leave.
        public IActionResult LeaveStatus()
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    
                    var empid = HttpContext.Session.GetString("Empid");

                    // string empid = TempData["Empid"].ToString();
                    List<EmpLeave> list = db.getLeaveStatus(empid);
                    return View(list);
                }
            }
            catch (Exception e)
            {
                return View();
            }

        }
        //this method is used to logout of employee or hr login
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
