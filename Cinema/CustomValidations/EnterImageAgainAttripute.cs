using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Cinema.CustomValidations
{
    public class EnterImageAgainAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Please Enter the Images Again";
        }

        public override bool IsValid(object? value)
        {
            return value switch
            {
                null => false,
                string s => !string.IsNullOrWhiteSpace(s),
                IEnumerable e => e.GetEnumerator().MoveNext(), // at least one item
                _ => true
            };
        }
    }
}