using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BillAutomation
{
    public class ElectricityBill
    {
        private string consumerNumber;
        public string ConsumerNumber
        {
            get { return consumerNumber; }
            set { consumerNumber = value; }
        }

        private string consumerName;
        public string ConsumerName
        {
            get { return consumerName; }
            set { consumerName = value; }
        }

        private int unitsConsumed;
        public int UnitsConsumed
        {
            get { return unitsConsumed; }
            set { unitsConsumed = value; }
        }

        private double billAmount;
        public double BillAmount
        {
            get { return billAmount; }
            set { billAmount = value; }
        }

        public ElectricityBill()
        {
        }

        public ElectricityBill(string consumerNumber, string consumerName, int unitsConsumed, double billAmount)
        {
            ConsumerNumber = consumerNumber;
            ConsumerName = consumerName;
            UnitsConsumed = unitsConsumed;
            BillAmount = billAmount;
        }

        public override string ToString()
        {
            return $"{ConsumerNumber}\n{ConsumerName}\n{UnitsConsumed}\nBill Amount:{BillAmount}".ToString();
        }
    }
}
