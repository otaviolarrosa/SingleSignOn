namespace SingleSignOn.Domain.ViewModels
{
    public class ErrorApiViewModel
    {
        public ErrorApiViewModel(string property, string errorMessage)
        {
            Property = property;
            ErrorMessage = errorMessage;
        }

        public string Property { get; set; }
        public string ErrorMessage { get; set; }
    }
}
