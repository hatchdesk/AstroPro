using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces.Repositories;
using Web.Domian.Entities;
using Web.Infrastructure.Repositories.Base;

namespace Web.Infrastructure.Repositories
{
    public class FeeRepositoary : Repository<Fee> , IFeeRepositoary
    {
        private readonly WebDbContext _webDbContext;

        public FeeRepositoary(WebDbContext webDbContext) : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public async Task<Fee?> GetByNameAsync(string name)
        {
                return await _webDbContext.Fees
                    .FirstOrDefaultAsync(p => p.ServiceName == name);
           
        }

		public async Task<Fee?> GetByIdAsync(int id)
		{
			return await _webDbContext.Fees
				.FirstOrDefaultAsync(p => p.Id == id);

		}
	}
}
