using MySqlConnector;
using IS_SISTEMA.models;

class Database {
        public static MySqlConnection Connect() {
        string? connectionStr = "Server=127.0.0.1;User=root;Password=admin;Database=is;Pooling=false;";
        
        var sql = new MySqlConnection(connectionStr);

        try {
            sql.Open();
            Console.WriteLine("[ DB ] - connected to database");
            
            Students.CTable(sql);
            Lecturers.CTable(sql);
            Groups.CTable(sql);
            Subjects.CTable(sql);
        
        } catch (Exception e) {
            Console.WriteLine("[ DB ] - Connection failed");
            Console.WriteLine(e.Message);
        }

        return sql;
    
    }

} 