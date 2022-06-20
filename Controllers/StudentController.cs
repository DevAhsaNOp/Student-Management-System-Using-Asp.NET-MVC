using System;
using System.Net;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Student_Management_System.Context;

namespace Student_Management_System.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        studentmanagementsystem_Entities1 dbObj = new studentmanagementsystem_Entities1();

        public ActionResult Student(student obj)
        {
            var classList = dbObj.classes.ToList();
            ViewBag.id = new MultiSelectList(classList, "id", "name", "section");

            return View(obj);
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                // return a bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = dbObj.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult AddStudent(student model)
        {
            student std = new student();
            std.std_id = model.std_id;
            std.std_Fname = model.std_Fname;
            std.std_Lname = model.std_Lname;
            std.std_Fathername = model.std_Fathername;
            std.std_Mothername = model.std_Mothername;
            std.std_gender = model.std_gender;
            std.std_phone = model.std_phone;
            std.std_address = model.std_address;
            std.std_email = model.std_email;
            std.std_nationality = model.std_nationality;
            std.std_dob = model.std_dob;
            std.std_doa = model.std_doa;
            std.std_age = model.std_age;
            std.std_religion = model.std_religion;
            std.std_class_id = model.std_class_id;

            var classList = dbObj.classes.ToList();
            ViewBag.id = new MultiSelectList(classList, "id", "name", "section");
            if (model.std_id == 0)
            {
                dbObj.students.Add(std);
                dbObj.SaveChanges();
            }
            else
            {
                dbObj.Entry(std).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();
            }

            ModelState.Clear();

            return RedirectToAction("StudentList");
        }

        public ActionResult StudentList(string searchBy, string searchValue)
        {
            List<JoinClass> jc = new List<JoinClass>();
            string constrg = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=studentmanagementsystem_;Persist Security Info=True;User ID=studentmanagementsystem_;Password=student123";
            SqlConnection sql = new SqlConnection(constrg);
            string sqlqu = "select student.std_id,student.std_Fname,student.std_Lname,student.std_Fathername,student.std_Mothername,student.std_gender,student.std_phone,student.std_address" +
                ", student.std_email,student.std_nationality,student.std_dob,student.std_doa,student.std_age,student.std_religion,student.std_class_id,class.name,class.section from " +
                "student inner join class on student.std_class_id=class.id";
            SqlCommand sqlComm = new SqlCommand(sqlqu, sql);
            SqlDataAdapter sda = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                JoinClass jcobj = new JoinClass();
                jcobj.std_id = (int)dr["std_id"];
                jcobj.std_Fname = dr["std_Fname"].ToString();
                jcobj.std_Lname = dr["std_Lname"].ToString();
                jcobj.std_Fathername = dr["std_Fathername"].ToString();
                jcobj.std_Mothername = dr["std_Mothername"].ToString();
                jcobj.std_gender = dr["std_gender"].ToString();
                jcobj.std_phone = dr["std_phone"].ToString();
                jcobj.std_address = dr["std_address"].ToString();
                jcobj.std_email = dr["std_email"].ToString();
                jcobj.std_nationality = dr["std_nationality"].ToString();
                jcobj.std_dob = (DateTime)dr["std_dob"];
                jcobj.std_doa = (DateTime)dr["std_doa"];
                jcobj.std_age = (int)dr["std_age"];
                jcobj.std_religion = dr["std_religion"].ToString();
                jcobj.std_class_id = (int)dr["std_class_id"];
                jcobj.ClassName = dr["name"].ToString();
                jcobj.Section = dr["section"].ToString();
                jc.Add(jcobj);
            }

            if (string.IsNullOrEmpty(searchValue))
            {
                TempData["InfoMessage"] = "Please provide search value.";
                return View(jc);
            }
            else
            {
                if (searchBy.ToLower() == "std_fname")
                {
                    var searchByFname = jc.Where(p => p.std_Fname.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByFname);
                }
                else if (searchBy.ToLower() == "std_phone")
                {
                    var searchByPhone = jc.Where(p => p.std_phone.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByPhone);
                }
                else if (searchBy.ToLower() == "classname")
                {
                    var searchByClass = jc.Where(p => p.ClassName.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByClass);
                }
            }

            return View(jc);

            #region Unused_Code
            //List<student> stdlist = dbObj.students.ToList();
            //List<@class> clslist = dbObj.classes.ToList();
            //ViewData["jointables"] = from std in stdlist
            //                         join cls in clslist on std.std_class_id equals cls.id
            //                         into table1
            //                         from cls in table1.DefaultIfEmpty()
            //                         select new MultipleTablesJoinClass { studentlist = std, classlist = cls };
            //var res = dbObj.students.ToList();
            //return View(ViewData["jointables"]);
            #endregion
        }
    }
}