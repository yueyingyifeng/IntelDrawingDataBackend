using IntelDrawingDataBackend.DB;
using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    // 用户验证
    public class UserCredential
    {
        public string token { get; set; }
        public UserInfo userInfo { get; set; }


        private LoginPackage loginPackage;
        private RegisterPackage registerPackage;
        public UserCredential(LoginPackage loginPackage)
        {
            this.loginPackage = loginPackage;
            
        }

        public UserCredential(RegisterPackage registerPackage)
        {
            this.registerPackage = registerPackage;
        }

        public bool isAlreadyHaveTheUser()
        {
            bool result = true;



            return result;
        }

        public bool isPass() {

            bool result = false;
            UserInfo userInfo = DBManager.login_checking(loginPackage);
            if (userInfo == null)
                return false;
            return true;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
