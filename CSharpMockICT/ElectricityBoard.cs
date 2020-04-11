using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillAutomation
{
    public class ElectricityBoard
    {
        public SqlConnection SqlCon { get; set; }

        public ElectricityBoard()
        {
            DBHandler dBHandler = new DBHandler();
            SqlCon = dBHandler.GetConnection();
        }


        public void AddBill(ElectricityBill ebill)
        {
            SqlCon.Open();

            SqlCommand command = new SqlCommand("insert into ElectricityBill values(@conNumber,@conName,@unitsCon,@billAmount)", SqlCon);

            command.Parameters.AddWithValue("@conNumber", ebill.ConsumerNumber);
            command.Parameters.AddWithValue("@conName", ebill.ConsumerName);
            command.Parameters.AddWithValue("@unitsCon", ebill.UnitsConsumed);
            command.Parameters.AddWithValue("@billAmount", ebill.BillAmount);
            command.ExecuteNonQuery();
        }

        public void CalculateBill(ElectricityBill ebill)
        {
            if (ebill.UnitsConsumed <= 100)
            {
                ebill.BillAmount = 0;
            }
            else if (ebill.UnitsConsumed > 100 && ebill.UnitsConsumed <= 300)
            {
                ebill.BillAmount = 1.5 * (ebill.UnitsConsumed - 100);
            }
            else if (ebill.UnitsConsumed > 300 && ebill.UnitsConsumed <= 600)
            {
                ebill.BillAmount = 1.5 * (200) +
                                    3.5 * (ebill.UnitsConsumed - 300);
            }
            else if (ebill.UnitsConsumed > 600 && ebill.UnitsConsumed <= 1000)
            {
                ebill.BillAmount = 1.5 * (200) +
                                   3.5 * (300) +
                                   5.5 * (ebill.UnitsConsumed - 600);
            }
            else if (ebill.UnitsConsumed > 1000)
            {
                ebill.BillAmount = 1.5 * (200) +
                                   3.5 * (300) +
                                   5.5 * (400) +
                                   7.5 * (ebill.UnitsConsumed - 1000);
            }
        }

        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            SqlCon.Open();
            SqlCommand command = new SqlCommand("select * from ElectricityBill", SqlCon);
            SqlDataReader reader = command.ExecuteReader();
            List<ElectricityBill> electricityBills = new List<ElectricityBill>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ElectricityBill eBill = new ElectricityBill();
                    eBill.ConsumerNumber = reader.GetString(0);
                    eBill.ConsumerName = reader.GetString(1);
                    eBill.UnitsConsumed = reader.GetInt32(2);
                    eBill.BillAmount = reader.GetDouble(3);
                    electricityBills.Add(eBill);
                }
            }
            electricityBills = electricityBills.Skip(electricityBills.Count - num).ToList();
            return electricityBills;
        }
    }
}
