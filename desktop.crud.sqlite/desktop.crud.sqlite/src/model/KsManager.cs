using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using desktop.crud.sqlite.src.db;
using System.IO;
/*
 * author		Antonio Membrides Espinosa
 * update       13/08/2019
 * version    	1.0
 */
namespace desktop.crud.sqlite.src
{
    class KsManager
    {
        private SQLiteConnection cn;
        private SQLiteDataAdapter da;
        private String path;

        public KsManager()
        {
            this.path = "";
            this.cn = null;
            this.da = null;
        }

        public KsManager(String path)
        {
            this.path = path;
        }

        public void setPath(String path) {
            if (!File.Exists(Path.GetFullPath(path)))
                Console.WriteLine("<< WARNING << the path {0} does not exist.", path);

            this.path = path;
        }

        public SQLiteConnection getConnection(){
            return this.cn;
        }
        public bool connect() {
            if (this.cn != null) {
                return true;
            }
            if (this.path != "") {                
                this.cn = new SQLiteConnection(string.Format("Data Source={0};", this.path));
                
                Console.WriteLine("<< SQLITE MANAGER STATE << {0}", this.cn.State);
                //this.cn.Open();

                return true;
            }
            return false;
        }

        public bool disconnect()
        {
            this.cn.Close();
            this.cn = null;
            return true;
        }

        public SQLiteCommand exec(string sql) {
            this.cn.Open();
            var command = new SQLiteCommand(sql, this.cn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            this.cn.Close();
            return command;
        }

        public DataTable get(String sql)
        {
            this.da = new SQLiteDataAdapter(sql, this.cn); ;
            DataTable dt = new DataTable();
            
            da.Fill(dt);
            return dt;
        }

        public DataTable getAt(String table, String field, String value)
        {
            return this.get("SELECT * FROM " + table + " WHERE " + field + "=" + value + ";");
        }

        public DataTable getAt(String table)
        {
            return this.get("SELECT * FROM " + table + ";");
        }

        private readonly static KsManager myself = new KsManager();
        public static KsManager self()
        {
            return myself;
        }
    }
}
