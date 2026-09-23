using Gym_Management_System.Business.Services;

namespace Gym_Management_System.BackgroundServices
{
    public class MembershipExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public MembershipExpirationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var membershipService = scope.ServiceProvider
                        .GetRequiredService<MembershipService>();

                    await membershipService.ExpireMembershipsAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
