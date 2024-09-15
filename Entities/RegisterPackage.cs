using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    // 注册包
    public class RegisterPackage
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Psw { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
