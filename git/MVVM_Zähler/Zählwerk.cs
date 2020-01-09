using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zähler
{
    class Zählwerk
    {
        private sbyte zählerstand;

        public sbyte Zählerstand 
        {
            get
            {
                return zählerstand;
            }
            set
            {
                zählerstand = value;
            } 
        }

        //public delegate void ÜberlaufEventHandler();
        //public event ÜberlaufEventHandler Überlauf;
        public event Action Überlauf;

        public Zählwerk(sbyte anfangswert)
        {
            // TODO: Complete member initialization
            this.Zählerstand = anfangswert;
        }

        /*public sbyte Zähle()
        {
            sbyte alt = zählerstand;
            zählerstand += 1;
            return zählerstand;
        }*/

        public sbyte Zähle(sbyte differenz)
        {
            if (differenz < 0 && zählerstand + differenz <= 0)
            {
                zählerstand = 0;
                return zählerstand;
            }
            if (differenz > 0 && zählerstand + differenz >= sbyte.MaxValue)
            {
                zählerstand = sbyte.MaxValue;
                return zählerstand;
            }
            zählerstand += differenz;
            return zählerstand;

        }
    }
}
