using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrkartenFramework
{
    class PrintingState : State
    {
        public PrintingState(Fahrkartenautomat automat) : base(automat) { }

        public override State Handle()
        {
            Fahrkartenautomat.FahrscheinDrucken();
            return new PaybackState(automat);
        }
    }
}
