using System;
using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators
{

    public class LocationValidator:AbstractValidator<LocationModel>
    {
        public LocationValidator()
        {
            RuleFor(x =>x.Latitude)
                .NotNull()
                .WithMessage("Must not be null")
                .ScalePrecision(2, 15)
                .WithMessage("Latitude must be a double");

            RuleFor(x =>x.Longitude)
                .NotNull()
                .WithMessage("Must not be null")
                .ScalePrecision(2, 15)
                .WithMessage("Longitude must be a double");
            
            RuleFor(x => x.StartDate)
                .NotNull()
                .WithMessage("Must not be null")
                .Must(BeAValidDate)
                .WithMessage("Must be a valid date!");
            
            RuleFor(x => x.EndDate)
                .NotNull()
                .WithMessage("Must not be null")
                .Must(BeAValidDate)
                .WithMessage("Must be a valid date!");

        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }

}