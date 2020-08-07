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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        B_User _U = new B_User();
        E_User objUsers = new E_User();
        SqlTransaction trans;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        String i = "";

        public User()
        {
            InitializeComponent();
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objUsers.UserName = txt_Name.Text;
                objUsers.Surname = txt_SurName.Text;
                objUsers.EmailID = txt_Email.Text;
                objUsers.MobileNo = txt_Phone.Text;

                objUsers.Action = "INSERT";
                i = _U.IUD_User(objUsers, con, trans);
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

        private void binduser(Int32 User_ID)
        {
            DataTable dt = _U.Load_User(User_ID);
            dgUser.ItemsSource = dt.DefaultView;
        }

        private void reset()
        {
            lbl_UserID.Content = "";
            txt_Name.Text = "";
            txt_SurName.Text = "";
            txt_Phone.Text = "";
            txt_Email.Text = "";
            binduser(0);
            btn_Insert.IsEnabled = false;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;
        }

        private void btn_ShowUsers_Click(object sender, RoutedEventArgs e)
        {
            binduser(0);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                objUsers.UserId = Int32.Parse(lbl_UserID.Content.ToString());
                objUsers.UserName = txt_Name.Text;
                objUsers.Surname = txt_SurName.Text;
                objUsers.EmailID = txt_Email.Text;
                objUsers.MobileNo = txt_Phone.Text;

                objUsers.Action = "UPDATE";
                i = _U.IUD_User(objUsers, con, trans);
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
                objUsers.UserId = Int32.Parse(lbl_UserID.Content.ToString());

                objUsers.Action = "DELETE";
                i = _U.IUD_User(objUsers, con, trans);
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

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                lbl_UserID.Content = dr["User_ID"].ToString();
                txt_Name.Text = dr["Name"].ToString();
                txt_SurName.Text = dr["Surname"].ToString();
                txt_Email.Text = dr["Email_ID"].ToString();
                txt_Phone.Text = dr["Telephone"].ToString();
                btn_Update.IsEnabled = true;
                btn_delete.IsEnabled = true;
                btn_Insert.IsEnabled = false;
            }
                 
        }

        private void btn_addUser_Click(object sender, RoutedEventArgs e)
        {
            lbl_UserID.Content = "";
            txt_Name.Text = "";
            txt_SurName.Text = "";
            txt_Phone.Text = "";
            txt_Email.Text = "";
            btn_Insert.IsEnabled = true;
            btn_Update.IsEnabled = false;
            btn_delete.IsEnabled = false;
        }

    }
}
