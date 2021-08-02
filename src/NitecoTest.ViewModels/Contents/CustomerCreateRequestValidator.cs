using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace NitecoTest.ViewModels.Contents
{
    public class CustomerCreateRequestValidator : AbstractValidator<CustomerCreateRequest>
    {
        public CustomerCreateRequestValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Customer Name cannot be empty");
        }
    }
}
