using PangeaAssignment.Models;

namespace PangeaAssignment.Storage
{
    public interface IInputStorage
    {
        Tuple<InputModel, InputModel> GetInputPiar();
        InputModel Input1 { get; set; }
        InputModel Input2 { get; set; }
    }
}
