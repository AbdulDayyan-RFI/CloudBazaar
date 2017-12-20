using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public class CustomerPassword
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
