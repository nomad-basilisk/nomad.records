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
        string name;
        string contributions;
        string purpose;
        string joining_fee;

        public Constitution_Wizard_1()
        {
            InitializeComponent();
            StaticStokvel.wizard = true;
        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            Stokvel sv = new Stokvel();
            sv.name = NameTextBox.Text;
            name = NameTextBox.Text; ;

            sv.purpose = PurposeTextBox.Text;
            purpose = PurposeTextBox.Text;

            sv.inception_date = DateTime.Today;

            //Contribution amount
            decimal contAmount;

            if (decimal.TryParse(ContAmountTextBox.Text, out contAmount))
            {
                sv.contribution_amount = contAmount;
                contributions = contAmount.ToString();
            }
            else 
            {
                MessageBox.Show("Please use a valid monetary amount for Contribution Amount.");
            }

            //Joining Fee
            decimal joiningFeeAmount;

            if (decimal.TryParse(JoiningFeeTextBox.Text, out joiningFeeAmount))
            {
                sv.joining_fee = joiningFeeAmount;
                joining_fee = joiningFeeAmount.ToString();
            }
            else
            {
                MessageBox.Show("Please use a valid monetary amount for Joining Fee.");
            }

            string stokvel_id = sv.insert();
            ConstitutionWizard.Constitution_Wizard_2 win = new ConstitutionWizard.Constitution_Wizard_2(stokvel_id, name, purpose, joining_fee, contributions);
            win.Show();
            this.Close();
        }

        private void CancelSetup(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
