using HostelManagement.DAL.Models;

namespace HostelManagement.BAL.Contracts
{
    public interface IMealManager
    {
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal> GetMealAsync(int id);
        Task<bool> AddMeal(Meal meal);
        void UpdateMeal(Meal meal);
        void DeleteMeal(Meal meal);
    }
}
