using MySqlConnector;


namespace IS_SISTEMA.models {

    public class Lecturers {

        public int id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Lecturers(string name, string lastname){
            this.Name = name;
            this.Lastname = lastname; 
        }
            public static void CTable(MySqlConnection sql) {
                string query = @"
                CREATE TABLE IF NOT EXISTS lecturers (
                    id INT AUTO_INCREMENT PRIMARY KEY, 
                    name VARCHAR(255) NOT NULL, 
                    lastname VARCHAR(255) NOT NULL
                );
                    CREATE TABLE IF NOT EXISTS group_lecturers (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    group_id INT NOT NULL,
                    lecturer_id INT NOT NULL,
                    FOREIGN KEY (group_id) REFERENCES groups(id) ON DELETE CASCADE,
                    FOREIGN KEY (lecturer_id) REFERENCES lecturers(id) ON DELETE CASCADE

                );
            ";
                using var cmd = new MySqlCommand(query, sql);
                cmd.ExecuteNonQuery();
            }
            public static void ITable(MySqlConnection sql, Lecturers lecturers) {
                string query = @"INSERT INTO lecturers (name, lastname) VALUE (@name, @lastname)";
                using var cmd = new MySqlCommand(query, sql);

                cmd.Parameters.AddWithValue("@name", lecturers.Name);
                cmd.Parameters.AddWithValue("@lastname", lecturers.Lastname);
                

                cmd.ExecuteNonQuery();
            }
            public static void Delete(MySqlConnection sql, int lecturersId){
                string query = "DELETE FROM lecturers WHERE id = @id";
                using var cmd = new MySqlCommand(query, sql);
                cmd.Parameters.AddWithValue(@"id", lecturersId);
                cmd.ExecuteNonQuery();
            }
        } 
    }