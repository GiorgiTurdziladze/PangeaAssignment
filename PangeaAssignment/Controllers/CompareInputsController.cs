using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PangeaAssignment.Models;
using PangeaAssignment.Services;
using PangeaAssignment.Storage;
using PangeaAssignment.Validators;
using System.Net;
using System.Text.Json;

namespace PangeaAssignment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompareInputsController : Controller
    {
        private readonly IDecoderService _decoderService;
        private readonly IInputStorage _inputStorage;
        private readonly IValidationHelper _validatorHelper;
        public CompareInputsController(IDecoderService decoderService, IInputStorage inputStorage, IValidationHelper validator)
        {
            _decoderService = decoderService;
            _inputStorage = inputStorage;
            _validatorHelper = validator;
        }

        [HttpPost]
        public IActionResult Left([FromBody] string input)
        {
            var jsonInput = _decoderService.Base64Decode(input);

            InputModel? model;

            var result = _validatorHelper.TryConvertToJson(jsonInput, out model);

            if (!result || model == null)
                throw new ArgumentException("Provided Input is not valid JSON format");

            _validatorHelper.ValidateInput(model); 

            _inputStorage.Input1 = model;

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Right([FromBody] string input)
        {
            var jsonInput = _decoderService.Base64Decode(input);

            InputModel? model;

            var result = _validatorHelper.TryConvertToJson(jsonInput, out model);

            if (!result || model == null)
                throw new ArgumentException("Provided Input is not valid JSON format");

            _validatorHelper.ValidateInput(model);

            _inputStorage.Input2 = model;

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Compare()
        {
            var resultModel = new ResultModel();

            _validatorHelper.CheckIfInputsAreNull(_inputStorage.Input1);
            _validatorHelper.CheckIfInputsAreNull(_inputStorage.Input2);

            var test1 = _inputStorage.Input1;
            var test2 = _inputStorage.Input2;


            var result = "";

            if (test1.Input == test2.Input)
                result = "they are equal";

            else if (test1.Input.Length != test2.Input.Length)
                result = "they are different in size";

            else
            {
                List<string> diff;
                IEnumerable<string> set1 = test1.Input.Split(' ').Distinct();
                IEnumerable<string> set2 = test2.Input.Split(' ').Distinct();

                if (set2.Count() > set1.Count())
                    diff = set2.Except(set1).ToList();
                else
                    diff = set1.Except(set2).ToList();

                result = $"offset: {diff[0]}, size :{diff.Count}";
            }
            resultModel.ResultMessage= result;
            return Ok(resultModel);
        }
    }
}

