using Gym_Management_System.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private readonly PaymentService _paymentService;
        public PaymentsController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet("Member/{id}/Payments")]



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPaymentsByMemberID(int id)
        {
            var Payments = await _paymentService.GetAllPaymentsByMemebrIDAsync(id);

            if (Payments.Count == 0)
                return NotFound($"Member {id} has no payments.");

            return Ok(Payments);

        }


        [HttpGet("GetAllPayments")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPayments()
        {
            var Payments = await _paymentService.GetAllPaymentsAsync();

            if (Payments.Count == 0)
                return NotFound($"no payments.");

            return Ok(Payments);

        }


    }
}
