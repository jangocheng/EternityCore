using System;
using System.Threading.Tasks;
using OrchardCore.Recipes.Models;
using OrchardCore.Recipes.Services;

namespace FairyPay.Recipes
{
    public class FairyPayRecipeHandler : IRecipeStepHandler
    {
        public Task ExecuteAsync(RecipeExecutionContext context)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
