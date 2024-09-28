namespace IntelDrawingDataBackend.Util
{
    public class Tools
    {
        public static (string fileName, string fileType) GetFileNameAndTypeFromPath(string filePath)
        {
            // 获取文件名部分，不包含路径
            string fileNameWithExtension = Path.GetFileNameWithoutExtension(filePath); // a_bar

            // 检查文件名部分是否为空或缺失
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                return ("null", "null");
            }

            // 将文件名分开
            string[] nameParts = fileNameWithExtension.Split('_'); // 分割为 a 和 bar

            // 获取文件名，若缺失则填充为 "null"
            string fileName = nameParts.Length > 0 && !string.IsNullOrEmpty(nameParts[0]) ? nameParts[0] : "null";

            // 获取文件类型，若缺失则填充为 "null"
            string fileType = nameParts.Length > 1 && !string.IsNullOrEmpty(nameParts[1]) ? nameParts[1] : "null";
            
            return (fileName, fileType);
        }
    }
}
