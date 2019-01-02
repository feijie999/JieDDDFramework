using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Identity.API.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("地址不能为空")
                .EmailAddress().WithMessage("请输入正确Email地址").WithErrorCode("-123");

            RuleFor(x => x.Password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}
