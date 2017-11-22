using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Infrastructure
{
    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly IDbContext _dbContext;

        public DbContextTransactionFilter(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var transaction = await _dbContext.BeginTransactionAsync())
            {
                await next();

                await _dbContext.SaveChangesAsync();

                transaction.Commit();
            }
        }
    }
}
