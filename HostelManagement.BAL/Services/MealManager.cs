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

        public async Task<int> AddMeal(Meal meal)
        {
            IEnumerable<Student> s = await _da.Student.GetAllAsync();
            if (meal != null)
            {
                IEnumerable<Meal> meals = await _da.Meal.GetAllAsync();
                if (meals.Any(x => x.Id.Equals(meal.Id)))
                {
                    return await Task.FromResult(0);
                }
                else if (!(s.Any(x => x.Id.Equals(meal.StudentId))))
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    _da.Meal.AddAsync(meal);
                    _da.Save();
                    return await Task.FromResult(2);
                }
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await _da.Meal.GetAllAsync();
        }

        public async Task<Meal> GetMealAsync(int id)
        {
            return await _da.Meal.GetFirstOrDefaultAsync(x => x.Id == id);
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