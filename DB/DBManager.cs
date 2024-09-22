using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using System.Text.Json;

namespace IntelDrawingDataBackend.DB
{
    public class DBManager
    {
        public static void InitDB()
        {
            Console.WriteLine("Creating Database");
            SqlResult sr = Sqlite3DBSupport.Exe(SqlSentences.init_tables);

            //Console.WriteLine(sr);
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
                sr = Sqlite3DBSupport.Exe(SqlSentences.login_checking(package));
                if (sr.Data != null && sr.Data.Count > 0 && sr.Data.Values.Any(list => list.Count == 1))
                {
                    
                    if (sr.Data["PSW"][0] == package.psw)//密码比对
                        userInfo = new UserInfo(
                            Convert.ToInt64(sr.Data["ID"][0]), 
                            sr.Data["Email"][0], 
                            sr.Data["Name"][0], 
                            Convert.ToInt64(sr.Data["CreateDate"][0])
                            );
                }
            }
            catch (JsonException je)
            {
                Console.WriteLine(sr + " ");
                Console.WriteLine("DB: " + je.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return userInfo;
        }
    
    
        public static UserInfo register_checking(RegisterPackage package)
        {
            UserInfo userInfo = null;
            SqlResult sr = null;
            try
            {
                sr = Sqlite3DBSupport.Exe(SqlSentences.register_checking(package));
                if (sr.Data == null)
                {
                    long id = IDGenerator.GenerateID();
                    sr = Sqlite3DBSupport.Exe(SqlSentences.register_insert(id, package));
                    userInfo = new UserInfo(
                        id,
                        package.Email,
                        package.Name,
                        DateTimeOffset.Now.ToUnixTimeMilliseconds()
                        );
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("DB: " + e.Message);
            }
            return userInfo;
        }

        public static bool CreateTable(long id, string filePath)
        {
            long fileID = IDGenerator.GenerateID();
            try
            {
                Sqlite3DBSupport.Exe(SqlSentences.CreateTable(id, fileID, filePath));
            }
            catch(Exception e)
            {
                Console.WriteLine("DB: " + e.Message);
                return false;
            }

            return true;
        }
    }
}
