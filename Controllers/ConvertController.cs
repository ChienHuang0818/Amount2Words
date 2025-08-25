using Microsoft.AspNetCore.Mvc;
using NumberToWordsApp.Models;

namespace NumberToWordsApp.Controllers
{
    public class ConvertController : Controller{

        // When the user first visit the page, the page will be initialized with an empty form 
        [HttpGet]
        public IActionResult Index(){

            var model = new ConvertViewModel();
            return View(model);
            
        }

        // When the user submit the form, the page will be updated with the result
        [HttpPost]
        public IActionResult Index(ConvertViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.NumberInput) && decimal.TryParse(model.NumberInput, out decimal value))
            {
                model.Result = NumberToWordsConverter.Convert(value);
                model.HasError = false;
            }
            else
            {
                model.HasError = true;
                model.ErrorMessage = "please enter a valid number";
                model.Result = "";
            }
            return View(model);
        }
    }
}
