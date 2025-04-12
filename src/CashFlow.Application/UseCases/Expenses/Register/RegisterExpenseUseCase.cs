using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            return new ResponseRegisteredExpenseJson();
        }

        public void Validate(RequestRegisterExpenseJson request)
        {
            Boolean titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);

            if (titleIsEmpty)
            {
                throw new ArgumentException("The title is required.");
            }

            int result = DateTime.Compare(request.Date, DateTime.UtcNow);

            if (result > 0)
            {
                throw new ArgumentException("Expenses cannot be for the future.");
            }

            if (request.Amount <= 0)
            {
                throw new ArgumentException("The Amount must be greater than zero.");
            }

            Boolean paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);

            if (!paymentTypeIsValid)
            {
                throw new ArgumentException("Payment type is not valid.");
            }
        }
    }
}
