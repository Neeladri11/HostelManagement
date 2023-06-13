using AutoMapper;
using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentManager _pm;
        private readonly IMapper _mapper;
        public PaymentController(IMapper mapper, IPaymentManager pm, ILogger<PaymentController> logger)
        {
            _pm = pm; ;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all hostels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PaymentVM>> GetAllPayments()
        {
            _logger.LogInformation("GetAllPayments method is called at " + DateTime.Now);
            IEnumerable<Payment> payments = await _pm.GetAllPaymentsAsync();
            var pvmList = payments.Select(payments => _mapper.Map<PaymentVM>(payments));
            return pvmList;
        }

        [HttpGet("{id}")]
        public async Task<PaymentVM> GetPayment(int id)
        {
            _logger.LogInformation("GetPayment method is called at " + DateTime.Now);
            
            Payment payment = await _pm.GetPaymentAsync(id);
            var pvm = _mapper.Map<PaymentVM>(payment);
            return pvm;
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentVM pvm)
        {
            _logger.LogInformation("AddPayment method is called at " + DateTime.Now);
            
            try
            {
                if (pvm == null)
                    return BadRequest();
                else
                {
                    Payment payment = _mapper.Map<Payment>(pvm);
                    var check = await _pm.AddPayment(payment);
                    if (check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Payment already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign key values are not correct");
                    else if (check == -1)
                        return StatusCode(StatusCodes.Status400BadRequest, "The Payment object entered is empty");
                    else
                        return StatusCode(StatusCodes.Status201Created, "New Payment is created");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Payment");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentVM pvm)
        {
            _logger.LogInformation("UpdateHostel method is called at " + DateTime.Now);
            
            try
            {
                if (pvm == null)
                    return BadRequest();
                else
                {
                    Payment exPayment = await _pm.GetPaymentAsync(id);
                    if (exPayment == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    Payment payment = _mapper.Map<PaymentVM, Payment>(pvm, exPayment);
                    _pm.UpdatePayment(payment);
                    return Ok("Payment is updated");
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