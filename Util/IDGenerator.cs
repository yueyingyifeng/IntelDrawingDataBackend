namespace IntelDrawingDataBackend.Util
{
    // 简易 Token 和 ID 生成器
    // Token 生成: 从时间戳中取后 3 位，再随机连续取 3 位，相乘的结果和长度为 6 的大小写字母和数字组合拼接，得到简易 Token
    // 简易 Token 生成废弃，因为对 Token 的功能有理解偏差，现在用新的 Token 生成器
    // ID 生成：雪花算法，省略了机器 ID 和数据中心 ID 
    // 注：由 chatGPT 生成的代码
    public class IDGenerator
    {
        public static string GenerateRandomString(int length)
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
        [Obsolete("因为对 Token 的功能有理解偏差，现在用新的 Token 生成器类")]
        public static string GenerateToken_old()
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
        

        
        
        //===========================================雪花算法===========================================//
        private static readonly DateTime Epoch = new DateTime(2024, 9, 13);
        private const int SequenceBits = 12;
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);
        private const int TimestampLeftShift = SequenceBits;

        private static long lastTimestamp = -1L;
        private static long sequence = 0L;
        private static readonly object lockObj = new object();

        public static long GenerateID()
        {
            lock (lockObj)
            {
                var timestamp = GetCurrentTimestamp();

                if (timestamp < lastTimestamp)
                {
                    throw new InvalidOperationException("Clock moved backwards.");
                }

                if (timestamp == lastTimestamp)
                {
                    sequence = (sequence + 1) & SequenceMask;
                    if (sequence == 0)
                    {
                        timestamp = WaitForNextTimestamp(lastTimestamp);
                    }
                }
                else
                {
                    sequence = 0L;
                }

                lastTimestamp = timestamp;

                return ((timestamp - Epoch.Ticks / TimeSpan.TicksPerMillisecond) << TimestampLeftShift) | sequence;
            }
        }

        private static long GetCurrentTimestamp()
        {
            return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private static long WaitForNextTimestamp(long lastTimestamp)
        {
            var timestamp = GetCurrentTimestamp();
            while (timestamp <= lastTimestamp)
            {
                timestamp = GetCurrentTimestamp();
            }
            return timestamp;
        }
    }
}
