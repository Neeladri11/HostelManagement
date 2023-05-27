using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentManager _pm;
        public PaymentController(IPaymentManager pm, ILogger<PaymentController> logger)
        {
            _pm = pm; ;
            _logger = logger;
        }

        /// <summary>
        /// Method to get all hostels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            _logger.LogInformation("GetAllPayments method is called at " + DateTime.Now);
            return await _pm.GetAllPaymentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Payment> GetPayment(int id)
        {
            _logger.LogInformation("GetPayment method is called at " + DateTime.Now);
            return await _pm.GetPaymentAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(Payment payment)
        {
            _logger.LogInformation("AddPayment method is called at " + DateTime.Now);
            try
            {
                if (payment == null)
                    return BadRequest();
                else
                {
                    if (await _pm.AddPayment(payment))
                        return StatusCode(StatusCodes.Status201Created, "New payment is created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Payment is already available");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Payment");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
        {
            _logger.LogInformation("UpdateHostel method is called at " + DateTime.Now);
            try
            {
                if (payment == null)
                    return BadRequest();
                else
                {
                    Payment exPayment = await _pm.GetPaymentAsync(id);
                    if (exPayment != null)
                    {
                        exPayment.PaymentId = payment.PaymentId;
                        _pm.UpdatePayment(exPayment);
                        return Ok("Payment is updated");
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the payment");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                if (id == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Payment delPayment = await _pm.GetPaymentAsync(id);
                    if (delPayment == null)
                    { return NotFound("ID does not exist"); }
                    _pm.DeletePayment(delPayment);
                    return Ok("Payment Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Payment Details");
            }

        }
    }
}