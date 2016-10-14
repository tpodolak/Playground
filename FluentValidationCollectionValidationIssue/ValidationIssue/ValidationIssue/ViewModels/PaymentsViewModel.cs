using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Results;

namespace ValidationIssue.ViewModels
{
    [Validator(typeof(PaymentsViewModelValidator))]
    public class PaymentsViewModel : List<PaymentViewModel>
    {
        public PaymentsViewModel()
        {
        }

        public PaymentsViewModel(IEnumerable<PaymentViewModel> payments) : base(payments)
        {
        }

    }

    public class PaymentViewModel
    {
        public string Method { get; set; }
    }

    public class PaymentsViewModelValidator : AbstractValidator<PaymentsViewModel>
    {
        public PaymentsViewModelValidator()
        {
            RuleFor(val => val.Count).InclusiveBetween(1, 2);
            RuleForEach(val => val).SetValidator(new PaymentViewModelValidator()).OverridePropertyName("Items");
        }

        public override ValidationResult Validate(PaymentsViewModel instance)
        {
            return base.Validate(instance);
        }

        public override ValidationResult Validate(ValidationContext<PaymentsViewModel> context)
        {
            return base.Validate(context);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<PaymentsViewModel> context)
        {
            return base.ValidateAsync(context);
        }
    }

    public class PaymentViewModelValidator : AbstractValidator<PaymentViewModel>
    {
        public PaymentViewModelValidator()
        {
            RuleFor(val => val.Method).NotEmpty();
        }

        public override ValidationResult Validate(PaymentViewModel instance)
        {
            return base.Validate(instance);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<PaymentViewModel> context)
        {
            return base.ValidateAsync(context);
        }

        public override ValidationResult Validate(ValidationContext<PaymentViewModel> context)
        {
            return base.Validate(context);
        }
    }
}