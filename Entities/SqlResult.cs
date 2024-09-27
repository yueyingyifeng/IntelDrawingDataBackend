using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    // Sqlite3DBSupport.dll 返回的类型
    public class SqlResult
    {
        public Dictionary<string, List<string>> Data { get; set; }
        public string Sql { get; set; }
        public int affected { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
