using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.ViewModels.Admin.Fee
{
    public class FeeToViewModel
    {

        public int Id { get; set; }

        public string? ServiceName { get; set; }

        public decimal Amount { get; set; }
  
    }
}
