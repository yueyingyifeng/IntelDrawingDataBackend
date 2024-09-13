namespace IntelDrawingDataBackend.Util
{
    // 简易 Token 生成
    // 从时间戳中取后 3 位，再随机连续取 3 位，相乘的结果和长度为 6 的大小写字母和数字组合拼接，得到简易 Token
    public class TokenGenerator
    {
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
        public static string GetToken()
        {
            // 获取当前时间戳（以毫秒为单位）
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // 将时间戳转换为字符串
            string timestampStr = timestamp.ToString();

            // 获取时间戳末尾的三位
            string lastThreeDigits = timestampStr.Substring(timestampStr.Length - 3);

            // 随机从时间戳长度中取三位
            Random random = new Random();
            int randomStart = random.Next(0, timestampStr.Length - 3);
            string randomThreeDigits = timestampStr.Substring(randomStart, 3);

            // 计算两个值的乘积
            int result = int.Parse(lastThreeDigits) * int.Parse(randomThreeDigits);

            // 生成一个长度为6的随机字符串
            string randomString = GenerateRandomString(6);

            // 拼接结果并返回
            return result.ToString() + randomString;
        }
    }
}
