using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using System.Linq;

namespace SingleSignOn.Domain.ViewModels
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
            ValidationResult = new ValidationResult();
        }

        [JsonIgnore]
        public string Id { get; set; }

        [JsonIgnore]
        public bool Valid { get => ValidationResult.Errors.Any() ? false : true; private set { } }

        [JsonIgnore]
        public bool Invalid => !Valid;

        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}
