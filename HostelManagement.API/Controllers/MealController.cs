using AutoMapper;
using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using HostelManagement.DAL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly ILogger<MealController> _logger;
        private readonly IMealManager _mm;
        private readonly IMapper _mapper;
        public MealController(IMapper mapper, IMealManager mm, ILogger<MealController> logger)
        {
            _mm = mm; ;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to get all meals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<MealVM>> GetAllMeals()
        {
            _logger.LogInformation("GetAllMeals method is called at " + DateTime.Now);
            
            IEnumerable<Meal> meals = await _mm.GetAllMealsAsync();
            var mvmList = meals.Select(meals => _mapper.Map<MealVM>(meals));
            return mvmList;
        }

        [HttpGet("{id}")]
        public async Task<MealVM> GetMeal(int id)
        {
            _logger.LogInformation("GetMeal method is called at " + DateTime.Now);
            
            Meal meal = await _mm.GetMealAsync(id);
            var mvm = _mapper.Map<MealVM>(meal);
            return mvm;
        }

        [HttpPost]
        public async Task<IActionResult> AddMeal(MealVM mvm)
        {
            _logger.LogInformation("AddMeal method is called at " + DateTime.Now);
            
            try
            {
                if (mvm == null)
                    return BadRequest();
                else
                {
                    Meal meal = _mapper.Map<Meal>(mvm);
                    var check = await _mm.AddMeal(meal);
                    if (check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Meal already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign key values are not correct");
                    else if (check == -1)
                        return StatusCode(StatusCodes.Status400BadRequest, "The Meal object entered is empty");
                    else
                        return StatusCode(StatusCodes.Status201Created, "New Meal is created");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Meal");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeal(int id, [FromBody] MealVM mvm)
        {
            _logger.LogInformation("UpdateMeal method is called at " + DateTime.Now);
            
            try
            {
                if (mvm == null)
                    return BadRequest();
                else
                {
                    Meal exMeal = await _mm.GetMealAsync(id);
                    if (exMeal == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    Meal meal = _mapper.Map<MealVM, Meal>(mvm, exMeal);
                    _mm.UpdateMeal(meal);
                    return Ok("Meal is updated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the meal");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
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
                    Meal delMeal = await _mm.GetMealAsync(id);
                    if (delMeal == null)
                    { return NotFound("ID does not exist"); }
                    _mm.DeleteMeal(delMeal);
                    return Ok("Meal Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Meal Details");
            }

        }
    }
}