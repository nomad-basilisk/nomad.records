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

namespace NomadRecords.ConstitutionWizard
{
    /// <summary>
    /// Interaction logic for Constitution_Wizard_1.xaml
    /// </summary>
    public partial class Constitution_Wizard_1 : Window
    {

        public Constitution_Wizard_1()
        {
            InitializeComponent();
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            Stokvel.name = NameTextBox.Text;
            Stokvel.purpose = PurposeTextBox.Text;

            ConstitutionWizard.Constitution_Wizard_2 win = new ConstitutionWizard.Constitution_Wizard_2();
            win.Show();
            this.Close();
        }

        private void CancelSetup(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
