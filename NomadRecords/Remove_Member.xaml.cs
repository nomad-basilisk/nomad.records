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
using System.Data;
using System.Data.SqlClient;

namespace NomadRecords
{
    /// <summary>
    /// Interaction logic for Remove_Member.xaml
    /// </summary>
    public partial class Remove_Member : Window
    {
        public Remove_Member()
        {
            InitializeComponent();
        }

        private void Fill_Member_DataGrid()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            
            string CmdString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = String.Format("SELECT * FROM udf_members({0})", User.id);
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Members");
                sda.Fill(dt);
                grdMember.ItemsSource = dt.DefaultView;
            }
        }

        private void FillDataGrid_Stokvel_Filtered(string filter)
        {
            //Populate Stokvel Data

            grdMember.ItemsSource = null;
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = String.Format("SELECT * FROM udf_members({0}) WHERE Stokvel_Name LIKE '%{1}%'", User.id, filter);
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Member");
                sda.Fill(dt);

                grdMember.ItemsSource = dt.DefaultView;

                StatusLabel.Content = String.Format("Data succesfully filtered for '{0}'.", filter);
            }
           
        }

        private void FillDataGrid_Member_Filtered(string filter)
        {
            //Populate Member Data
            
            grdMember.ItemsSource = null;
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = String.Format("SELECT * FROM udf_members({0}) WHERE Name LIKE '%{1}%'", User.id, filter);
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Member");
                sda.Fill(dt);

                grdMember.ItemsSource = dt.DefaultView;

                StatusLabel.Content = String.Format("Data succesfully filtered for '{0}'.", filter);
            }
        }

        private void grdRefresh(object sender, RoutedEventArgs e)
        {
            Fill_Member_DataGrid();
        }
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text.Length > 0)
            {
                if (searchBy_ComboBox.Text == "Stokvel Name")
                {
                    FillDataGrid_Stokvel_Filtered(searchTextBox.Text);
                }
                else
                {
                    FillDataGrid_Member_Filtered(searchTextBox.Text);
                }
            }
            else
            {
                Fill_Member_DataGrid();
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (searchTextBox.Text.Length > 0)
            {
                if (searchBy_ComboBox.Text == "Stokvel Name")
                {
                    FillDataGrid_Stokvel_Filtered(searchTextBox.Text);
                }
                else
                {
                    FillDataGrid_Member_Filtered(searchTextBox.Text);
                }
            }
            else
            {
                Fill_Member_DataGrid();
            }
        }

        private void Remove_Member_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;

            Member m = new Member();
            m.id = int.Parse(ID.ToString());

            m.remove();

            StatusLabel.Content = "Stokvel sucessfully remmoved";

            Fill_Member_DataGrid();
        }
    }
}
