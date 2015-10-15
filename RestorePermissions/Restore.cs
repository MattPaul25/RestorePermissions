using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace RestorePermissions
{
    class RestoreCredentials
    {
        public string fileName { get; set; }
        public string sqlFile { get; set; }
        public RestoreCredentials()
        {
            Console.WriteLine("resetting logins");
            fileName = ConfigurationManager.ConnectionStrings["People"].ConnectionString;
            sqlFile = ConfigurationManager.ConnectionStrings["SqlRoles"].ConnectionString;
           
            ReadFile();
        }

        private string ReadSql()
        {
            string fullLine = "";
            using (StreamReader sr = new StreamReader(sqlFile))
            {
                string currentLine = sr.ReadLine();
                while (currentLine != null)
                {
                    fullLine += currentLine + "\n";             
                    currentLine = sr.ReadLine();                    
                }
            }
            return fullLine;
        }

        private void ReadFile()
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string currentLine = sr.ReadLine();
                while (currentLine != null)
                {
                    createCredential(currentLine);
                    currentLine = sr.ReadLine();
                }
            }
        }

        private void createCredential(string currentLine)
        {
            string user = TextUtils.LeftOf(currentLine, "|");
            int firstPipe = TextUtils.Search(currentLine, "|") + 1;
            string[] Databases = currentLine.Substring(firstPipe).Split('|');
            foreach (string db in Databases)
            {
                string sql = ReadSql();
                sql = sql.Replace("[db]", "[" + db + "]");
                sql = sql.Replace("[user]", "[" + user + "]");
                var x = new RunSQL(sql);
            }
        }
    }
}
