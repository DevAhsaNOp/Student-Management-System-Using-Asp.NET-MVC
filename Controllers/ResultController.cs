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
    public class ResultController : Controller
    {
        studentmanagementsystem_Entities1 dbObj = new studentmanagementsystem_Entities1();
        // GET: Result
        public ActionResult AddResult(result model)
        {
            var stdList = dbObj.students.Select(s => new
            {
                Text = s.std_Fname + " " + s.std_Lname,
                Value = s.std_id
            }).ToList();
            ViewBag.std = new SelectList(stdList, "Value", "Text");
            var classList = dbObj.classes.ToList();
            ViewBag.classname = new MultiSelectList(classList, "id", "name", "section");

            return View(model);
        }

        [HttpPost]
        public ActionResult Result(result model)
        {
            result f = new result();
            f.RES_id = model.RES_id;
            f.RES_class_id = model.RES_class_id;
            f.RES_std_id = model.RES_std_id;
            f.Exam_type = model.Exam_type;
            f.RES_maths_marks = model.RES_maths_marks;
            f.RES_englishliterature_marks = model.RES_englishliterature_marks;
            f.RES_englishgrammar_marks = model.RES_englishgrammar_marks;
            f.RES_urdu_marks = model.RES_urdu_marks;
            f.RES_islamiyat_marks = model.RES_islamiyat_marks;
            f.RES_Science_marks = model.RES_Science_marks;
            f.RES_Physics_marks = model.RES_Physics_marks;
            f.RES_Chemistry_marks = model.RES_Chemistry_marks;
            f.RES_History_marks = model.RES_History_marks;
            f.RES_Geography_marks = model.RES_Geography_marks;
            f.RES_Computer_marks = model.RES_Computer_marks;
            f.RES_activity_marks = model.RES_activity_marks;
            f.RES_obtain_marks = model.RES_obtain_marks;
            f.RES_total_marks = model.RES_total_marks;
            f.RES_percentage = model.RES_percentage;
            f.RES_grade = model.RES_grade;
            f.RES_REmarks = model.RES_REmarks;

            if (model.RES_id == 0)
            {
                dbObj.results.Add(f);
                dbObj.SaveChanges();
            }
            else
            {
                dbObj.Entry(f).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();
            }
            ModelState.Clear();

            return RedirectToAction("ResultList", "Result");
        }

        public ActionResult ResultList(string searchBy, string searchValue)
        {
            List<Joinresult> jc = new List<Joinresult>();
            string constrg = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=studentmanagementsystem_;Persist Security Info=True;User ID=studentmanagementsystem_;Password=student123";
            SqlConnection sql = new SqlConnection(constrg);
            string sqlqu = "select r.RES_id,r.RES_class_id,r.RES_std_id,r.Exam_type,r.RES_maths_marks,r.RES_englishliterature_marks,RES_englishgrammar_marks," +
                "RES_urdu_marks,RES_islamiyat_marks,RES_Science_marks,RES_Physics_marks,RES_Chemistry_marks,RES_History_marks,RES_Geography_marks,RES_Computer_marks" +
                ",RES_activity_marks,RES_obtain_marks,RES_total_marks,RES_percentage,RES_grade,RES_REmarks,s.std_Fname,s.std_Lname,c.name,c.section from result r inner join " +
                "student s on r.RES_std_id = s.std_id inner join class c on r.RES_class_id = c.id";
            SqlCommand sqlComm = new SqlCommand(sqlqu, sql);
            SqlDataAdapter sda = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Joinresult jcobj = new Joinresult();
                jcobj.RES_id = (int)dr["RES_id"];
                jcobj.RES_class_id = (int)dr["RES_class_id"];
                jcobj.RES_std_id = (int)dr["RES_std_id"];
                jcobj.Exam_type = dr["Exam_type"].ToString();
                jcobj.RES_maths_marks = (int)dr["RES_maths_marks"];
                jcobj.RES_englishliterature_marks = (int)dr["RES_englishliterature_marks"];
                jcobj.RES_englishgrammar_marks = (int)dr["RES_englishgrammar_marks"];
                jcobj.RES_urdu_marks = (int)dr["RES_urdu_marks"];
                jcobj.RES_islamiyat_marks = (int)dr["RES_islamiyat_marks"];
                jcobj.RES_Science_marks = (int)dr["RES_Science_marks"];
                jcobj.RES_Physics_marks = (int)dr["RES_Physics_marks"];
                jcobj.RES_Chemistry_marks = (int)dr["RES_Chemistry_marks"];
                jcobj.RES_History_marks = (int)dr["RES_History_marks"];
                jcobj.RES_Geography_marks = (int)dr["RES_Geography_marks"];
                jcobj.RES_Computer_marks = (int)dr["RES_Computer_marks"];
                jcobj.RES_activity_marks = (int)dr["RES_activity_marks"];
                jcobj.RES_obtain_marks = (int)dr["RES_obtain_marks"];
                jcobj.RES_total_marks = (int)dr["RES_total_marks"];
                jcobj.RES_percentage = (double)dr["RES_percentage"];
                jcobj.RES_grade = dr["RES_grade"].ToString();
                jcobj.RES_REmarks = dr["RES_REmarks"].ToString();
                jcobj.ClassName = dr["name"].ToString();
                jcobj.ClassSection = dr["section"].ToString();
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
                    var searchByLname = jc.Where(p => p.StudentLname.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByLname);
                }
                else if (searchBy.ToLower() == "classname")
                {
                    var searchByClass = jc.Where(p => p.ClassName.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByClass);
                }
                else if (searchBy.ToLower() == "classsection")
                {
                    var searchByCSection = jc.Where(p => p.ClassSection.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByCSection);
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
            result cls = dbObj.results.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }

        public void CalCulate(result model)
        {
            double percentage=  (model.RES_obtain_marks / model.RES_total_marks) * 100;
            model.RES_percentage = percentage;
        }
    }
}