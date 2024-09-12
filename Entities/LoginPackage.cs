using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
            return "LoginPackage:{ id: " + id + ", email: " + email + ", psw: " + psw + " }";
        }
    }
}
