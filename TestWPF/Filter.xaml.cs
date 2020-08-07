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
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : Window
    {
        B_Room _Room = new B_Room();
        B_RoomType _RoomType = new B_RoomType();
        E_Room objRoom = new E_Room();
        E_RoomType objRoomType = new E_RoomType();
        B_Reservation _Reservation = new B_Reservation();
        E_Reservation objReservation = new E_Reservation();
        B_User _USer = new B_User();
        E_User objUSer = new E_User();
        SqlTransaction trans;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        String i = "";
        public Filter()
        {
            InitializeComponent();
            bindRoom(0,0);
            bindUser();
        }

        private void bindRoom(Int32 RoomNo, Int32 Floor)
        {
            DataTable dt = _Room.Load_Room(RoomNo, Floor);
            this.cbdetail_Room.ItemsSource = ((IListSource)dt).GetList();
            this.cbdetail_Room.DisplayMemberPath = "Room_No";
            this.cbdetail_Room.SelectedValuePath = "Room_ID";
        }

        private void bindUser()
        {

            DataTable dt = _USer.Load_User(0);
            this.cbdetail_User.ItemsSource = ((IListSource)dt).GetList();
            this.cbdetail_User.DisplayMemberPath = "FullName";
            this.cbdetail_User.SelectedValuePath = "User_ID";
        }


        private void btn_RoomDetail_Click(object sender, RoutedEventArgs e)
        {
            Int32 RoomID = Int32.Parse(cbdetail_Room.SelectedValue.ToString());
            Int32 RoomNo = 0, Floor= 0;
            DataTable dt = _USer.Load_UserBooking(0,RoomID);
            if (dt.Rows.Count == 1)
            {
                Floor  = Convert.ToInt32(dt.Rows[0]["Floor"].ToString());
                RoomNo = Convert.ToInt32(dt.Rows[0]["Room_No"].ToString());
            }


            DataTable dtReser= _Reservation.Load_Reservation(RoomNo, Floor, dt_From.Text, dt_To.Text);
            dgdetail.ItemsSource = dtReser.DefaultView;
            dgdetail.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void btn_UserDetail_Click(object sender, RoutedEventArgs e)
        {
            Int32 USerID = Int32.Parse(cbdetail_User.SelectedValue.ToString());

            DataTable dt = _USer.Load_UserBooking(USerID,0);
            dgdetail.ItemsSource = dt.DefaultView;
            dgdetail.Columns[0].Visibility = Visibility.Collapsed;
            dgdetail.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            dgdetail.Items.Clear();
        }
    }
}
