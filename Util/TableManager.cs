using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Entities;

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
                Console.WriteLine(p);
                csvManager.SaveToFile(p);
                DBManager.CreateTable(userInfo.id, p);
            }
            catch (Exception ex)
            {
                Console.WriteLine("TM: " + ex.Message);
                return false;
            }

            return true;
        }

    }
}
