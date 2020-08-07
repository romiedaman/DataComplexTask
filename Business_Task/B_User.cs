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
    public class B_User
    {
        dac objdac = new dac();

        public String IUD_User(E_User objUser, SqlConnection mycon, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IUD_User";
            cmd.Connection = mycon;

            cmd.Parameters.Add("@User_ID", SqlDbType.BigInt).Value = objUser.UserId;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = objUser.UserName;
            cmd.Parameters.Add("@Surname", SqlDbType.VarChar).Value = objUser.Surname;
            cmd.Parameters.Add("@Email_ID", SqlDbType.VarChar).Value = objUser.EmailID;
            cmd.Parameters.Add("@Telephone", SqlDbType.VarChar).Value = objUser.MobileNo;
            cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = objUser.Action;

            cmd.Transaction = trans;
            String i = objdac.iexecute(cmd, mycon);
            return i;
        }

        public DataTable Load_User(Int32 UserID)
        {
            SqlCommand cmd = new SqlCommand("Exec Load_User '" + UserID + "'");
            DataSet ds = new DataSet();
            ds = objdac.select(cmd);
            return ds.Tables[0];
        }
        public DataTable Load_UserBooking(Int32 UserID,Int32 RoomID)
        {
            SqlCommand cmd = new SqlCommand("Exec Load_UserBooking '" + UserID + "','" + RoomID + "'");
            DataSet ds = new DataSet();
            ds = objdac.select(cmd);
            return ds.Tables[0];
        }
    }
}
