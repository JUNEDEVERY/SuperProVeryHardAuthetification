using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Login_Confirmation.xaml
    /// </summary>
     
    public partial class LoginConfirmation : Window

    {
        private int counter = 11;
       
        private  DispatcherTimer dispatcherTimer;
        string  generationCode;
        public LoginConfirmation(int generationCode)
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
            this.generationCode = Convert.ToString(generationCode);
        }

         void DispatcherTimer_Tick(object sender, EventArgs e)
         {
            if(counter != 0)
            {
                if(counter%2 == 0)
                {
                    tbCodeInfo.Foreground = Brushes.Red;

                }
                else
                {
                    tbCodeInfo.Foreground = Brushes.Black;
                }
                counter--;
                tbCodeInfo.Text = string.Format("00:0{0}:{1} секунд", counter / 60, counter % 60);


            }
            else
            {
              
                this.Close();
            }

        }
     
        private void tbCodeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(tbCodeInput.Text == generationCode)
            {
                MainWindow.successfullyCode = tbCodeInput.Text;
                this.Close();
                CaptchaAuthenification captchaAuthenification = new CaptchaAuthenification();
                captchaAuthenification.ShowDialog();
            }
            MainWindow.countOutput = tbCodeInput.Text.Length;
        }
    }
}
