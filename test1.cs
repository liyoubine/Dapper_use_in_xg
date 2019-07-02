
using XuguClient;
using Dapper;
using System.Data;

namespace Dapper_use_in_xg
{
 public class School
    {
        public string Name { set; get; }
        public string Addr { set; get; }

        public School(string title, string addr)
        {
            this.Name = title;
            this.Addr = addr;
        }

        public School()
        {
            this.Name = "Massachusetts Institute of Technology";
            this.Addr = "Americ";
        }
    }
  public class testDapper 
    {
        public static string conn_xg3 = "IP=192.168.2.203;DB=SYSTEM;User=SYSDBA;PWD=SYSDBA;Port=5138;AUTO_COMMIT=on;CHAR_SET=GBK";
        public static int t_testDapper()
        {
            IDbConnection Connection = null;
            Connection = (IDbConnection)new XGConnection(conn_xg3);
            Connection.Open();
            Connection.Execute( "CREATE TABLE T_CMD_PARAMNAME(PID  INTEGER, NAME VARCHAR ,BIRTH DATETIME, ADDR VARCHAR);");
            Connection.Execute("INSERT INTO T_CMD_PARAMNAME VALUES(7,'sdf','2019-06-25 09:09:55','dhuiwho');");
            String sql = "INSERT INTO T_CMD_PARAMNAME VALUES(19,:Name,'2019-05-11 14:00:55',:Addr);";
            Connection.Execute(sql, new { name = "Tsinghua University", addr = "China" });

            Connection.Execute(sql, new { name = "Massachusetts Institute of Technology", addr = "saime" });
            String sql2 = "INSERT INTO T_CMD_PARAMNAME VALUES(17,:Name,'2019-06-28 11:16:55',:Addr);";
            School Var = new School();
            Connection.Execute(sql2, Var);
         
            string Dql = "SELECT NAME, ADDR  FROM T_CMD_PARAMNAME WHERE ADDR =:Addr ";
            var Result = Connection.Query<School>(Dql, new { Addr = "China" });
            foreach (var Record in Result)
            {
                Console.WriteLine(Record.Name);
            }
            Connection.Close();
            return 0;
        }
   }

}
