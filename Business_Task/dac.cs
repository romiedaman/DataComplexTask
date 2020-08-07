using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Task
{
    public class dac
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        const String _Status = "Status";

        public String iexecute(SqlCommand cmd, SqlConnection con)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlParameter para1 = new SqlParameter(_Status, SqlDbType.VarChar, 90000);
            para1.Value = "";
            para1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(para1);
            cmd.ExecuteNonQuery();
            String i = Convert.ToString(cmd.Parameters["Status"].Value);
            return i;
        }
        public DataSet select(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            con.Close();
            return ds;
        }
    }
}
