using HostelManagement.BAL.Contracts;
using HostelManagement.DAL.DataAccess.Interface;
using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Services
{
    public class MealManager : IMealManager
    {
        private readonly IDataAccess _da;
        public MealManager(IDataAccess da)
        {
            _da = da;
        }

        public async Task<bool> AddMeal(Meal meal)
        {
            if (meal != null)
            {
                IEnumerable<Meal> meals = await _da.Meal.GetAllAsync();
                if (meals.Any(x => x.MealId.Equals(meal.MealId)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var m = new Meal();
                    m.MealId = meal.MealId;
                    _da.Meal.AddAsync(m);
                    _da.Save();
                }
            }
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await _da.Meal.GetAllAsync();
        }

        public async Task<Meal> GetMealAsync(int MealId)
        {
            return await _da.Meal.GetFirstOrDefaultAsync(x => x.MealId == MealId);
        }

        public void UpdateMeal(Meal meal)
        {
            _da.Meal.UpdateExisting(meal);
            _da.Save();
        }

        public void DeleteMeal(Meal meal)
        {
            _da.Meal.Remove(meal);
            _da.Save();
        }
    }
}
