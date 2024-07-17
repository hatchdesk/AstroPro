using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domian.Entities;

namespace Web.Application.Interfaces.Repositories
{
	public interface IConsultationRepository
	{

		Task<Page?> GetByNameAsync(string name);

	}
}
