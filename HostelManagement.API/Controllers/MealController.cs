using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HostelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly ILogger<MealController> _logger;
        private readonly IMealManager _mm;
        public MealController(IMealManager mm, ILogger<MealController> logger)
        {
            _mm = mm; ;
            _logger = logger;
        }

        /// <summary>
        /// Method to get all meals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Meal>> GetAllMeals()
        {
            _logger.LogInformation("GetAllMeals method is called at " + DateTime.Now);
            return await _mm.GetAllMealsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Meal> GetMeal(int mealId)
        {
            _logger.LogInformation("GetMeal method is called at " + DateTime.Now);
            return await _mm.GetMealAsync(mealId);
        }

        [HttpPost]
        public async Task<IActionResult> AddMeal(Meal meal)
        {
            _logger.LogInformation("AddMeal method is called at " + DateTime.Now);
            try
            {
                if (meal == null)
                    return BadRequest();
                else
                {
                    if (await _mm.AddMeal(meal))
                        return StatusCode(StatusCodes.Status201Created, "New meal is created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Meal is already available");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error is Adding a New Meal");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeal(int mealId, [FromBody] Meal meal)
        {
            _logger.LogInformation("UpdateMeal method is called at " + DateTime.Now);
            try
            {
                if (meal == null)
                    return BadRequest();
                else
                {
                    Meal exMeal = await _mm.GetMealAsync(mealId);
                    if (exMeal != null)
                    {
                        exMeal.MealId = meal.MealId;
                        _mm.UpdateMeal(exMeal);
                        return Ok("Meal is updated");
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating the meal");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            try
            {
                if (mealId == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Meal delMeal = await _mm.GetMealAsync(mealId);
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