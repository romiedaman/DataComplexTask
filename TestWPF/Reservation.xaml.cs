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
using System.Globalization;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Window
    {
        B_Room _Room = new B_Room();
        B_RoomType _RoomType = new B_RoomType();
        E_Room objRoom = new E_Room();
        E_RoomType objRoomType = new E_RoomType();
        B_User _User = new B_User();
        E_User objUser = new E_User();
        B_Reservation _Reservation = new B_Reservation();
        E_Reservation objReservation = new E_Reservation();
        SqlTransaction trans;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        String i = "";

        public Reservation()
        {
            InitializeComponent();
            bindUser();
            bindFloor();
        }

        private void bindUser()
        {

            DataTable dt = _User.Load_User(0);
            this.cbUser.ItemsSource = ((IListSource)dt).GetList();
            this.cbUser.DisplayMemberPath = "FullName";
            this.cbUser.SelectedValuePath = "User_ID";
        }

        private void bindFloor()
        {

            DataTable dt = _Room.Load_Room(0,0);
            this.cb_Floor.ItemsSource = ((IListSource)dt).GetList();
            this.cb_Floor.DisplayMemberPath = "Floor";
            this.cb_Floor.SelectedValuePath = "Floor";
        }

        private void bindReservation(Int32 RoomNo, Int32 Floor,string from, string To)
        {
            DataTable dt = _Reservation.Load_Reservation(RoomNo, Floor,from,To);
            dgReservation.ItemsSource = dt.DefaultView;
            dgReservation.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void reset()
        {
            lbl_ReservationID.Content = "";
            cbUser.Items.Clear();
            cb_Floor.Items.Clear();
            txt_PhoneNO.Text = "";
            txt_EmailID.Text = "";
            txt_NoofGuest.Text = "";
            cb_RoomID.Items.Clear();
            bindReservation(0, 0,null,null);
            btn_Insert.IsEnabled = false;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;
            bindFloor();
            bindUser();
        }

        private void btn_addReservation_Click(object sender, RoutedEventArgs e)
        {

            txt_PhoneNO.Text = "";
            txt_EmailID.Text = "";
            txt_NoofGuest.Text = "";
            btn_Insert.IsEnabled = true;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {

            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objReservation.Room_ID = Int32.Parse(cb_RoomID.SelectedValue.ToString());
                objReservation.UserID = Int32.Parse(cbUser.SelectedValue.ToString());
                objReservation.No_of_Guest = Int32.Parse(txt_NoofGuest.Text);
                objReservation.From = DateTime.ParseExact(dt_From.Text, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objReservation.To = DateTime.ParseExact(dt_To.Text, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objReservation.Action = "INSERT";

                i = _Reservation.IUD_Reservation(objReservation, con, trans);
                if (i != "")
                {
                    trans.Commit();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);
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

        private void btn_ShowReservation_Click(object sender, RoutedEventArgs e)
        {
            bindReservation(0, 0, null, null);
        }

        private void dgReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                lbl_ReservationID.Content = dr["Reservation_ID"].ToString();
                DataTable dt = _User.Load_User(Int32.Parse(dr["User_ID"].ToString()));
                if (dt.Rows.Count == 1)
                {
                    cbUser.SelectedItem = dt.Rows[0]["FullName"].ToString();
                }
                cbUser.SelectedValue = dr["User_ID"].ToString();
                txt_PhoneNO.Text = dr["Telephone"].ToString();
                txt_EmailID.Text = dt.Rows[0]["Email_ID"].ToString();
                cb_Floor.SelectedItem = dr["Floor"].ToString();
                cb_Floor.SelectedValue = dr["Floor"].ToString();
                cbUser.SelectedItem = dr["Room_No"].ToString();
                cbUser.SelectedValue = dr["Room_ID"].ToString();

                txt_NoofGuest.Text = dr["No_of_Guest"].ToString();
                dt_From.Text = dr["From"].ToString();
                dt_To.Text = dr["To"].ToString();

              


                btn_Update.IsEnabled = true;
                btn_delete.IsEnabled = true;
                btn_Insert.IsEnabled = false;
            }
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objReservation.Reservation_ID = Int32.Parse(lbl_ReservationID.Content.ToString());
                objReservation.Room_ID = Int32.Parse(cb_RoomID.SelectedValue.ToString());
                objReservation.UserID = Int32.Parse(cbUser.SelectedValue.ToString());
                objReservation.No_of_Guest = Int32.Parse(txt_NoofGuest.Text);
                objReservation.From = DateTime.ParseExact(dt_From.Text, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objReservation.To = DateTime.ParseExact(dt_To.Text, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                objReservation.Action = "UPDATE";
                i = _Reservation.IUD_Reservation(objReservation, con, trans);
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
                objReservation.Reservation_ID = Int32.Parse(lbl_ReservationID.Content.ToString());

                objReservation.Action = "DELETE";
                i = _Reservation.IUD_Reservation(objReservation, con, trans);
                if (i != "")
                {
                    trans.Commit();
                    con.Close();

                    MessageBoxResult result = MessageBox.Show(i);
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

        private void cbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Int32 userID = Int32.Parse(cbUser.SelectedValue.ToString());
            DataTable dt = _User.Load_User(userID);
            if (dt.Rows.Count == 1)
            {
                txt_PhoneNO.Text = dt.Rows[0]["Telephone"].ToString();
                txt_EmailID.Text = dt.Rows[0]["Email_ID"].ToString();
            }
        }

        private void cb_Floor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Int32 Floor = Int32.Parse(cb_Floor.SelectedValue.ToString());
            DataTable dt = _Room.Load_Room(0, Floor);
            this.cb_RoomID.ItemsSource = ((IListSource)dt).GetList();
            this.cb_RoomID.DisplayMemberPath = "Room_No";
            this.cb_RoomID.SelectedValuePath = "Room_ID";
        }
    }
}
