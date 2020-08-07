using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using Entity_Task;

namespace Business_Task
{
    public class B_Reservation
    {
        dac objdac = new dac();

        public String IUD_Reservation(E_Reservation objReservation, SqlConnection mycon, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IUD_Reservation";
            cmd.Connection = mycon;

            cmd.Parameters.Add("@Reservation_ID", SqlDbType.BigInt).Value = objReservation.Reservation_ID;
            cmd.Parameters.Add("@Room_ID", SqlDbType.BigInt).Value = objReservation.Room_ID;
            cmd.Parameters.Add("@UserID", SqlDbType.BigInt).Value = objReservation.UserID;
            cmd.Parameters.Add("@No_of_Guest", SqlDbType.Decimal).Value = objReservation.No_of_Guest;
            cmd.Parameters.Add("@From", SqlDbType.Date).Value = objReservation.From;
            cmd.Parameters.Add("@To", SqlDbType.Date).Value = objReservation.To;
            cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = objReservation.Action;

            cmd.Transaction = trans;
            String i = objdac.iexecute(cmd, mycon);
            return i;
        }

        public DataTable Load_Reservation(Int32 RoomNo, Int32 Floor,string From, string To)
        {
            SqlCommand cmd = new SqlCommand("Exec Load_Reservation '" + RoomNo + "','" + Floor + "','" + From + "','" + To + "'");
            DataSet ds = new DataSet();
            ds = objdac.select(cmd);
            return ds.Tables[0];
        }
    }
}
