using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours
{
    [Flags]
    public enum Utility
    {
        None = 0,
        TV = 1,
        Toilet = 2,
        Enternet = 4,
        Socket = 8,
        USB = 16,
    }
}
