using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Web.Domian.Entities
{
	public  class CaptchaResponse
	{
		[JsonProperty("success")]
		public bool Success { get; set; }
	}
}
