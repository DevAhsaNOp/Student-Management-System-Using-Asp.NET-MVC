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
    public class ClassRoutineController : Controller
    {
        studentmanagementsystem_Entities1 dbObj = new studentmanagementsystem_Entities1();

        // GET: ClassRoutine
        public ActionResult AddTimeTable(class_routine model)
        {
            var classList = dbObj.classes.ToList();
            ViewBag.id = new MultiSelectList(classList, "id", "name", "section");

            return View(model);
        }

        [HttpPost]
        public ActionResult TimeTable(class_routine model)
        {
            model.IsMaths = (model.IsMaths == null) ? false : model.IsMaths;
            model.IsMathsSlot = (model.IsMathsSlot == null) ? "Not Applicable" : model.IsMathsSlot;
            model.IsEnglishLiterature = (model.IsEnglishLiterature == null) ? false : model.IsEnglishLiterature;
            model.IsEnglishLiteratureSlot = model.IsEnglishLiteratureSlot ?? "Not Applicable";
            model.IsEnglishGrammar = (model.IsEnglishGrammar == null) ? false : model.IsEnglishGrammar;
            model.IsEnglishGrammarSlot = model.IsEnglishGrammarSlot ?? "Not Applicable";
            model.IsUrdu = (model.IsUrdu == null) ? false : model.IsUrdu;
            model.IsUrduSlot = model.IsUrduSlot ?? "Not Applicable";
            model.IsIslamiyat = (model.IsIslamiyat == null) ? false : model.IsIslamiyat;
            model.IsIslamiyatSlot = model.IsIslamiyatSlot ?? "Not Applicable";
            model.IsScience = (model.IsScience == null) ? false : model.IsScience;
            model.IsScienceSlot = model.IsScienceSlot ?? "Not Applicable";
            model.IsPhysics = (model.IsPhysics == null) ? false : model.IsPhysics;
            model.IsPhysicsSlot = model.IsPhysicsSlot ?? "Not Applicable";
            model.IsChemistry = (model.IsChemistry == null) ? false : model.IsChemistry;
            model.IsChemistrySlot = model.IsChemistrySlot ?? "Not Applicable";
            model.IsComputer = (model.IsComputer == null) ? false : model.IsComputer;
            model.IsComputerSlot = model.IsComputerSlot ?? "Not Applicable";
            model.IsHistory = (model.IsHistory == null) ? false : model.IsHistory;
            model.IsHistorySlot = model.IsHistorySlot ?? "Not Applicable";
            model.IsGeography = (model.IsGeography == null) ? false : model.IsGeography;
            model.IsGeographySlot = model.IsGeographySlot ?? "Not Applicable";

            class_routine cls = new class_routine();
            cls.rou_id = model.rou_id;
            cls.rou_day = model.rou_day.ToString();
            cls.class_id = model.class_id;
            cls.IsMaths = model.IsMaths;
            cls.IsMathsSlot = model.IsMathsSlot;
            cls.IsEnglishLiterature = model.IsEnglishLiterature;
            cls.IsEnglishLiteratureSlot = model.IsEnglishLiteratureSlot;
            cls.IsEnglishGrammar = model.IsEnglishGrammar;
            cls.IsEnglishGrammarSlot = model.IsEnglishGrammarSlot;
            cls.IsUrdu = model.IsUrdu;
            cls.IsUrduSlot = model.IsUrduSlot;
            cls.IsIslamiyat = model.IsIslamiyat;
            cls.IsIslamiyatSlot = model.IsIslamiyatSlot;
            cls.IsScience = model.IsScience;
            cls.IsScienceSlot = model.IsScienceSlot;
            cls.IsPhysics = model.IsPhysics;
            cls.IsPhysicsSlot = model.IsPhysicsSlot;
            cls.IsChemistry = model.IsChemistry;
            cls.IsChemistrySlot = model.IsChemistrySlot;
            cls.IsComputer = model.IsComputer;
            cls.IsComputerSlot = model.IsComputerSlot;
            cls.IsHistory = model.IsHistory;
            cls.IsHistorySlot = model.IsHistorySlot;
            cls.IsGeography = model.IsGeography;
            cls.IsGeographySlot = model.IsGeographySlot;

            var classList = dbObj.classes.ToList();
            ViewBag.id = new MultiSelectList(classList, "id", "name", "section");
            if (model.rou_id == 0)
            {
                dbObj.class_routine.Add(cls);
                dbObj.SaveChanges();
            }
            else
            {
                dbObj.Entry(cls).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();
            }
            ModelState.Clear();

            return RedirectToAction("TimeTableList", "ClassRoutine");
        }

        public ActionResult TimeTableList(string searchBy, string searchValue)
        {
            List<JoinTimeTable> jc = new List<JoinTimeTable>();
            string constrg = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=studentmanagementsystem_;Persist Security Info=True;User ID=studentmanagementsystem_;Password=student123";
            SqlConnection sql = new SqlConnection(constrg);
            string sqlqu = "select cr.rou_id,cr.rou_day,cr.class_id,cr.IsMaths,cr.IsMathsSlot,cr.IsEnglishLiterature,cr.IsEnglishLiteratureSlot,cr.IsEnglishGrammar,cr.IsEnglishGrammarSlot,cr.IsUrdu,cr.IsUrduSlot," +
                "cr.IsIslamiyat,cr.IsIslamiyatSlot,cr.IsScience,cr.IsScienceSlot,cr.IsPhysics,cr.IsPhysicsSlot,cr.IsChemistry,cr.IsChemistrySlot,cr.IsComputer,cr.IsComputerSlot,cr.IsHistory,cr.IsHistorySlot," +
                "cr.IsGeography,cr.IsGeographySlot,c.name,c.section from class_routine cr inner join class c on cr.class_id = c.id ";
            SqlCommand sqlComm = new SqlCommand(sqlqu, sql);
            SqlDataAdapter sda = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                JoinTimeTable jcobj = new JoinTimeTable();
                jcobj.rou_id = (int)dr["rou_id"];
                jcobj.rou_day = dr["rou_day"].ToString();
                jcobj.class_id = (int)dr["class_id"];
                jcobj.IsMaths = (bool)dr["IsMaths"];
                jcobj.IsMathsSlot = dr["IsMathsSlot"].ToString();
                jcobj.IsEnglishLiterature = (bool)dr["IsEnglishLiterature"];
                jcobj.IsEnglishLiteratureSlot = dr["IsEnglishLiteratureSlot"].ToString();
                jcobj.IsEnglishGrammar = (bool)dr["IsEnglishGrammar"];
                jcobj.IsEnglishGrammarSlot = dr["IsEnglishGrammarSlot"].ToString();
                jcobj.IsUrdu = (bool)dr["IsUrdu"];
                jcobj.IsUrduSlot = dr["IsUrduSlot"].ToString();
                jcobj.IsIslamiyat = (bool)dr["IsIslamiyat"];
                jcobj.IsIslamiyatSlot = dr["IsIslamiyatSlot"].ToString();
                jcobj.IsScience = (bool)dr["IsScience"];
                jcobj.IsScienceSlot = dr["IsScienceSlot"].ToString();
                jcobj.IsPhysics = (bool)dr["IsPhysics"];
                jcobj.IsPhysicsSlot = dr["IsPhysicsSlot"].ToString();
                jcobj.IsChemistry = (bool)dr["IsChemistry"];
                jcobj.IsChemistrySlot = dr["IsChemistrySlot"].ToString();
                jcobj.IsComputer = (bool)dr["IsComputer"];
                jcobj.IsComputerSlot = dr["IsComputerSlot"].ToString();
                jcobj.IsHistory = (bool)dr["IsHistory"];
                jcobj.IsHistorySlot = dr["IsHistorySlot"].ToString();
                jcobj.IsGeography = (bool)dr["IsGeography"];
                jcobj.IsGeographySlot = dr["IsGeographySlot"].ToString();
                jcobj.ClassName = dr["name"].ToString();
                jcobj.ClassSection = dr["section"].ToString();

                jc.Add(jcobj);
            }

            if (string.IsNullOrEmpty(searchValue))
            {
                TempData["InfoMessage"] = "Please provide search value.";
                return View(jc);
            }
            else
            {
                if (searchBy.ToLower() == "rou_day")
                {
                    var searchByrou_day = jc.Where(p => p.rou_day.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByrou_day);
                }
                else if (searchBy.ToLower() == "classname")
                {
                    var searchByClassN = jc.Where(p => p.ClassName.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByClassN);
                }
                else if (searchBy.ToLower() == "section")
                {
                    var searchBysection = jc.Where(p => p.ClassSection.ToLower().Contains(searchValue.ToLower()));
                    return View(searchBysection);
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
            class_routine cls = dbObj.class_routine.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }
    }
}