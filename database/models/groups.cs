using MySqlConnector;

namespace IS_SISTEMA.models {
    

    public class Groups {

        public int id { get; set; }
        public string Shortcut { get; set; }
        public string Name { get; set; }
     
        public Groups(string shortcut, string name){
            this.Shortcut = shortcut;
            this.Name = name; 
        }
            public static void CTable(MySqlConnection sql) {
                string query = @"
                    CREATE TABLE IF NOT EXISTS groups (
                        id INT AUTO_INCREMENT PRIMARY KEY, 
                        shortcut VARCHAR(255) NOT NULL, 
                        name VARCHAR(255) NOT NULL
                    );
                ";
                using var cmd = new MySqlCommand(query, sql);
                cmd.ExecuteNonQuery();
            }
            public static void ITable(MySqlConnection sql, Groups groups) {
                string query = @"INSERT INTO groups (shortcut, name) VALUE (@shortcut, @name)";
                using var cmd = new MySqlCommand(query, sql);

                cmd.Parameters.AddWithValue("@shortcut", groups.Shortcut);
                cmd.Parameters.AddWithValue("@name", groups.Name);
                
                cmd.ExecuteNonQuery();
            }

            public static void InsertStudent(MySqlConnection sql, int group_id, int student_id) {
                string query = @"INSERT INTO groups_student (group_id, student_id) VALUE (@group_id, @student_id)";
                using var cmd = new MySqlCommand(query, sql);

                cmd.Parameters.AddWithValue("@group_id", group_id);
                cmd.Parameters.AddWithValue("@student_id", student_id);

                cmd.ExecuteNonQuery();
            }
            public static void InsertLecturer(MySqlConnection sql, int group_id, int lecturer_id) {
                string query = @"INSERT INTO group_lecturers (group_id, lecturer_id) VALUE (@group_id, @lecturer_id)";
                using var cmd = new MySqlCommand(query, sql);

                cmd.Parameters.AddWithValue("@group_id", group_id);
                cmd.Parameters.AddWithValue("@lecturer_id", lecturer_id);

                cmd.ExecuteNonQuery();
            }
            public static void InsertSubject(MySqlConnection sql, int group_id, int subject_id) {
                string query = @"INSERT INTO group_subjects (group_id, subject_id) VALUE (@group_id, @subject_id)";
                using var cmd = new MySqlCommand(query, sql);

                cmd.Parameters.AddWithValue("@group_id", group_id);
                cmd.Parameters.AddWithValue("@subject_id", subject_id);

                cmd.ExecuteNonQuery();
            }
            public static void Delete(MySqlConnection sql, int groupsId){
                string query = "DELETE FROM groups WHERE id = @id";
                using var cmd = new MySqlCommand(query, sql);
                cmd.Parameters.AddWithValue(@"id",groupsId);
                cmd.ExecuteNonQuery();
            }
            public static List<Students> GetStudentsByGroupId(MySqlConnection sql, int groupId)
            {
                string query = @"SELECT students.id, students.name, students.lastname, students.phone
                                FROM groups_student
                                JOIN students ON groups_student.student_id = students.id
                                WHERE groups_student.group_id = @groupId";
                using var cmd = new MySqlCommand(query, sql);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                
                using var reader = cmd.ExecuteReader();
                var students = new List<Students>();
                
                while (reader.Read())
                {
                    var student = new Students(
                    
                        reader.GetString("name"),
                        reader.GetString("lastname"),
                        reader.GetString("phone")
                    )
                    {
                        id = reader.GetInt32("id")
                    };
                    students.Add(student);
                }
            return students;
        } 
    }
}