using System;
namespace UnsualLotery.Services.Validations
{
    public class ValidateModelOutput
    {
        public IEnumerable<string> Errors { get; private set; }

        public ValidateModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}

