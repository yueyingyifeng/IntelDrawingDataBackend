using IntelDrawingDataBackend.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace IntelDrawingDataBackend.Util
{
    // Token 生成器，包含生成和解析
    public class TokenGenerator
    {
        public static string sk = "c88359007bafbf2f0yyyf009d9db5f8b";
        public static string GenerateToken(UserInfo userInfo)
        {
            string iv = IDGenerator.GenerateRandomString(16);
            StringEncryptor encryptor = new StringEncryptor(sk, iv);
            string result = encryptor.Encrypt(userInfo.ToString());
            return result + iv;
        }

        public static UserInfo? AnalysisToken(string t)
        {
            string iv = t.Substring(t.Length - 16);
            string token = t[..^16];    //抹掉尾16位
            UserInfo? result;
            try
            {
                string j = new StringEncryptor(sk, iv).Decrypt(token);
                result = JsonConvert.DeserializeObject<UserInfo>(j);
                
            }
            catch (Exception)
            {
                return null;
            }
            return result;
        }
    }
}
