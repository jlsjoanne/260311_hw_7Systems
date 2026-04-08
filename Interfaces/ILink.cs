using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _260311_hw_7Systems.Interfaces
{
    internal interface ILink
    {
        string linkName { get; set; }
        string linkUrl { get; set; }
        bool isNewPage { get; set; }
    }
}
