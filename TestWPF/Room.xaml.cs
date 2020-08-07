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
using System.ComponentModel;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : Window
    {

        B_Room _Room = new B_Room();
        B_RoomType _RoomType = new B_RoomType();
        E_Room objRoom = new E_Room();
        E_RoomType objRoomType = new E_RoomType();
        SqlTransaction trans;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        String i = "";

        public Room()
        {
            InitializeComponent();
            bindRoomType();
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objRoom.RoomNo = Int32.Parse(txt_RoomNO.Text);
                objRoom.RoomType_ID = Int32.Parse(cb_RoomTypeID.SelectedValue.ToString());
                objRoom.Floor = Int32.Parse(txt_Floor.Text);
                objRoom.Area = Decimal.Parse(txt_Area.Text);
                objRoom.NoofPlaces = Int32.Parse(txt_noofplaces.Text);
                objRoom.PriceForNight = Decimal.Parse(txt_pricefornight.Text);
                objRoom.Action = "INSERT";

                i = _Room.IUD_Room(objRoom, con, trans);
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

        private void bindRoomType()
        {

            DataTable dt = _RoomType.Load_RoomType(0);
            this.cb_RoomTypeID.ItemsSource = ((IListSource)dt).GetList();
            this.cb_RoomTypeID.DisplayMemberPath = "Room_Type";
            this.cb_RoomTypeID.SelectedValuePath = "RoomType_ID";
        }


        private void bindRoom(Int32 RoomNo, Int32 Floor)
        {
            DataTable dt = _Room.Load_Room(RoomNo, Floor);
            dgRoom.ItemsSource = dt.DefaultView;
        }

        private void reset()
        {
            txt_RoomNO.Text = "";
            txt_Floor.Text = "";
            txt_Area.Text = "";
            txt_noofplaces.Text = "";
            txt_pricefornight.Text = "";
            bindRoom(0, 0);
            btn_Insert.IsEnabled = false;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;
            bindRoomType();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objRoom.RoomNo = Int32.Parse(txt_RoomNO.Text.ToString());
                objRoom.RoomType_ID = Int32.Parse(cb_RoomTypeID.SelectedValue.ToString());
                objRoom.Floor = Int32.Parse(txt_Floor.Text.ToString());
                objRoom.Area = Decimal.Parse(txt_Area.Text.ToString());
                objRoom.NoofPlaces = Int32.Parse(txt_noofplaces.Text.ToString());
                objRoom.PriceForNight = Decimal.Parse(txt_pricefornight.Text.ToString());

                objRoom.Action = "UPDATE";
                i = _Room.IUD_Room(objRoom, con, trans);
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
                objRoom.RoomNo = Int32.Parse(txt_RoomNO.Text.ToString());
                objRoom.Floor = Int32.Parse(txt_Floor.Text.ToString());

                objRoom.Action = "DELETE";
                i = _Room.IUD_Room(objRoom, con, trans);
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

        private void btn_addRoom_Click(object sender, RoutedEventArgs e)
        {
            txt_RoomNO.Text = "";
            txt_Floor.Text = "";
            txt_Area.Text = "";
            txt_pricefornight.Text = "";
            txt_noofplaces.Text = "";
            btn_Insert.IsEnabled = true;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;

        }

        private void btn_ShowRooms_Click(object sender, RoutedEventArgs e)
        {
            bindRoom(0, 0);
        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txt_RoomNO.Text = dr["Room_No"].ToString();
                txt_Floor.Text = dr["Floor"].ToString();
                cb_RoomTypeID.SelectedItem = dr["Room_Type"].ToString();
                cb_RoomTypeID.SelectedValue = dr["RoomType_ID"].ToString();
                txt_Area.Text = dr["Area"].ToString();
                txt_noofplaces.Text = dr["No_of_Places"].ToString();
                txt_pricefornight.Text = dr["Price_For_Night"].ToString();
                btn_Update.IsEnabled = true;
                btn_delete.IsEnabled = true;
                btn_Insert.IsEnabled = false;
            }
        }
    }
}
