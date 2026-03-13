using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _260311_hw_7Systems
{
    internal interface IFile
    {
        string fileName { get; set; }
        string fileDesc { get; set; }
        string filePath { get; set; }
    }
}
