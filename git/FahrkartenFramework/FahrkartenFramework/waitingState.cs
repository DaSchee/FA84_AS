using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrkartenFramework
{
    internal class WaitingState : State
    {
        public WaitingState(Fahrkartenautomat automat) : base(automat)
        {
        }
        public override State Handle()
        {
            automat.Reset();
            SelectTicket();
            return new AcceptingState(automat);
        }

        private void SelectTicket()
        {
            while (automat.zuZahlen == 0)
            {
                automat.zuZahlen = Fahrkartenautomat.Eingabe();
            }
        }
    }
}
