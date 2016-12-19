using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Web.Models.AccountViewModels
{
    public class IndexViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public Setting.Language PreferedLanguage { get; set; }
        [Required]
        public string Email { get; set; }
        public string GameDirectory { get; set; }
        public Setting.Theme PreferedTheme { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
