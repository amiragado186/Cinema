using System.ComponentModel.DataAnnotations;

namespace Cinema.CustomValidations
{
    public class MinMaxLengthCustomAttribute : ValidationAttribute
    {
        public int Min { get; }
        public int Max { get; }

        public MinMaxLengthCustomAttribute(int min = 2, int max = 50)
        {
            Min = min;
            Max = max;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"The length of {name} must be between [{Min},{Max}]"
                : string.Format(ErrorMessageString, name, Min, Max);
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
                return true; // let [Required] handle nulls

            return value is string s && s.Length >= Min && s.Length <= Max;
        }
    }
}