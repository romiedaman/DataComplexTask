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
    public class B_RoomType
    {
        dac objdac = new dac();

        public String IUD_RoomType(E_RoomType objRoomType, SqlConnection mycon, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IUD_RoomType";
            cmd.Connection = mycon;

            cmd.Parameters.Add("@RoomType_ID", SqlDbType.BigInt).Value = objRoomType.RoomTypeID;
            cmd.Parameters.Add("@Room_Type", SqlDbType.VarChar).Value = objRoomType.RoomType;
            cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = objRoomType.Action;

            cmd.Transaction = trans;
            String i = objdac.iexecute(cmd, mycon);
            return i;
        }

        public DataTable Load_RoomType(Int32 RoomType_ID)
        {
            SqlCommand cmd = new SqlCommand("Exec Load_RoomType '" + RoomType_ID + "'");
            DataSet ds = new DataSet();
            ds = objdac.select(cmd);
            return ds.Tables[0];
        }
    }
}
