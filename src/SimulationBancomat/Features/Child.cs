using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationBancomat.Features
{
     class Child : Account
    {
        public Child(string ownerType, string ownerName, int moneyAmount) 
            : base(ownerType, ownerName, moneyAmount)
        {
            
        }

    }
}
