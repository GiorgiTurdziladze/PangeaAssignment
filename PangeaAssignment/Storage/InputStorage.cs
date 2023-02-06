using PangeaAssignment.Models;

namespace PangeaAssignment.Storage
{
    public class InputStorage : IInputStorage
    {

        private InputModel _input1;
        private InputModel _input2;

        public Tuple<InputModel, InputModel> GetInputPiar()
        {
            return Tuple.Create(_input1, _input2);
        }

        public InputModel Input1 { get; set; }
        public InputModel Input2 { get; set; }
    }
}
