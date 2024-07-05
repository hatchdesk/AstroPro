using Web.Application.Interfaces;
using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Account;

namespace Web.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IAccountRepository accountRepository , IUnitOfWork unitOfWork)
        {
            this.accountRepository = accountRepository;
            this._unitOfWork = unitOfWork;
            
        }

        public async Task<LogInToViewModel?> Authenticate(LogInToCreateViewModel model)
        {
            var admin  = await accountRepository.FindAsync(x=>x.Username == model.Username && x.Password == model.Password);
            if (admin == null) return null;
            return new LogInToViewModel
                {
                    Id = admin.Id,
                    Username = admin.Username,
                };           
        }
    }
}
