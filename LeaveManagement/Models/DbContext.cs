using System.Data.SqlClient;


namespace LeaveManagement.Models
{
    public class DbContext
    {
        string constr = "Data Source=.;Initial Catalog=leaveManagement;User ID=sa;Password=Password$2";
        string eid="";
        string ename = "";
        string uname = "";

        //this method is used to login validation of admin panel. 
        public bool validateLogin(string UserName, string Password)
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_adminLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : "+e);
                return false;
            }



        }

        //this method is used to login validation of employees. 
        public int validateEmpLogin(string UserName, string Password,string EmpType)
        {
            try
            {

                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_empLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@EmpType", EmpType);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string emptype = dr.GetValue(13).ToString();

                    eid = dr.GetValue(1).ToString();
                    ename = dr.GetValue(5).ToString();

                    if (emptype == "HR" || emptype == "hr")
                    {
                        return 1;
                    }
                    else if (emptype == "Employee" || emptype == "Employee")
                    {
                        return 2;
                    }
                }
                else
                {
                    return 0;
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
            }
            return 0;
        }

        //by this method we can return empid.
        public string getEmpId()
        {
            return eid;
        }
        //by this method we can return empname.
        public string getEmpName()
        {
            return ename;
        }

        // by this method we can retun empid of last employee who added as a new employee.
        public int getId()
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from employees order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int id = Convert.ToInt32(dr.GetValue(0).ToString());
                uname = dr.GetValue(3).ToString();
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
            }

            return 0; 
        }
        //this method is to add employees in database. it is used by admin controller in create function
        public bool addEmployee(Employees emp)
        {

            try
            {
                int id = getId();
                int newid = id + 1;


                string empid = "Emp00" + newid;
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empid);
                cmd.Parameters.AddWithValue("@UserName", emp.UserName);
                cmd.Parameters.AddWithValue("@Password", emp.Password);
                cmd.Parameters.AddWithValue("@Confirmpwd", emp.Confirmpwd);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Address", emp.Address);
                cmd.Parameters.AddWithValue("@MobileNo", emp.MobileNo);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@DOB", emp.DOB);
                cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                cmd.Parameters.AddWithValue("@Qualification", emp.Qualification);
                cmd.Parameters.AddWithValue("@Designation", emp.Designation);
                cmd.Parameters.AddWithValue("@EmpType", emp.EmpType);
                cmd.Parameters.AddWithValue("@DOJ", emp.DOJ);

                int i = cmd.ExecuteNonQuery();


                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
                return false;
            }



        }
        // this method is used to admin controller to display all employee record.
        public List<Employees> getEmployee()
        {
            List<Employees> list = new List<Employees>();
            try
            {
               
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employees obj = new Employees();
                    obj.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                    obj.EmpId = dr.GetValue(1).ToString();
                    obj.UserName = dr.GetValue(2).ToString();
                    obj.Password = dr.GetValue(3).ToString();
                    obj.Confirmpwd = dr.GetValue(4).ToString();
                    obj.EmpName = dr.GetValue(5).ToString();
                    obj.Address = dr.GetValue(6).ToString();
                    obj.MobileNo = dr.GetValue(7).ToString();
                    obj.Email = dr.GetValue(8).ToString();
                    obj.DOB = dr.GetValue(9).ToString();
                    obj.Gender = dr.GetValue(10).ToString();
                    obj.Qualification = dr.GetValue(11).ToString();
                    obj.Designation = dr.GetValue(12).ToString();
                    obj.EmpType = dr.GetValue(13).ToString();
                    obj.DOJ = dr.GetValue(14).ToString();

                    list.Add(obj);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
            }
            return list;

        }
        // by this method we can access selected record by username.
        public List<Employees> getEmployeeByUname(string UserName)
        {
            List<Employees> list = new List<Employees>();

            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getEmpByUname", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Employees obj = new Employees();
                    obj.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                    obj.EmpId = dr.GetValue(1).ToString();
                    obj.UserName = dr.GetValue(2).ToString();
                    obj.Password = dr.GetValue(3).ToString();
                    obj.Confirmpwd = dr.GetValue(4).ToString();
                    obj.EmpName = dr.GetValue(5).ToString();
                    obj.Address = dr.GetValue(6).ToString();
                    obj.MobileNo = dr.GetValue(7).ToString();
                    obj.Email = dr.GetValue(8).ToString();
                    obj.DOB = dr.GetValue(9).ToString();
                    obj.Gender = dr.GetValue(10).ToString();
                    obj.Qualification = dr.GetValue(11).ToString();
                    obj.Designation = dr.GetValue(12).ToString();
                    obj.EmpType = dr.GetValue(13).ToString();
                    obj.DOJ = dr.GetValue(14).ToString();

                    list.Add(obj);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
            }
            return list;

        }

        //by this method we can check if employee already applied for leave then show error
        public string checkLeaveValidation(string empid)
        {
            string hrap = "";
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("spcheckLvalidate", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empid);


                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    hrap = dr.GetValue(8).ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
            }
            return hrap;
        }
        //by this method store data in a database which employee apply for leave from emp login.
        public int addLeaveDetails(string empid,string empname,string leavetype,string startdate,string enddate,string reasons)
        {
            try
            {
                string hrap = checkLeaveValidation(empid);

                if (hrap == "Pending")
                {
                    return 2;
                }
                else
                {

                    SqlConnection con = new SqlConnection(constr);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spEmpLeave", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpId", empid);
                    cmd.Parameters.AddWithValue("@EmpName", empname);
                    cmd.Parameters.AddWithValue("@leaveType", leavetype);
                    cmd.Parameters.AddWithValue("@StartDate", startdate);
                    cmd.Parameters.AddWithValue("@EndDate", enddate);
                    cmd.Parameters.AddWithValue("@Reasons", reasons);

                    int i = cmd.ExecuteNonQuery();


                    if (i > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                    con.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
                return 0;
            }
            
        }
        // by this method we can access leave status of employee in a employee login.
        public List<EmpLeave> getLeaveStatus(string empid)
        {
            List<EmpLeave> list = new List<EmpLeave>();
            try
            {
                
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_checkstaus", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empid);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpLeave obj = new EmpLeave();
                    obj.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                    obj.EmpId = dr.GetValue(1).ToString();
                    obj.EmpName = dr.GetValue(2).ToString();
                    obj.Date = dr.GetValue(3).ToString();
                    obj.LeaveType = dr.GetValue(4).ToString();
                    obj.StartDate = dr.GetValue(5).ToString();
                    obj.EndDate = dr.GetValue(6).ToString();
                    obj.Reasons = dr.GetValue(7).ToString();
                    obj.HRApproval = dr.GetValue(8).ToString();

                    list.Add(obj);

                }
                return list;
            }
            catch (Exception e) { return list; }

        }

        //this method is used to display all employee leave who applied.
        public List<EmpLeave> getHRDetails()
        {
            List<EmpLeave> list = new List<EmpLeave>();
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getByHr", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HRApproval", "Pending");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpLeave obj = new EmpLeave();
                    obj.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                    obj.EmpId = dr.GetValue(1).ToString();
                    obj.EmpName = dr.GetValue(2).ToString();
                    obj.Date = dr.GetValue(3).ToString();
                    obj.LeaveType = dr.GetValue(4).ToString();
                    obj.StartDate = dr.GetValue(5).ToString();
                    obj.EndDate = dr.GetValue(6).ToString();
                    obj.Reasons = dr.GetValue(7).ToString();
                    obj.HRApproval = dr.GetValue(8).ToString();

                    list.Add(obj);

                }
                return list;
            }
            catch (Exception e)
            {
                return list;
            }
        }

        //this method is used to generate the list of approved list
        public List<EmpLeave> ApprovedList()
        {
            List<EmpLeave> list = new List<EmpLeave>();
            try
            {
               
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getByHr", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HRApproval", "Approved");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpLeave obj = new EmpLeave();
                    obj.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                    obj.EmpId = dr.GetValue(1).ToString();
                    obj.EmpName = dr.GetValue(2).ToString();
                    obj.Date = dr.GetValue(3).ToString();
                    obj.LeaveType = dr.GetValue(4).ToString();
                    obj.StartDate = dr.GetValue(5).ToString();
                    obj.EndDate = dr.GetValue(6).ToString();
                    obj.Reasons = dr.GetValue(7).ToString();
                    obj.HRApproval = dr.GetValue(8).ToString();

                    list.Add(obj);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : e"+e);
            }
            return list;

        }

        //this method is used to generate the list of rejected list
        public List<EmpLeave> RejectedList()
        {
            List<EmpLeave> list = new List<EmpLeave>();
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getByHr", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HRApproval", "Rejected");
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpLeave obj = new EmpLeave();
                    obj.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                    obj.EmpId = dr.GetValue(1).ToString();
                    obj.EmpName = dr.GetValue(2).ToString();
                    obj.Date = dr.GetValue(3).ToString();
                    obj.LeaveType = dr.GetValue(4).ToString();
                    obj.StartDate = dr.GetValue(5).ToString();
                    obj.EndDate = dr.GetValue(6).ToString();
                    obj.Reasons = dr.GetValue(7).ToString();
                    obj.HRApproval = dr.GetValue(8).ToString();

                    list.Add(obj);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : "+e);
            }
            return list;

        }

        //hr will take action on leave who applied from emp login.
        public bool ActionTekenOnLeave(EmpLeave el)
        {
            try
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_actionOnLeave", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", el.Id);
                cmd.Parameters.AddWithValue("@EmpId", el.EmpId);
                cmd.Parameters.AddWithValue("@EmpName", el.EmpName);
                cmd.Parameters.AddWithValue("@Date", el.Date);
                cmd.Parameters.AddWithValue("@LeaveType", el.LeaveType);
                cmd.Parameters.AddWithValue("@StartDate", el.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", el.EndDate);
                cmd.Parameters.AddWithValue("@Reasons", el.Reasons);
                cmd.Parameters.AddWithValue("@HRApproval", el.HRApproval);

                int i = cmd.ExecuteNonQuery();


                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : "+e);
                return false;
            }

           
        }



    }
}
