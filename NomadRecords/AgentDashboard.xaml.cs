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
    /// Interaction logic for AgentDashboard.xaml
    /// </summary>
    public partial class AgentDashboard : Window
    {
        public AgentDashboard()
        {
            InitializeComponent();
            FillDataGrid();
            
        }

        private void Add_Stokvel_Click(object sender, RoutedEventArgs e)
        {
            ConstitutionWizard.Constitution_Wizard_1 win = new ConstitutionWizard.Constitution_Wizard_1();
            win.Show();
        }

        private void FillDataGrid()
        {
            //Populate Stokvel Data
            //TODO: Add filter for individual agent.

            grdStokvel.ItemsSource = null;
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = "SELECT id, " + 
                            "Stokvel_Name, " + 
                            "SUBSTRING(CAST(Next_Meeting_Date AS VARCHAR),0, 12) AS 'Next_Meeting_Date', " + 
                            "Purpose, " + 
                            "CAST(Current_Balance AS BIGINT) AS 'Current_Balance', " + 
                            "Members " +
                            "FROM vw_stokvel_dashboard";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Stokvel");
                sda.Fill(dt);

                grdStokvel.ItemsSource = dt.DefaultView;
            }
        }

        private void FillDataGrid_Filtered(string filter)
        {
            //Populate Stokvel Data
            //TODO: Add filter for individual agent.

            grdStokvel.ItemsSource = null;
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = 
                    String.Format("SELECT id, "+
                                    "Stokvel_Name, "+
                                    "SUBSTRING(CAST(Next_Meeting_Date AS VARCHAR),0, 12) AS 'Next_Meeting_Date', "+
                                    "Purpose,  "+
                                    "CAST(Current_Balance AS BIGINT) AS 'Current_Balance', "+
                                    "Members "+
                                "FROM vw_stokvel_dashboard "+
                                "WHERE Stokvel_Name LIKE '%{0}%'"
                    , filter);
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Stokvel");
                sda.Fill(dt);

                grdStokvel.ItemsSource = dt.DefaultView;
            }
        }

        private void FillDataGrid_Member_Filtered(string filter)
        {
            //Populate Stokvel Data
            //TODO: Add filter for individual agent.

            grdStokvel.ItemsSource = null;
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = String.Format("SELECT id, "+ 
                                            "Stokvel_Name, "+
                                            "SUBSTRING(CAST(Next_Meeting_Date AS VARCHAR),0, 12) AS 'Next_Meeting_Date', "+
                                            "Purpose,  "+
                                            "CAST(Current_Balance AS BIGINT) AS 'Current_Balance', "+
                                            "Members " +
                                        "FROM udf_stokvel_dashboard_member_filter('{0}')"
                            , filter);
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Stokvel");
                sda.Fill(dt);

                grdStokvel.ItemsSource = dt.DefaultView;
            }
        }

        private void grdRefresh(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void Open_Stokvel(object sender, RoutedEventArgs e)
        {
            //Button handler to open detailed Stokvel screen from button in dgrid.
            object ID = ((Button)sender).CommandParameter;
            StaticStokvel.id = ID.ToString();

            Stokvel_Dashboard sd = new Stokvel_Dashboard(ID.ToString());
            sd.Show();

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text.Length > 0)
            {
                if (searchBy_ComboBox.Text == "Stokvel Name")
                {
                    FillDataGrid_Filtered(searchTextBox.Text);
                }
                else
                {
                    FillDataGrid_Member_Filtered(searchTextBox.Text);
                }
            }
            else
            {
                FillDataGrid();
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (searchTextBox.Text.Length > 0)
            {
                if (searchBy_ComboBox.Text == "Stokvel Name")
                { 
                    FillDataGrid_Filtered(searchTextBox.Text);
                }
                else
                {
                    FillDataGrid_Member_Filtered(searchTextBox.Text);
                }
            }
            else
            {
                FillDataGrid();
            }
             
        }

        private void Remove_Stokvel_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Are you sure you would like to remove this stokvel? Your business administrator will be notified";
            if (MessageBox.Show(msg, "Remove Stokvel", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Remove_Stokvel rm = new Remove_Stokvel();
                rm.Show();
            }
        }

        private void Remove_Member_Click(object sender, RoutedEventArgs e)
        {
            string msg = "Are you sure you would like to remove this member? Your business administrator will be notified";
            if (MessageBox.Show(msg, "Remove Member", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Remove_Member rm = new Remove_Member();
                rm.Show();
            }
        }
    }
}
