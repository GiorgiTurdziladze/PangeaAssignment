using PangeaAssignment.Models;

namespace PangeaAssignment.Validators
{
    public interface IValidationHelper
    {
        bool TryConvertToJson(string value, out InputModel? model);
        void ValidateInput(InputModel model);
    }
}
