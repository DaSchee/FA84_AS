using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrkartenFramework
{
    class AcceptingState : State
    {
        public AcceptingState(Fahrkartenautomat automat) : base(automat) { }

        public override State Handle()
        {
            Pay();
            return new PrintingState(automat);
        }

        private void Pay()
        {
            while (automat.eingezahlterGesamtbetrag < automat.zuZahlen)
            {
                automat.eingeworfeneMünze = Fahrkartenautomat.Münzeinwurf(automat.zuZahlen, automat.eingezahlterGesamtbetrag);
                automat.eingezahlterGesamtbetrag += automat.eingeworfeneMünze;
            }
        }
    }
}
