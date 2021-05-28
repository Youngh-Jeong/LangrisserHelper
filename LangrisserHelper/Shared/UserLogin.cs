using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LangrisserHelper.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage ="이름을 입력하세요 제발")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="비밀번호를 입력하세요 제발")]
        public string Password { get; set; }

    }
}
