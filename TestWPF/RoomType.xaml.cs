using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using Entity_Task;
using Business_Task;
using System.Data;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for RoomType.xaml
    /// </summary>
    public partial class RoomType : Window
    {
        B_RoomType _RoomType = new B_RoomType();
        E_RoomType objRoomType = new E_RoomType();
        SqlTransaction trans;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        String i = "";

        public RoomType()
        {
            InitializeComponent();
        }

        private void btn_addRoomType_Click(object sender, RoutedEventArgs e)
        {
            lbl_RoomTypeID.Content = "";
            txt_RoomType.Text = "";
            btn_Insert.IsEnabled = true;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;
        }

        private void binduser(Int32 RoomType_ID)
        {
            DataTable dt = _RoomType.Load_RoomType(RoomType_ID);
            dgRoomType.ItemsSource = dt.DefaultView;
        }

        private void reset()
        {
            lbl_RoomTypeID.Content = "";
            txt_RoomType.Text = "";
            binduser(0);
            btn_Insert.IsEnabled = false;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objRoomType.RoomType = txt_RoomType.Text;

                objRoomType.Action = "INSERT";
                i = _RoomType.IUD_RoomType(objRoomType, con, trans);
                if (i != "")
                {
                    trans.Commit();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);
                    reset();
                }
                else
                {
                    trans.Rollback();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);
                }
            }
            catch (Exception e1)
            {
                trans.Rollback();
                con.Close();
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void btn_ShowRoomType_Click(object sender, RoutedEventArgs e)
        {
            binduser(0);
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objRoomType.RoomTypeID = Int32.Parse(lbl_RoomTypeID.Content.ToString());
                objRoomType.RoomType = txt_RoomType.Text;

                objRoomType.Action = "UPDATE";
                i = _RoomType.IUD_RoomType(objRoomType, con, trans);
                if (i != "")
                {
                    trans.Commit();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);
                    reset();
                }
                else
                {
                    trans.Rollback();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);

                }
            }
            catch (Exception e1)
            {
                trans.Rollback();
                con.Close();
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objRoomType.RoomTypeID = Int32.Parse(lbl_RoomTypeID.Content.ToString());

                objRoomType.Action = "DELETE";
                i = _RoomType.IUD_RoomType(objRoomType, con, trans);
                if (i != "")
                {
                    trans.Commit();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);
                    reset();
                }
                else
                {
                    trans.Rollback();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);

                }
            }
            catch (Exception e1)
            {
                trans.Rollback();
                con.Close();
            }

        }

        private void dgRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                lbl_RoomTypeID.Content = dr["RoomType_ID"].ToString();
                txt_RoomType.Text = dr["Room_Type"].ToString();
                btn_Update.IsEnabled = true;
                btn_delete.IsEnabled = true;
                btn_Insert.IsEnabled = false;
            }
        }
    }
}
