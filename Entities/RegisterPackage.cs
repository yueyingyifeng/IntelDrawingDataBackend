using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    // 注册包
    public class RegisterPackage
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Psw { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
