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

namespace NomadRecords
{
    /// <summary>
    /// Interaction logic for Add_Meeting.xaml
    /// </summary>
    public partial class Add_Meeting : Window
    {
        public Add_Meeting()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Record_Add(object sender, RoutedEventArgs e)
        {
            Meeting m = new Meeting();
            m.dateX = DateDatePicker.SelectedDate ?? default(DateTime);
            m.notesX = NotesTextBox.Text;

            m.insert();

            this.Close();
        }
    }
}
