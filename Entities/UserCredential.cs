using IntelDrawingDataBackend.DB;
using IntelDrawingDataBackend.Util;
using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    // 用户验证
    public class UserCredential
    {
        public string? token { get; set; }
        public UserInfo? userInfo { get; set; }


        private LoginPackage? loginPackage;
        private RegisterPackage? registerPackage;

        public UserCredential(string? token) {
            this.token = token;

        }
        public UserCredential(LoginPackage loginPackage)
        {
            this.loginPackage = loginPackage;
            
        }

        public UserCredential(RegisterPackage registerPackage)
        {
            this.registerPackage = registerPackage;
        }
        // 注册判断
        public bool isAlreadyHaveTheUser()
        {
            userInfo = DBManager.register_checking(registerPackage);
            if (userInfo == null)
                return true;
            return false;
        }
        // 登录判断
        public bool isPass() {
            userInfo = DBManager.login_checking(loginPackage);
            if (userInfo == null)
                return false;
            return true;
        }

        public void GenerateToken()
        {
            token = TokenGenerator.GenerateToken(userInfo);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        bool NotRightToken()
        {
            return token == null || token.Length < 16;
        }

        UserInfo? GetUserInfoByToken() {
            token = token.Substring(6);
            return TokenGenerator.AnalysisToken(token);
        }

        public bool IsTokenCool()
        {
            if (NotRightToken()) 
                return false;
            userInfo = GetUserInfoByToken();
            if(userInfo == null)
                return false;
            return true;
        }
    }
}
