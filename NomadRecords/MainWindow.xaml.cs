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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NomadRecords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid(); 
        }

        private void FillDataGrid() {
            string ConString = "Data Source=RokoBasilisk-PC;Initial Catalog=NMR_001_BETA;Integrated Security=True;";
            string CmdString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT s.name AS 'Stokvel Name', s.contribution_amount AS 'Contribution Amount', s.inception_date AS 'Inception Date', count(m.id) AS 'Members' FROM stokvel s LEFT JOIN member m ON m.stokvel_id = s.id GROUP BY s.name, s.contribution_amount, s.inception_date";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Stokvel");
                sda.Fill(dt);
                grdStokvel.ItemsSource = dt.DefaultView;
            }  
        }

        private void NewStokvelButton(object sender, RoutedEventArgs e)
        {
            ConstitutionWizard.Constitution_Wizard_1 win = new ConstitutionWizard.Constitution_Wizard_1();
            win.Show();
        }

    }
}
