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
    public class B_Room
    {
        dac objdac = new dac();

        public String IUD_Room(E_Room objRoom, SqlConnection mycon, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IUD_Room";
            cmd.Connection = mycon;

            cmd.Parameters.Add("@Room_No", SqlDbType.BigInt).Value = objRoom.RoomNo;
            cmd.Parameters.Add("@RoomType_ID", SqlDbType.BigInt).Value = objRoom.RoomType_ID;
            cmd.Parameters.Add("@Floor", SqlDbType.BigInt).Value = objRoom.Floor;
            cmd.Parameters.Add("@Area", SqlDbType.Decimal).Value = objRoom.Area;
            cmd.Parameters.Add("@No_of_Places", SqlDbType.BigInt).Value = objRoom.NoofPlaces;
            cmd.Parameters.Add("@Price_For_Night", SqlDbType.Decimal).Value = objRoom.PriceForNight;
            cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = objRoom.Action;

            cmd.Transaction = trans;
            String i = objdac.iexecute(cmd, mycon);
            return i;
        }

        public DataTable Load_Room(Int32 RoomNo, Int32 Floor)
        {
            SqlCommand cmd = new SqlCommand("Exec Load_Room '" + RoomNo + "','" + Floor + "'");
            DataSet ds = new DataSet();
            ds = objdac.select(cmd);
            return ds.Tables[0];
        }
    }
}
