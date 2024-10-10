using IntelDrawingDataBackend.Entities;
using IntelDrawingDataBackend.Util;
using System.Diagnostics;
using System.Text.Json;

namespace IntelDrawingDataBackend.DB
{
    public class DBManager
    {
        public static void InitDB()
        {
            Debug.WriteLine("Creating Database");
            SqlResult sr = Sqlite3DBSupport.Exe(SqlSentences.init_tables);

            if (sr == null)
                throw new Exception("Create DB failed");
            Debug.WriteLine("Create Database done");
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
                Debug.WriteLine(sr + " ");
                Debug.WriteLine("DB: " + je.Message);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
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
                Debug.WriteLine("DB: " + e.Message);
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
                Debug.WriteLine("DB CreateTable: " + e.Message);
                return false;
            }

            return true;
        }

        public static string? GetFilePathByFileID(long fileID)
        {
            string? filePath = null;
            try
            {
                SqlResult s = Sqlite3DBSupport.Exe(SqlSentences.GetFileNameByfileID(fileID));
                if (s.Data == null)
                    return null;
                filePath = s.Data["FilePath"][0];
            }
            catch (Exception e)
            {
                Debug.WriteLine($"DB GetFilePathByFileID: {e.Message}");
            }

            return filePath;
        }

        public static bool DeleteTable(long fileID)
        {
            try
            {
                int affected = Sqlite3DBSupport.Exe(SqlSentences.DeleteTable(fileID)).affected;
                if(affected == 0) return false;
                if (affected > 1)//这本可以是完全没必要的
                    throw new Exception("########## delete too many!");
            }
            catch (Exception e)
            {
                Debug.WriteLine($"DB DeleteTable: {e.Message}");
                return false;
            }
            return true;
        }
        // 获取该用户所保存的图表列表
        public static object? GetChartListByUserID(long userID)
        {
            var result = new
            {
                data = new List<object>()
            };

            try
            {
                SqlResult s = Sqlite3DBSupport.Exe(SqlSentences.GetChartListByUserID(userID));
                if (s.Data == null)
                    return result;
                for (int i = 0; i < s.Data["FileID"].Count; i++)
                {
                    (string fileName, string fileType)= Tools.GetFileNameAndTypeFromPath(s.Data["FilePath"][i]);

                    var item = new
                    {
                        fileID = s.Data["FileID"][i],
                        name = fileName,
                        type = fileType,
                        createTime = s.Data["CreateDate"][i],
                    };
                    result.data.Add(item);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"DB GetChartListByUserID: {e.Message}");
                return null;
            }

            return result;
        }
    
    
        public static CreateTablePackage? LoadChart(long userID, long fileID)
        {
            CreateTablePackage ctp = new CreateTablePackage();
            try
            {
                var s = Sqlite3DBSupport.Exe(SqlSentences.GetChartListByUserID(userID));
                if (s.Data == null)
                    return null;
                //s.Data["FileID"];
            }
            catch(Exception e)
            {
                Debug.WriteLine($"DB LoadChart: {e.Message}");
                return null;
            }

            return ctp;
        }

        public static bool UpdateChartPathByFileID(long fileID, string newPath)
        {
            try
            {
                var s = Sqlite3DBSupport.Exe(SqlSentences.UpDateChartPathByFileID(fileID,newPath));
                if (s.affected != 1)
                    return false;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"DB UpdateChartPathByFileID: {e.Message}");
                return false;
            }
            return true;
        }
    
    }
}
