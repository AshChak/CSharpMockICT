using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace BillAutomation
{
    public class Program
    {

        static void Main(string[] args)
        {
            try
            {
                //Implement the code here
                Console.WriteLine("Enter Number of Bills To Be Added : ");
                int number = int.Parse(Console.ReadLine());
                string conNumber = String.Empty;
                string conName = String.Empty;
                int unitsCon;
                string pattern = "^EB[0-9]{5}$";
                BillValidator billValidator = new BillValidator();

                for (int i = 0; i < number; i++)
                {
                    Console.WriteLine("Enter Consumer Number:");
                    conNumber = Console.ReadLine();
                    if (!Regex.IsMatch(conNumber, pattern))
                    {
                        throw new FormatException("Invalid Consumer Number");
                    }

                    Console.WriteLine("Enter Consumer Name:");
                    conName = Console.ReadLine();

                    Console.WriteLine("Enter Units Consumed:");
                    unitsCon = int.Parse(Console.ReadLine());

                    while (billValidator.ValidateUnitsConsumed(unitsCon) == "Given units is invalid")
                    {
                        Console.WriteLine("Given units is invalid");
                        Console.WriteLine("Enter Units Consumed:");
                        unitsCon = int.Parse(Console.ReadLine());
                    }

                    ElectricityBill bill = new ElectricityBill(conNumber, conName, unitsCon, 0);

                    ElectricityBoard board = new ElectricityBoard();

                    board.CalculateBill(bill);

                    board.AddBill(bill);
                }
                Console.WriteLine("Enter Last 'N' Number of Bills To Generate:");
                int num = int.Parse(Console.ReadLine());


                ElectricityBoard electricityBoard = new ElectricityBoard();
                List<ElectricityBill> electricityBills = electricityBoard.Generate_N_BillDetails(num);

                foreach (ElectricityBill bill in electricityBills)
                {
                    Console.WriteLine(bill);
                }

                foreach (ElectricityBill bill in electricityBills)
                {
                    Console.WriteLine($"EB Bill for {bill.ConsumerName} is {bill.BillAmount}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    public class BillValidator
    {
        public String ValidateUnitsConsumed(int UnitsConsumed)
        {
            //Implement code here
            if (UnitsConsumed < 0)
            {
                return "Given units is invalid";
            }
            return "";

        }
    }
}
