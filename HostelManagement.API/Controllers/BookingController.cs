using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bk;
        public BookingController(IBookingManager bk, ILogger<BookingController> logger)
        {
            _bk = bk; ;
            _logger = logger;
        }


        /// <summary>
        /// This method returns all the Bookings from Booking Table
        /// </summary>
        /// <returns>List of all the Bookings</returns>
        [HttpGet]
        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            _logger.LogInformation("GetAllBookings method is called at " + DateTime.Now);
            return await _bk.GetAllBookingsAsync();
        }




        //Get: api/Booking/{id}
        /// <summary>
        /// This method returns a single Booking that matches wit the Id parameter of a Booking
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Booking that matches with the id</returns>
        [HttpGet("{id}")]
        public async Task<Booking> GetHostel(int id)
        {
            _logger.LogInformation("GetBooking method is called at " + DateTime.Now);
            return await _bk.GetBookingAsync(id);
        }





        //Post: api/Booking
        /// <summary>
        /// This method adds new Booking objects to the Booking table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="bk"></param>
        /// <returns>Output that Booking is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddBooking(Booking bk)
        {
            _logger.LogInformation("AddHostel method is called at " + DateTime.Now);
            try
            {
                if (bk == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    if (await _bk.AddBooking(bk))
                        return StatusCode(StatusCodes.Status201Created, "New Booking is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Booking already exists");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Booking");
            }
        }





        /// <summary>
        /// This method is used to update Booking details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bk"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking booking)
        {
            _logger.LogInformation("UpdateBooking method is called at " + DateTime.Now);
            try
            {
                if (booking == null)
                    return BadRequest();
                else
                {
                    Booking exBooking = await _bk.GetBookingAsync(id);
                    if (exBooking != null)
                    {
                        exBooking.BookingId = booking.BookingId;
                        _bk.UpdateBooking(exBooking);
                        return Ok("Booking is updated");
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the booking");
            }
        }




        /// <summary>
        /// This method is used to remove customer details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
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
                    Booking delBooking = await _bk.GetBookingAsync(id);
                    if (delBooking == null)
                    { return NotFound("ID does not exist"); }
                    _bk.DeleteBooking(delBooking);
                    return Ok("Booking Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Booking Details");
            }

        }
    }
}