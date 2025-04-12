using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
        {
            try
            {
                ResponseRegisteredExpenseJson response = new RegisterExpenseUseCase().Execute(request);

                return Created(string.Empty, response);
            }
            catch (ArgumentException ex)
            {
                ResponseErrorJson errorMessage = new ResponseErrorJson(ex.Message);

                return BadRequest(errorMessage);
            }
            catch
            {
                ResponseErrorJson errorMessage = new ResponseErrorJson("Unknown error");

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
    }
}
