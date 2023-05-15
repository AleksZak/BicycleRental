using BicycleRental.Bussines.Services.Interfaces;

namespace BicycleRental.Bussines.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? null;

                return Guid.TryParse(user, out var userId) ? userId : new Guid();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                var userIdentity = _httpContextAccessor.HttpContext!.User.Identity;
                var isAuthenticated = userIdentity != null && userIdentity.IsAuthenticated;
                return isAuthenticated;
            }
        }
    }
}
