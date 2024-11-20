using MySqlConnector;

namespace IS_SISTEMA.models {

    public class Subjects {

        public int id { get; set; }
        public string Name { get; set; }
     
        public Subjects(string name) {
            this.Name = name;
        }
            public static void CTable(MySqlConnection sql) {
                string query = @"CREATE TABLE IF NOT EXISTS subjects (
                    id INT AUTO_INCREMENT PRIMARY KEY, 
                    name VARCHAR(255) NOT NULL
                );
                    CREATE TABLE IF NOT EXISTS group_subjects (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    group_id INT NOT NULL,
                    subject_id INT NOT NULL,
                    FOREIGN KEY(group_id) REFERENCES groups(id) ON DELETE CASCADE,
                    FOREIGN KEY(subject_id) REFERENCES subjects(id) ON DELETE CASCADE
                );
                ";
                using var cmd = new MySqlCommand(query, sql);
                cmd.ExecuteNonQuery();
            }
            public static void ITable(MySqlConnection sql, Subjects subjects) {
                string query = @"INSERT INTO subjects (name) VALUE (@name)";
                using var cmd = new MySqlCommand(query, sql);
                
                cmd.Parameters.AddWithValue("@name", subjects.Name);
                
                cmd.ExecuteNonQuery();
            }
            public static void Delete(MySqlConnection sql, int subjectsId){
                string query = "DELETE FROM subjects WHERE id = @id";
                using var cmd = new MySqlCommand(query, sql);
                cmd.Parameters.AddWithValue("@id", subjectsId);
                
                cmd.ExecuteNonQuery();
            }
        } 
    }
