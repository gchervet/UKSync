using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Domain
{
    public enum TarjetaEnum
    {
        [Description("TD-Mae")]
        Mae = 1,
        [Description("TD-VisaEle")]
        VisaEle = 2,
        [Description("TC-Master")]
        Master = 3,
        [Description("TC-Visa")]
        Visa = 4
    }
}
