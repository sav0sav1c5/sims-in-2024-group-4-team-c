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

namespace BookingApp.WPF.Views.OwnerView
{
    /// <summary>
    /// Interaction logic for CommentDialog.xaml
    /// </summary>
    public partial class CommentDialog : Window
    {
        public string CommentText { get; set; }
        public CommentDialog()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Zatvori pop-up prozor bez akcije
            DialogResult = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Postavi komentar na vrednost koju je korisnik uneo u TextBox
            CommentText = CommentTextBox.Text;

            // Zatvori pop-up prozor
            DialogResult = true;
        }
    }
}
