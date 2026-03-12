using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationBancomat.Features
{
    class Admin : Account
    {
        public Admin(string ownerType, string ownerName, int moneyAmount, string pwd)
        : base(ownerType, ownerName, moneyAmount, pwd)
        {

        }

    }
}
