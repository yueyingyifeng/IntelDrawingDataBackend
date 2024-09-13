using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace IntelDrawingDataBackend.Entities
{
    // 登录包
    public class LoginPackage
    {
        public int? id {  get; set; }
        public string? email {  get; set; }
        [Required]
        public string psw { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
