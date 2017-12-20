using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.ViewModel
{
    public class OrderNoteViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Note { get; set; }
        public int DownloadId { get; set; }
        public bool DisplayToCustomer { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
