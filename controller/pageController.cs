using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using IS_SISTEMA.models;

namespace IS_SISTEMA.controllers {
    public class PageController : Controller {
                
        public IActionResult Index() {
            return View("~/views/index.cshtml");
            
        }
         public IActionResult LecturerIndex() {
            return View("~/views/lecturerdashboard.cshtml");
         }
        public IActionResult Groups() {
            MySqlConnection sql = Database.Connect();
            string query = @"Select id, shortcut, name FROM groups";
            using var cmd = new MySqlCommand(query, sql);
            using var reader = cmd.ExecuteReader();
            var groupTable = new List<Groups>();
            while(reader.Read()){
                groupTable.Add(new Groups(
                    reader.GetString("Shortcut"),
                    reader.GetString("Name")
                ){
                    id = reader.GetInt32("id")
                });
            }
            return View("~/views/groups.cshtml", groupTable);
        }
        public IActionResult Lecturers() {
            MySqlConnection sql = Database.Connect();
            string query = @"Select id, name, lastname FROM lecturers";
            using var cmd = new MySqlCommand(query, sql);
            using var reader = cmd.ExecuteReader();
            var lecturerTable = new List<Lecturers>();
            while(reader.Read()){
                lecturerTable.Add(new Lecturers(
                    reader.GetString("Name"),
                    reader.GetString("Lastname")
                ){
                    id = reader.GetInt32("id")
                });
            }
            return View("~/views/lecturers.cshtml", lecturerTable);
        }
        public IActionResult Subjects() {
            MySqlConnection sql = Database.Connect();
            string query = @"Select id, name FROM subjects";
            using var cmd = new MySqlCommand(query, sql);
            using var reader = cmd.ExecuteReader();
            var subjectTable = new List<Subjects>();
            while(reader.Read()){
                subjectTable.Add(new Subjects(
                    reader.GetString("Name")
                ){
                    id = reader.GetInt32("id")
                });
            }
            return View("~/views/subjects.cshtml", subjectTable);
        }
        public IActionResult Students() {
            MySqlConnection sql = Database.Connect();
            string query = @"Select id, name, lastname, phone FROM students";
            using var cmd = new MySqlCommand(query, sql);
            using var reader = cmd.ExecuteReader();
            var studentsTable = new List<Students>();
            while(reader.Read()){
                studentsTable.Add(new Students(
                    reader.GetString("Name"),
                    reader.GetString("Lastname"),
                    reader.GetString("Phone")
                ){
                    id = reader.GetInt32("id")
                });
            }  
            return View("~/views/students.cshtml", studentsTable);
        }
    }  
}
        
    




