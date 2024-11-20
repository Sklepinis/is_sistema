using MySqlConnector;


namespace IS_SISTEMA.models {

    public class Students {

        public int id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }  

        public Students(string name, string lastname, string phone){
            this.Name = name;
            this.Lastname = lastname; 
            this.Phone = phone;
        }
            public static void CTable(MySqlConnection sql) {
                string query = @"CREATE TABLE IF NOT EXISTS students (
                    id INT AUTO_INCREMENT PRIMARY KEY, 
                    name VARCHAR(255) NOT NULL, 
                    lastname VARCHAR(255) NOT NULL,
                    phone VARCHAR(255) NOT NULL
                );
                    CREATE TABLE IF NOT EXISTS groups_student (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    group_id INT NOT NULL,
                    student_id INT NOT NULL,
                    FOREIGN KEY (group_id) REFERENCES groups(id) ON DELETE CASCADE,
                    FOREIGN KEY (student_id) REFERENCES students(id) ON DELETE CASCADE   
                );    
                ";
                using var cmd = new MySqlCommand(query, sql);
                cmd.ExecuteNonQuery();
            }
            public static void ITable(MySqlConnection sql, Students students) {
                string query = @"INSERT INTO students (name, lastname, phone) VALUE (@name, @lastname, @phone)";
                using var cmd = new MySqlCommand(query, sql);

                cmd.Parameters.AddWithValue("@name", students.Name);
                cmd.Parameters.AddWithValue("@lastname", students.Lastname);
                cmd.Parameters.AddWithValue("@phone", students.Phone);
                cmd.ExecuteNonQuery();
            }
            public static void Delete(MySqlConnection sql, int studentsId){
                string query = "DELETE FROM students WHERE id = @id";
                using var cmd = new MySqlCommand(query, sql);
                cmd.Parameters.AddWithValue("@id", studentsId);
                
                cmd.ExecuteNonQuery();
            }
        } 
    }
