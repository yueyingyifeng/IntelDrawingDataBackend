using IntelDrawingDataBackend.Entities;
using System.Text.Json;

namespace IntelDrawingDataBackend.DB
{



    public class DBManager
    {


        public static void InitDB()
        {
            Console.WriteLine("Creating Database");
            SqlResult sr = Sqlite3DBSupport.Exe(SqlSentences.init_tables);

            Console.WriteLine(sr);
            if (sr == null)
                throw new Exception("Create DB failed");
            Console.WriteLine("Create Database done");
        }

        public static UserInfo login_checking(LoginPackage package)
        {
            UserInfo userInfo = null;
            SqlResult sr = null;
            try
            {
                sr = Sqlite3DBSupport.Exe(SqlSentences.login_checking(package)); ;
                Console.WriteLine(sr);
                if(sr.Data != null && sr.Data.Count == 1)
                {
                    userInfo = new UserInfo(
                        Convert.ToInt32(sr.Data["ID"][0]), 
                        sr.Data["Email"][0], 
                        sr.Data["Name"][0], 
                        Convert.ToInt64(sr.Data["CreateDate"][0])
                        );
                }
            }
            catch (JsonException je)
            {
                Console.WriteLine(sr + " ");
                Console.WriteLine(je.Message);
            }



            return userInfo;
        }
    }
}
