using Newtonsoft.Json;
using PangeaAssignment.Models;
using System.Text.Json.Nodes;

namespace PangeaAssignment.Validators
{
    public class ValidationHelper : IValidationHelper
    {
        public bool TryConvertToJson(string value, out InputModel? model)
        {
            try
            {
                model = JsonConvert.DeserializeObject<InputModel>(value);
                return true;
            }
            catch
            {
                model = null;
                return false;
            }
        }

        public void ValidateInput(InputModel model)
        {
            if (string.IsNullOrEmpty(model.Input))
            {
                throw new ArgumentNullException("Input Can not be null");
            }
        }

        public void CheckIfInputsAreNull(InputModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Left or Right is Null");
            }
        }
    }
}
