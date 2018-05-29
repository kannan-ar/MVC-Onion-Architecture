namespace iH.UI.Code.Validation
{
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class StringArrayRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] array = value as string[];

            if (array == null || array.Any(item => string.IsNullOrEmpty(item)))
            {
                return new ValidationResult(this.ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}