using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace RestorePermissions
{
    class RunSQL
    {
        public RunSQL(string sql)
        {
            connect(sql);
        }
         private void connect(string sql)
         {
            
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }

        }
    }
}
