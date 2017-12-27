using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEB.Core.Domain
{
    public partial class Picture 
    {
        public byte[] PictureBinary { get; set; }
        
        public string MimeType { get; set; }
        
        public string SeoFilename { get; set; }
        
        public string AltAttribute { get; set; }
        
        public string TitleAttribute { get; set; }
        
        public bool IsNew { get; set; }
        public int Id { get; set; }
    }

}
