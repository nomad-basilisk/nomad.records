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

namespace NomadRecords
{
    /// <summary>
    /// Interaction logic for Meeting_Notes.xaml
    /// </summary>
    public partial class Meeting_Notes : Window
    {
        public Meeting_Notes(string notes_id)
        {
            InitializeComponent();
            Load_Meeting_Notes(notes_id);
            Console.WriteLine(notes_id);
        }

        private void Load_Meeting_Notes(string notes_id) 
        {
            var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            string CmdString = String.Empty;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CmdString = String.Format("select notes As Note from meeting where id = {0}", notes_id);
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Notes");
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    NotesTextBlock.Text = dr["Note"].ToString();
                }
            }  
        }
    }
}
