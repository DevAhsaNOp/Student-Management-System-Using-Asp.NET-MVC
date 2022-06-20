using Student_Management_System.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Student_Management_System.Controllers
{
    public class ClassController : Controller
    {

        // GET: Class
        studentmanagementsystem_Entities1 dbObj = new studentmanagementsystem_Entities1();
        public ActionResult AddClass(@class model)
        {
            return View(model);
        }


        [HttpPost]
        public ActionResult Class(@class model)
        {
            model.IsMaths = (model.IsMaths == null) ? false : model.IsMaths;
            model.IsEnglishLiterature = (model.IsEnglishLiterature == null) ? false : model.IsEnglishLiterature;
            model.IsEnglishGrammar = (model.IsEnglishGrammar == null) ? false : model.IsEnglishGrammar;
            model.IsUrdu = (model.IsUrdu == null) ? false : model.IsUrdu;
            model.IsIslamiyat = (model.IsIslamiyat == null) ? false : model.IsIslamiyat;
            model.IsScience = (model.IsScience == null) ? false : model.IsScience;
            model.IsPhysics = (model.IsPhysics == null) ? false : model.IsPhysics;
            model.IsChemistry = (model.IsChemistry == null) ? false : model.IsChemistry;
            model.IsComputer = (model.IsComputer == null) ? false : model.IsComputer;
            model.IsHistory = (model.IsHistory == null) ? false : model.IsHistory;
            model.IsGeography = (model.IsGeography == null) ? false : model.IsGeography;

            @class cls = new @class();
            cls.id = model.id;
            cls.name = model.name;
            cls.section = model.section;
            cls.IsMaths = model.IsMaths;
            cls.IsEnglishLiterature = model.IsEnglishLiterature;
            cls.IsEnglishGrammar = model.IsEnglishGrammar;
            cls.IsUrdu = model.IsUrdu;
            cls.IsIslamiyat = model.IsIslamiyat;
            cls.IsScience = model.IsScience;
            cls.IsPhysics = model.IsPhysics;
            cls.IsChemistry = model.IsChemistry;
            cls.IsComputer = model.IsComputer;
            cls.IsHistory = model.IsHistory;
            cls.IsGeography = model.IsGeography;

            #region Unused_Code
            //cls.IsMaths = (model.IsMaths = (maths == "true") ? true : false);
            //cls.IsEnglishLiterature = (model.IsEnglishLiterature = (engli == "true"));
            //cls.IsEnglishGrammar = (model.IsEnglishGrammar = (enggr == "true"));
            //cls.IsUrdu = (model.IsUrdu == (urdu == "true"));
            //cls.IsIslamiyat = (model.IsIslamiyat = (isy == "true"));
            //cls.IsScience = (model.IsScience = (sci == "true"));
            //cls.IsPhysics = (model.IsPhysics = (phy == "true"));
            //cls.IsChemistry = (model.IsChemistry = (che == "true"));
            //cls.IsComputer = (model.IsComputer = (comp == "true"));
            //cls.IsHistory = (model.IsHistory = (hist == "true"));
            //cls.IsGeography = (model.IsGeography = (geog == "true"));
            #endregion


            if (model.id == 0)
            {
                dbObj.classes.Add(cls);
                dbObj.SaveChanges();
            }
            else
            {
                dbObj.Entry(cls).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();
            }
            ModelState.Clear();

            return RedirectToAction("ClassList", "Class");
        }

        //, string maths, string engli, string enggr, string urdu
        //    , string isy, string sci, string phy, string che, string comp, string hist, string geog
        //[HttpPost]
        //public ActionResult Edit(@class model)
        //{

        //    @class cls = new @class();

        //    cls.name = model.name;
        //    cls.section = model.section;
        //    cls.IsMaths = model.IsMaths;
        //    cls.IsEnglishLiterature = model.IsEnglishLiterature;
        //    cls.IsEnglishGrammar = model.IsEnglishGrammar;
        //    cls.IsUrdu = model.IsUrdu;
        //    cls.IsIslamiyat = model.IsIslamiyat;
        //    cls.IsScience = model.IsScience;
        //    cls.IsPhysics = model.IsPhysics;
        //    cls.IsChemistry = model.IsChemistry;
        //    cls.IsComputer = model.IsComputer;
        //    cls.IsHistory = model.IsHistory;
        //    cls.IsGeography = model.IsGeography;
        //    //cls.IsMaths = (model.IsMaths = (maths == "true") ? true : false);
        //    //cls.IsEnglishLiterature = (model.IsEnglishLiterature = (engli == "true"));
        //    //cls.IsEnglishGrammar = (model.IsEnglishGrammar = (enggr == "true"));
        //    //cls.IsUrdu = (model.IsUrdu == (urdu == "true"));
        //    //cls.IsIslamiyat = (model.IsIslamiyat = (isy == "true"));
        //    //cls.IsScience = (model.IsScience = (sci == "true"));
        //    //cls.IsPhysics = (model.IsPhysics = (phy == "true"));
        //    //cls.IsChemistry = (model.IsChemistry = (che == "true"));
        //    //cls.IsComputer = (model.IsComputer = (comp == "true"));
        //    //cls.IsHistory = (model.IsHistory = (hist == "true"));
        //    //cls.IsGeography = (model.IsGeography = (geog == "true"));

        //    if (model.id == 0)
        //    {
        //        dbObj.classes.Add(cls);
        //        dbObj.SaveChanges();
        //    }
        //    else
        //    {
        //        dbObj.Entry(cls).State = EntityState.Modified;
        //        dbObj.SaveChanges();
        //    }
        //    ModelState.Clear();

        //    return RedirectToAction("StudentList", "Student");
        //}

        public ActionResult Edit(int Id)
        {
            List<@class> clslist = dbObj.classes.ToList();

            //getting a class from collection
            var cls = clslist.Where(s => s.id == Id).FirstOrDefault();

            return View(cls);
        }

        [HttpPost]
        public ActionResult Edit(@class clas)
        {
            List<@class> clslist = dbObj.classes.ToList();

            //update list by removing old class and adding updated class information
            var cls = clslist.Where(s => s.id == clas.id).FirstOrDefault();
            dbObj.classes.Remove(cls);
            dbObj.classes.Add(clas);
            dbObj.SaveChanges();

            return RedirectToAction("ClassList");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // return a bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            @class cls = dbObj.classes.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }

        //public ActionResult Edit(int? id)
        //{
        //    // it good practice to consider that things could go wrong so,it is wise to have a validation in the controller
        //    if (id == null)
        //    {
        //        // returns a bad request
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    // It finds the Student to be edited.
        //    @class student = dbObj.classes.Find(id);
        //    if (student == null)
        //    {
        //        // if doesn't found returns 404
        //        return HttpNotFound();
        //    }
        //    // Returns the Student data to fill out the edit form values.
        //    return View(student);
        //}

        //[HttpPost]

        //[ValidateAntiForgeryToken]

        //Represents an attribute that is used for the name of an action.
        //[ActionName("Edit")]
        //public ActionResult Edit(@class cls)
        //{
        //    try
        //    {
        //        Gets a value that indicates whether this instance received from the view is valid.
        //        if (ModelState.IsValid)
        //        {
        //            Two thing happens here:
        //             1) db.Entry(student)->Gets a DbEntityEntry object for the student entity providing access to information about it and the ability to perform actions on the entity.

        //           2) Set the student state to modified, that means that the student entity is being tracked by the context and exists in the database, and some or all of its property values have been modified.
        //            dbObj.Entry(cls).State = EntityState.Modified;

        //            Now just save the changes that all the changes made in the form will be persisted.
        //           dbObj.SaveChanges();

        //            Returns an HTTP 302 response to the browser, which causes the browser to make a GET request to the specified action, in this case the index action.
        //            return RedirectToAction("ClassList");
        //        }
        //    }
        //    catch
        //    {
        //        Log the error add a line here to write a log.
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        //    }

        //    return the invalid student instance to be corrected.
        //    return View(cls);
        //}

        public ActionResult ClassList(string searchBy, string searchValue)
        {
            List<JoinClasses> jc = new List<JoinClasses>();
            string constrg = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=studentmanagementsystem_;Persist Security Info=True;User ID=studentmanagementsystem_;Password=student123";
            SqlConnection sql = new SqlConnection(constrg);
            string sqlqu = "select * from class";
            SqlCommand sqlComm = new SqlCommand(sqlqu, sql);
            SqlDataAdapter sda = new SqlDataAdapter(sqlComm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                JoinClasses jcobj = new JoinClasses();
                jcobj.id = (int)dr["id"];
                jcobj.name = dr["name"].ToString();
                jcobj.section = dr["section"].ToString();
                jcobj.IsMaths = (bool)dr["IsMaths"];
                jcobj.IsEnglishLiterature = (bool)dr["IsEnglishLiterature"];
                jcobj.IsEnglishGrammar = (bool)dr["IsEnglishGrammar"];
                jcobj.IsUrdu = (bool)dr["IsUrdu"];
                jcobj.IsIslamiyat = (bool)dr["IsIslamiyat"];
                jcobj.IsScience = (bool)dr["IsScience"];
                jcobj.IsPhysics = (bool)dr["IsPhysics"];
                jcobj.IsChemistry = (bool)dr["IsChemistry"];
                jcobj.IsComputer = (bool)dr["IsComputer"];
                jcobj.IsHistory = (bool)dr["IsHistory"];
                jcobj.IsGeography = (bool)dr["IsGeography"];

                jc.Add(jcobj);
            }

            if (string.IsNullOrEmpty(searchValue))
            {
                TempData["InfoMessage"] = "Please provide search value.";
                return View(jc);
            }
            else
            {
                if (searchBy.ToLower() == "name")
                {
                    var searchByFname = jc.Where(p => p.name.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByFname);
                }
                else if (searchBy.ToLower() == "section")
                {
                    var searchByPhone = jc.Where(p => p.section.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByPhone);
                }
            }

            #region Unused_Code
            //if (string.IsNullOrEmpty(searchValue))
            //{
            //    TempData["InfoMessage"] = "Please provide search value.";
            //    return View(jc);
            //}
            //else
            //{
            //    if (searchBy.ToLower() == "std_fname")
            //    {
            //        var searchByFname = jc.Where(p => p.std_Fname.ToLower().Contains(searchValue.ToLower()));
            //        return View(searchByFname);
            //    }
            //    else if (searchBy.ToLower() == "std_phone")
            //    {
            //        var searchByPhone = jc.Where(p => p.std_phone.ToLower().Contains(searchValue.ToLower()));
            //        return View(searchByPhone);
            //    }
            //    else if (searchBy.ToLower() == "classname")
            //    {
            //        var searchByClass = jc.Where(p => p.ClassName.ToLower().Contains(searchValue.ToLower()));
            //        return View(searchByClass);
            //    }
            //}
            #endregion

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

            return View(jc);
        }
    }
}