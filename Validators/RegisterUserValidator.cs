using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using server_new_try.DTOs;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    ///Validation class for the RegisterUserDto
    {

        public RegisterUserValidator()
        {


            RuleFor(x => x.Username) //set of rules for  the username
                .NotNull()
                .WithMessage("Must not be null")
                .MinimumLength(4)
                .WithMessage("Must have minimum 4 characters")
                .Must(Start)
                .WithMessage("Must start with a letter");                

            RuleFor(x => x.Email) ///set of rules for  the email
                .NotNull()
                .WithMessage("Must not be null")
                .EmailAddress()
                .WithMessage("Must be a valid email address");

            RuleFor(x  =>x.Password) //set of rules for the password
                .NotNull()
                .WithMessage("Must not be null")
                .MinimumLength(4)
                .WithMessage("Must have minimum 4 characters");

                       
        }

        public bool Start(String Username) => char.IsLetter(Username[0]) ? true : false ;
        
    
    }
}