using Student_Management_System.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Student_Management_System.Controllers
{
    public class FeesController : Controller
    {
        // GET: Fees
        studentmanagementsystem_Entities1 dbObj = new studentmanagementsystem_Entities1();

        public ActionResult GenerateFees(fee model)
        {
            var stdList = dbObj.students.Select(s => new
            {
                Text = s.std_Fname + " " + s.std_Lname,
                Value = s.std_id
            }).ToList();
            ViewBag.id = new SelectList(stdList, "Value", "Text");
            return View(model);
        }

        [HttpPost]
        public ActionResult Fees(fee model)
        {
            model.fees_PreviousCharges = (model.fees_PreviousCharges == null) ? 0 : model.fees_PreviousCharges;
            model.fees_PaidDate = (model.fees_PaidDate == null) ? "Not Paid" : model.fees_PaidDate;
            fee f = new fee();
            f.fees_reciept_no = model.fees_reciept_no;
            f.fees_std_id = model.fees_std_id;
            f.fees_IssueDate = model.fees_IssueDate;
            f.fees_Status = model.fees_Status;
            f.fees_PaidDate = model.fees_PaidDate;
            f.fees_Payment = model.fees_Payment;
            f.fees_PreviousCharges = model.fees_PreviousCharges;
            f.fees_TotalAmount = model.fees_TotalAmount;
            f.fees_Remarks = model.fees_Remarks;

            if (model.fees_reciept_no == 0)
            {
                dbObj.fees.Add(f);
                dbObj.SaveChanges();
            }
            else
            {
                dbObj.Entry(f).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();
            }
            ModelState.Clear();

            return RedirectToAction("FeeList", "Fees");
        }

        public ActionResult FeeList(string searchBy, string searchValue)
        {
            List<Joinfee> jc = new List<Joinfee>();
            string constrg = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=studentmanagementsystem_;Persist Security Info=True;User ID=studentmanagementsystem_;Password=student123";
            SqlConnection sql = new SqlConnection(constrg);
            string sqlqu = "select f.fees_reciept_no,f.fees_std_id,s.std_Fname,s.std_Lname,f.fees_IssueDate,f.fees_Status" +
                ", f.fees_PaidDate,f.fees_Payment,f.fees_PreviousCharges,f.fees_Remarks, f.fees_TotalAmount" +
                " from fees f inner join student s on f.fees_std_id = s.std_id";
            SqlCommand sqlComm = new SqlCommand(sqlqu, sql);
            SqlDataAdapter sda = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Joinfee jcobj = new Joinfee();
                jcobj.fees_reciept_no = (int)dr["fees_reciept_no"];
                jcobj.fees_std_id = (int)dr["fees_std_id"];
                jcobj.fees_IssueDate = dr["fees_IssueDate"].ToString();
                jcobj.fees_Status = dr["fees_Status"].ToString();
                jcobj.fees_PaidDate = dr["fees_PaidDate"].ToString();
                jcobj.fees_Payment = (double)dr["fees_Payment"];
                jcobj.fees_PreviousCharges = (double)dr["fees_PreviousCharges"];
                jcobj.fees_TotalAmount = (double)dr["fees_TotalAmount"];
                jcobj.fees_Remarks = dr["fees_Remarks"].ToString();
                jcobj.StudentFname = dr["std_Fname"].ToString();
                jcobj.StudentLname = dr["std_Lname"].ToString();
                jc.Add(jcobj);
            }

            if (string.IsNullOrEmpty(searchValue))
            {
                TempData["InfoMessage"] = "Please provide search value.";
                return View(jc);
            }
            else
            {
                if (searchBy.ToLower() == "studentfname")
                {
                    var searchByFname = jc.Where(p => p.StudentFname.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByFname);
                }
                else if (searchBy.ToLower() == "studentlname")
                {
                    var searchByPhone = jc.Where(p => p.StudentLname.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByPhone);
                }
            }

            return View(jc);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // return a bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fee cls = dbObj.fees.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }
    }
}