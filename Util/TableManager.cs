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
        string filePath;
        //注意，这个fileName 应该是 name_type 的格式（没有后缀）
        public TableManager(UserInfo userInfo, string fileName) {
            this.userInfo = userInfo;
            this.fileName = fileName;
            Directory.CreateDirectory(root);
        }

        private string FilePath(long id, string fileName)
        {
            return root + $"/{id}/{fileName}.csv";
        }

        public string getFilePath()
        {
            return filePath;
        }

        public bool CreateTable(List<List<string>> data)
        {
            try
            {
                CSVManager csvManager = new CSVManager(data);
                Directory.CreateDirectory(root + "/" + userInfo.id.ToString());
                filePath = FilePath(userInfo.id, fileName);

                // 防止重复创建相同的文件 (...)
                dynamic? list = DBManager.GetChartListByUserID(userInfo.id);
                if(list == null)
                    return false;
                List<dynamic> items = list.data;
                bool hasDuplicates = items
                    .GroupBy(p => p.fileID)
                    .Any(g => g.Count() > 1);
                if(hasDuplicates)
                    return false;

                csvManager.SaveToFile(filePath);
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
