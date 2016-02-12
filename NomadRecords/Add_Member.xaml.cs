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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace NomadRecords
{
    /// <summary>
    /// Interaction logic for Add_Member.xaml
    /// </summary>
    public partial class Add_Member : Window
    {
        string stokvel_id;

        public Add_Member(string stokvel_id_x)
        {
            InitializeComponent();

            //set stokvel id
            stokvel_id = stokvel_id_x;

        }

        private void AddMember(object sender, RoutedEventArgs e)
        {
            DateTime dob = DateTime.Today;
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string idNumber = IdNumTextBox.Text;
            string cellPhone = CellphoneTextBox.Text;
            string email = EmailTextBox.Text;
            string address = AddressTextBox.Text;
            
            //Make sure DOB is not blank
            if (DOBDatePicker.SelectedDate.HasValue)
            {
                dob = DOBDatePicker.SelectedDate.Value;
            }
            else {
                DisplayError("Please enter a valid date for Date of Birth.");
            }
            
            //Error handling First Name
            if (firstName.Length == 0){
                DisplayError("Please enter a valid first name.");
            }

            //Error handling Last Name
            if (lastName.Length == 0){
                DisplayError("Please enter a valid last name.");
            }

            //Error handling Id number
            if (idNumber.Length == 0){
                DisplayError("Please enter a valid Id Number.");
            }

            Member m = new Member();
            m.stokvel_idStr = stokvel_id;
            m.firstNameStr = firstName;
            m.lastNameStr = lastName;
            m.idNumberStr = idNumber;
            m.emailStr = email;
            m.cellPhoneStr = cellPhone;
            m.addressStr = address;
            m.dobDt = dob;

            m.insert();
                        
            //Check if user is busy with Wizard and continue if true.
            if (StaticStokvel.wizard)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to verify this member now?", "Verify Member", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //Get member OTP
                    var connectionString = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

                    string CmdString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        Console.WriteLine(StaticMemberOTP.member_id);

                        CmdString = String.Format("select OTP from member_otp where member_id = {0}", StaticMemberOTP.member_id);
                        SqlCommand cmd = new SqlCommand(CmdString, con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable("MemberID");
                        sda.Fill(dt);

                        foreach (DataRow row in dt.Rows) 
                        {
                            StaticMemberOTP.otp = row["OTP"].ToString();

                            string number = cellPhone.Substring(1, 9);
                            number = String.Concat("27", number);

                            Sms_OTP_Send.SmsSendYes(StaticMemberOTP.otp, number);

                            Member_Verification_OTP mvo = new Member_Verification_OTP();
                            mvo.Show();
                        }
                    }  

                }
            }
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        { 
        
        }

        private void DisplayError(string error) {
            if (MessageBox.Show(error, "Member Error", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                
            }
        }
    }
}
