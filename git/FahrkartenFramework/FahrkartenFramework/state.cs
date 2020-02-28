using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrkartenFramework
{
    abstract class State
    {
        protected Fahrkartenautomat automat;
        public State(Fahrkartenautomat automat)
        {
            this.automat = automat;
        }
        public abstract State Handle();
    }
}
