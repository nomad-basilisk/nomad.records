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
using System.Windows.Threading;

namespace NomadRecords.ConstitutionWizard
{
    /// <summary>
    /// Interaction logic for Constitution_Wizard_2.xaml
    /// </summary>
    public partial class Constitution_Wizard_2 : Window
    {

        private DispatcherTimer _timer = null;

        string stokvel_id;
        string name;
        string purpose;
        string joining_fee;
        string contributions;

        public Constitution_Wizard_2(string stokvel_id_x, string name_x, string purpose_x, string joining_fee_x, string contributions_x)
        {
            InitializeComponent();

            //set stokvel id
            stokvel_id = stokvel_id_x;
            name = name_x;
            purpose = purpose_x;
            joining_fee = joining_fee_x;
            contributions = contributions_x;

            FillDataGrid();
        }

        private void FillDataGrid()
        {

            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = String.Format("select firstname, lastname, contact_number, email, id_number, address, dob, CASE WHEN mo.verified = 1 THEN 'YES' ELSE 'NO' END AS 'Verified' from member m join member_otp mo on mo.member_id = m.id where stokvel_id = {0}", StaticStokvel.id.ToString());
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Members");
                sda.Fill(dt);
                membersGrd.ItemsSource = dt.DefaultView;
            }
        }

        private void Refresh_Grid(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        private void AddMember(object sender, RoutedEventArgs e) 
        {
            Add_Member am = new Add_Member(stokvel_id);
            am.Show();
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            Constitution c = new Constitution();
            c.CreateSampleDocument(stokvel_id, name, purpose, joining_fee, contributions);

            this.Close();
        }

        private void CancelSetup(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
