using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketSystem
{
    internal class Wallet
    {
        // Give the user 50 to spend
        private static float walletTotal = 50.00f;
        public static float WalletTotal
        {
            get
            {
                return walletTotal;
            }
            set
            {
                walletTotal = value;
            }
        }

        public static bool DeductPrice(float price)
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
