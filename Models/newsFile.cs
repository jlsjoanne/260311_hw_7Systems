using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _260311_hw_7Systems
{
    public class newsFile : IFile
    {
        public string fileName { get; set; }
        public string fileDesc { get; set; }
        public string filePath { get; set; }
        public int newsId { get; set; }
    }
}