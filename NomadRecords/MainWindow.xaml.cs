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
using System.Windows.Threading;

namespace NomadRecords
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer = null;

        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();

            _timer = new DispatcherTimer();
            _timer.Tick += Each_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1500);
            _timer.Start(); 
        }

        private void Each_Tick(object o, EventArgs sender)
        {
            // Refresh from database etc ...
            FillDataGrid();
            Console.WriteLine("Polling database.");
        }

        private void FillDataGrid() {
            grdStokvel.ItemsSource = null;
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = "SELECT s.id, s.name AS 'Stokvel_Name', s.contribution_amount AS 'Contribution_Amount', s.inception_date AS 'Inception_Date', count(m.id) AS 'Members' FROM stokvel s LEFT JOIN member m ON m.stokvel_id = s.id GROUP BY s.id, s.name, s.contribution_amount, s.inception_date";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Stokvel");
                sda.Fill(dt);
                
                grdStokvel.ItemsSource = dt.DefaultView;
            }  
        }
        
        private void NewStokvelButton(object sender, RoutedEventArgs e)
        {
            ConstitutionWizard .Constitution_Wizard_1 win = new ConstitutionWizard.Constitution_Wizard_1();
            win.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object ID = ((Button)sender).CommandParameter;
            StaticStokvel.id = ID.ToString();

            Stokvel_Dashboard sd = new Stokvel_Dashboard(ID.ToString());
            sd.Show();
           
        }
    }
}
