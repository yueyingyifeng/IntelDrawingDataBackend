using IntelDrawingDataBackend.Util;

namespace IntelDrawingDataBackend.Entities
{
    // 用户验证
    public class UserCredential
    {
        public string token { get; set; }
        public UserInfo userInfo { get; set; }


        private LoginPackage loginPackage;
        public UserCredential(LoginPackage loginPackage)
        {
            this.loginPackage = loginPackage;
        }

        public bool isPass() {

            return findUser();
        }

        bool findUser()
        {
            bool result = false;
            if((loginPackage.id == 123 || loginPackage.email == "123@123.com") && loginPackage.psw == "123456")
            {
                userInfo = new UserInfo(123, "123@123.com", "hzc", DateTimeOffset.Now.ToUnixTimeMilliseconds());
                //TODO: 这里是数据库查询
                result = true;
            }

            return result;
        }
    }
}
