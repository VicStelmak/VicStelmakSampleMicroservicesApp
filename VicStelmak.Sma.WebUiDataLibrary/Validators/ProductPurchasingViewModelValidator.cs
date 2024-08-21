using FluentValidation;
using VicStelmak.Sma.WebUiDataLibrary.ViewModels;

namespace VicStelmak.Sma.WebUiDataLibrary.Validators
{
    public class ProductPurchasingViewModelValidator : AbstractValidator<ProductPurchasingViewModel>
    {
        public ProductPurchasingViewModelValidator(int amountInStock)
        {
            RuleFor(viewModel => viewModel.AmountToPurchase)
                .GreaterThan(0).WithMessage("{PropertyName} can't be equal to zero or be less than zero.")
                .LessThanOrEqualTo(amountInStock).WithMessage("{PropertyName} can't be greater than amount in stock.").WithName("Amount");
            RuleFor(viewModel => viewModel.Apartment)
                .GreaterThan(0).WithMessage("{PropertyName} can't be equal to zero or be less than zero.");
            RuleFor(viewModel => viewModel.Building)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(viewModel => viewModel.City)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(viewModel => viewModel.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(viewModel => viewModel.PostalCode)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(viewModel => viewModel.Street)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}