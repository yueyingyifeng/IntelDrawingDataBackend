using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    // 用户信息
    public class UserInfo
    {
        public UserInfo(long id, string email, string name, long createTime)
        {
            this.id = id;
            this.email = email;
            this.name = name;
            this.createTime = createTime;
        }

        public long id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public long createTime { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
