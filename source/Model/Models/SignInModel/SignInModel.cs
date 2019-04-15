using System.ComponentModel.DataAnnotations;

namespace DotNetCoreArchitecture.Model
{
    public class SignInModel
    {
        [Required]
        [StringLength(6)]
        public string Login { get; set; }

        [Required]
        [StringLength(6)]
        public string Password { get; set; }
    }
}
