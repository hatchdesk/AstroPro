using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.ViewModels.Admin.Service
{
    public class ServiceToViewModel
    {
        public int Id { get; set; }
        public string? Icon { get; set; }
        public string Title { get; set; } = string.Empty;
		public string FeeText { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
    }
}
