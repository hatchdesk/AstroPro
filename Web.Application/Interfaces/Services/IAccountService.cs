using Web.Application.ViewModels.Admin.Account;

namespace Web.Application.Interfaces.Services
{
    public  interface IAccountService
    {
        Task<LogInToViewModel?> Authenticate(LogInToCreateViewModel model);

    }
}
