using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LangrisserHelper.Shared
{
    public class UserRegister
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(16, ErrorMessage ="16자 이하로 하세요")]
        public string UserName { get; set; }
        public string Bio { get; set; }
        [Required, StringLength(100, MinimumLength=6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage ="비밀번호가 일치하지 않습니다.")]
        public string ConfirmedPassword { get; set; }
        [Range(0, 1000)]
        public int Bananas { get; set; } = 100;
        [Range(typeof(bool), "true", "true", ErrorMessage ="Only Confirmed users can play!")]
        public bool IsConfirmed { get; set; }
    }
}
