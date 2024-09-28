using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;
using System.Diagnostics;

namespace IntelDrawingDataBackend.Util
{
    public class TableManager
    {
        UserInfo userInfo;
        string fileName;
        string root = "data";
        public TableManager(UserInfo userInfo, string fileName) {
            this.userInfo = userInfo;
            this.fileName = fileName;
            Directory.CreateDirectory(root);
        }

        private string FilePath(long id, string fileName)
        {
            return root + $"/{id}/{fileName}.csv";
        }

        public bool CreateTable(List<List<string>> data)
        {
            try
            {
                CSVManager csvManager = new CSVManager(data);
                Directory.CreateDirectory(root + "/" + userInfo.id.ToString());
                string p = FilePath(userInfo.id, fileName);
                csvManager.SaveToFile(p);
                DBManager.CreateTable(userInfo.id, p);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TM: " + ex.Message);
                return false;
            }

            return true;
        }
        // 组合用户 id 和 名字
        public bool DeleteTableFile()
        {
            return DeleteTableFile(FilePath(userInfo.id, fileName));
        }
        // 直接删除
        public bool DeleteTableFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TableManager DeleteTableFile: {ex.Message}");
                return false;
            }
            return true;
        }

    }
}
