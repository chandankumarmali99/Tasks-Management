using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Task_Management.Models;

namespace Task_Management.DAL
{

    public class TaskDAL
    {
        private string connStr = ConfigurationManager.ConnectionStrings["conn"].ToString();

        // Get Task by ID
        //public Task GetTaskById(int id) { /* use sp_Task_GetById */ }

        // Add/Edit Task
        public bool SaveTask(Tasks model)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("InsertTask", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TaskId", model.TaskId);
                cmd.Parameters.AddWithValue("@TaskTitle", model.TaskTitle);
                cmd.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);
                cmd.Parameters.AddWithValue("@TaskDueDate", model.TaskDueDate);
                cmd.Parameters.AddWithValue("@TaskStatus", model.TaskStatus);
                cmd.Parameters.AddWithValue("@TaskRemarks", model.TaskRemarks ?? "");
                cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
                cmd.Parameters.AddWithValue("@UpdatedBy", "Admin");

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<Tasks> GetAllTasks()
        {
            List<Tasks> list = new List<Tasks>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllTasks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Tasks
                    {
                        TaskId = Convert.ToInt32(dr["TaskId"]),
                        TaskTitle = dr["TaskTitle"].ToString(),
                        TaskDescription = dr["TaskDescription"].ToString(),
                        TaskDueDate = dr["TaskDueDate"] == DBNull.Value ? (DateTime?) null : Convert.ToDateTime(dr["TaskDueDate"]),
                        TaskStatus = dr["TaskStatus"].ToString(),
                        TaskRemarks = dr["TaskRemarks"].ToString(),
                        CreatedOn = dr["CreatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedOn"]),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        UpdatedBy = dr["UpdatedBy"].ToString(),
                        UpdatedOn = dr["UpdatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedOn"]),
                    });
                }
            }
            return list;
        }
        // Update Task
        public void UpdateTask(Tasks obj)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("InsertTask", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TaskId", obj.TaskId);
                cmd.Parameters.AddWithValue("@TaskTitle", obj.TaskTitle);
                cmd.Parameters.AddWithValue("@TaskDescription", obj.TaskDescription);
                cmd.Parameters.AddWithValue("@TaskDueDate", obj.TaskDueDate);
                cmd.Parameters.AddWithValue("@TaskStatus", obj.TaskStatus);
                cmd.Parameters.AddWithValue("@TaskRemarks", obj.TaskRemarks);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete Task
        public void DeleteTask(int id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("TaskDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // GetBYid
        public Tasks GetTaskById(int id)
        {
            Tasks task = null;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("TaskGetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskId", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    task = new Tasks
                    {
                        TaskId = Convert.ToInt32(dr["TaskId"]),
                        TaskTitle = dr["TaskTitle"].ToString(),
                        TaskDescription = dr["TaskDescription"].ToString(),
                        TaskDueDate = dr["TaskDueDate"] == DBNull.Value
                                        ? (DateTime?)null
                                        : Convert.ToDateTime(dr["TaskDueDate"]),
                        TaskStatus = dr["TaskStatus"].ToString(),
                        TaskRemarks = dr["TaskRemarks"].ToString()
                    };
                }
            }
            return task;
        }

        // Search Task
        public List<Tasks> SearchTaskByTitle(string taskTitle)
        {
            List<Tasks> list = new List<Tasks>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("TaskSearch", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SearchText", taskTitle);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Tasks
                    {
                        TaskId = Convert.ToInt32(dr["TaskId"]),
                        TaskTitle = dr["TaskTitle"].ToString(),
                        TaskDescription = dr["TaskDescription"].ToString(),
                        TaskDueDate = dr["TaskDueDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TaskDueDate"]),
                        TaskStatus = dr["TaskStatus"].ToString(),
                        TaskRemarks = dr["TaskRemarks"].ToString(),
                        CreatedOn = dr["CreatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedOn"]),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        UpdatedBy = dr["UpdatedBy"].ToString(),
                        UpdatedOn = dr["UpdatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedOn"]),
                    });
                }
            }
            return list;
        }

    }
}