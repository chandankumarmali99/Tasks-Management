using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task_Management.DAL;
using Task_Management.Models;

namespace Task_Management.Controllers
{
    public class TaskController : Controller
    {
        private TaskDAL dal = null;

        public TaskController()
        {
            dal = new TaskDAL();
        }

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveTask(Tasks model)
        {
           bool success = success = dal.SaveTask(model);
           return Json(new { success, message = "Saved successfully!" });
        }
        public JsonResult GetAllTasks()
        {
            var data = dal.GetAllTasks();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTaskById(int id)
        {
            var task = dal.GetTaskById(id);
            return Json(task, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateTask(Tasks model)
        {
            if (model == null || model.TaskId == 0)
            {
                return Json("Invalid task data");
            }

            dal.SaveTask(model);

            return Json("Task updated successfully");

        }
        [HttpPost]
        public JsonResult DeleteTask(int id)
        {
            if (id == 0)
            {
                return Json("Invalid task id");
            }

            dal.DeleteTask(id);

            return Json("Task deleted successfully");
        }

        [HttpGet]
        public JsonResult SearchTask(string taskTitle)
        {
            TaskDAL dal = new TaskDAL();
            var data = dal.SearchTaskByTitle(taskTitle);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }


}