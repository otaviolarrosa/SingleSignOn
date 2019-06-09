using FluentValidation;
using SingleSignOn.Domain.ViewModels;

namespace SingleSignOn.Domain.Validators
{
    public abstract class BaseValidator<TViewModel> : AbstractValidator<TViewModel> where TViewModel : BaseViewModel
    {
        public void Validate(ref TViewModel model, AbstractValidator<TViewModel> validator)
        {
            model.ValidationResult = validator.Validate(model);
        }
    }
}
