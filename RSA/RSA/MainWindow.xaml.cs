using System;
using System.Windows;

namespace RSA
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_GenerateNum_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBox_src.Text != "")
            {
                TxtBox_p.Text = (Calculations.GetP()).ToString();
                TxtBox_q.Text = (Calculations.GetQ()).ToString();
                TxtBox_r.Text = (Calculations.GetR()).ToString();
                TxtBox_fi.Text = (Calculations.EulerFunction(Calculations.p, Calculations.q)).ToString();
                TxtBox_e.Text = (Calculations.OpenExhibitor(Calculations.fi)).ToString();
                TxtBox_d.Text = (Calculations.Euclide(Calculations.fi, Calculations.e)).ToString();
                Calculations.sourceNumFirst = Convert.ToInt64(TxtBox_src.Text);
                TxtBox_cphr.Text = (Calculations.cryptRSA(Calculations.sourceNumFirst, Calculations.cipherNumFirst, Calculations.e, Calculations.r)).ToString();
                TxtBox_cphr_decr.Text = TxtBox_cphr.Text;
                Calculations.cipherNumSec = Convert.ToInt64(TxtBox_cphr_decr.Text);
                TxtBox_src_decr.Text = (Calculations.decryptRSA(Calculations.sourceNumSec, Calculations.cipherNumSec, Calculations.d, Calculations.r)).ToString();
                TxtBox_hash.Text = Calculations.HashSum(Calculations.r).ToString();
                Calculations.m = Convert.ToInt64(TxtBox_hash.Text);
                Calculations.digitalSign(Calculations.m, Calculations.d, Calculations.r);
                TxtBox_sender.Text = Calculations.senderS.ToString();
                Calculations.digitalUnsign(Calculations.senderS, Calculations.e, Calculations.r);
                TxtBox_reciever.Text = Calculations.recieverS.ToString();
            }
            else
            {
                MessageBox.Show("Empty source number.");
            }
           
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            TxtBox_p.Text = "";
            TxtBox_q.Text = "";
            TxtBox_r.Text = "";
            TxtBox_r.Text = "";
            TxtBox_fi.Text = "";
            TxtBox_e.Text = "";
            TxtBox_d.Text = "";
            TxtBox_cphr.Text = "";
            TxtBox_src.Text = "";
            TxtBox_cphr_decr.Text = "";
            TxtBox_src_decr.Text = "";
            TxtBox_hash.Text = "";
            TxtBox_sender.Text = "";
            TxtBox_reciever.Text = "";
        }
    }
}
