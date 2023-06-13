using AutoMapper;
using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bk;
        private readonly IMapper _mapper;
        public BookingController(IMapper mapper,IBookingManager bk, ILogger<BookingController> logger)
        {
            _bk = bk; ;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// This method returns all the Bookings from Booking Table
        /// </summary>
        /// <returns>List of all the Bookings</returns>
        [HttpGet]
        public async Task<IEnumerable<BookingVM>> GetAllBookings()
        {
            _logger.LogInformation("GetAllBookings method is called at " + DateTime.Now);
            
            IEnumerable<Booking> bookings = await _bk.GetAllBookingsAsync();
            var bvmList = bookings.Select(bookings => _mapper.Map<BookingVM>(bookings));
            return bvmList;
        }




        //Get: api/Booking/{id}
        /// <summary>
        /// This method returns a single Booking that matches wit the Id parameter of a Booking
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Booking that matches with the id</returns>
        [HttpGet("{id}")]
        public async Task<BookingVM> GetHostel(int id)
        {
            _logger.LogInformation("GetBooking method is called at " + DateTime.Now);
            
            Booking booking = await _bk.GetBookingAsync(id);
            var bvm = _mapper.Map<BookingVM>(booking);
            return bvm;
        }





        //Post: api/Booking
        /// <summary>
        /// This method adds new Booking objects to the Booking table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="bk"></param>
        /// <returns>Output that Booking is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddBooking(BookingVM bvm)
        {
            _logger.LogInformation("AddHostel method is called at " + DateTime.Now);
            try
            {
                if (bvm == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Booking booking = _mapper.Map<Booking>(bvm);
                    var check = await _bk.AddBooking(booking);
                    if (check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Booking already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign key values are not correct");
                    else if (check == -1)
                        return StatusCode(StatusCodes.Status400BadRequest, "The Booking object entered is empty");
                    else
                        return StatusCode(StatusCodes.Status201Created, "New Booking is created");
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
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingVM bvm)
        {
            _logger.LogInformation("UpdateBooking method is called at " + DateTime.Now);
            
            try
            {
                if (bvm == null)
                    return BadRequest();
                else
                {
                    Booking exBooking = await _bk.GetBookingAsync(id);
                    if (exBooking == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    Booking booking = _mapper.Map<BookingVM, Booking>(bvm, exBooking);
                    _bk.UpdateBooking(booking);
                    return Ok("Booking is updated");
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