namespace IntelDrawingDataBackend.Util
{
    // 用户信息
    public class UserInfo
    {
        public UserInfo(int id, string email, string name, long createTime)
        {
            this.id = id;
            this.email = email;
            this.name = name;
            this.createTime = createTime;
        }

        public int id {  get; set; }
        public string email { get; set; }
        public string name {  get; set; }
        public long createTime { get; set; }
    }
}
