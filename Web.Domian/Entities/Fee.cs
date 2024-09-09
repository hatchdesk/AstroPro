using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Domian.Entities
{
    public class Fee

    {
        public int Id { get; set; }

        public string  ? ServiceName { get; set; }

        public decimal Amount { get; set; }

        public string ? Text { get; set; }

    }
}
