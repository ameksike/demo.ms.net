using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsRFS.src.model
{
    class FtpFileInfo
    {
        public Boolean IsDirectory { get; set; }
        public Char[] Permissions { get; set; }
        public Int32 NrOfInodes { get; set; }
        public String User { get; set; }
        public String Group { get; set; }
        public Int64 FileSize { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public String FileName { get; set; }
    }
}
