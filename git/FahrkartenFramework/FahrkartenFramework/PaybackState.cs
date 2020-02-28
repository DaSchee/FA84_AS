using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrkartenFramework
{
    class PaybackState : State
    {
        public PaybackState(Fahrkartenautomat automat) : base(automat) { }

        public override State Handle()
        {
            Payback();
            return new WaitingState(automat);
        }

        private void Payback()
        {
            automat.rückgabebetrag = automat.eingezahlterGesamtbetrag - automat.zuZahlen;
            automat.rückgabebetrag = Fahrkartenautomat.Rückgabe(automat.rückgabebetrag);

            Console.WriteLine("\nVergessen Sie nicht, den Fahrschein\n" +
                              "vor Fahrtantritt stempeln zu lassen!\n" +
                              "Wir wünschen Ihnen eine gute Fahrt.");
            Console.ReadKey();
        }
    }
}
