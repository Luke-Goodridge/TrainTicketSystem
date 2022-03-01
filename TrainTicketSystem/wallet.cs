using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Wallet
    {
        private float WalletTotal { get; set; }

        // Let them have 50 in the wallet to begin with
        public Wallet()
        {
            WalletTotal = 50.00f;
        }
        public bool DeductPrice(float price)
        {
            if (WalletTotal - price < 0)
            {
                return false;
            }
            else
            {
                WalletTotal -= price;
                return true;
            }
        }
    }
}
