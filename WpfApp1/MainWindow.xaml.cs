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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcher;
        private int counter = 60;
        public MainWindow()
        {
            InitializeComponent();
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = new TimeSpan(0, 0, 1);
            dispatcher.Tick += new EventHandler(TimerEnd);

        }

   
        private void TimerEnd(object sender, EventArgs e)
        {
            try
            {
                if (counter != 0)
                {
                    tbNewCode.Text = "Новый код доступен через \n\t" + string.Format("00:0{0}:{1}", counter / 60, counter % 60) + " секунд ";


                }
                else
                {
                    btnNewCode.IsEnabled = true;
                    btnNewCode.Visibility = Visibility.Visible;
                    tbNewCode.Visibility = Visibility.Collapsed;
                    dispatcher.Stop();
                }
                counter--;
                
            }
            catch
            {
                MessageBox.Show("Дваайте еще раз попробуем");
            }
            
         

        }
        public static int countOutput = 0;
        public static string successfullyCode = "";
    

        void StartAthorization()
        {
            try
            {
                if (tbLogin.Text != "admin" && tbPassword.Text != "admin")
                {
                    MessageBox.Show("Удостоверьтесь в корректности введеных учетной записи");
                }
                else
                {
                    Random random = new Random();
                    int generationCode = random.Next(0, 100000);
                    MessageBox.Show("Код для входа - " + generationCode.ToString());
                    LoginConfirmation login = new LoginConfirmation(generationCode);
                    login.ShowDialog();
                    if (successfullyCode != Convert.ToString(generationCode))
                    {
                        if (countOutput < 5)
                        {
                            MessageBox.Show("Введите код, состоящий из пятизначных цифр");
                            this.Close();
                        }
                        if (countOutput == 5)
                        {
                            MessageBox.Show("Вы не угадали =(", "Ошибка при аутентификации", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        stackGenerate.Visibility = Visibility.Visible;
                        btnAuth.Visibility = Visibility.Collapsed;
                        btnNewCode.Visibility = Visibility.Collapsed;
                        tbLogin.IsEnabled = false;
                        tbPassword.IsEnabled = false;
                        counter = 60; // объявил еще раз, а то у меня время в обратную сторону уходило, т.е. в минус
                        tbNewCode.Text = "Новый код доступен через \n\t" + string.Format("00:0{0}:{1}", counter / 60, counter % 60) + " секунд ";
                        tbNewCode.Visibility = Visibility.Visible;
                        dispatcher.Start();

                    }
                    else
                    {
                        authSucc.Visibility = Visibility.Visible;
                        stackLoginPassword.Visibility = Visibility.Collapsed;

                    }
                }
            }
            catch
            {
                MessageBox.Show("Дваайте еще раз попробуем");
            }
            
           

        }

        private void btnNewCode_Click(object sender, RoutedEventArgs e)
        {
        
            StartAthorization();
        }

        private void btnAuthorizationTwo_Click(object sender, RoutedEventArgs e)
        {

            tbLogin.Text = "";          
            tbPassword.Text = "";
            authSucc.Visibility = Visibility.Collapsed;
            stackLoginPassword.Visibility = Visibility.Visible;
            btnAuth.Visibility = Visibility.Visible;
            btnNewCode.Visibility = Visibility.Collapsed;
            tbNewCode.Visibility = Visibility.Collapsed;
            tbLogin.IsEnabled = true;
            tbPassword.IsEnabled = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "admin" && tbPassword.Text != "admin")
            {
                MessageBox.Show("Удостоверьтесь в корректности введеных учетной записи");
            }
            else
            {
                StartAthorization();
            }


        }

        private void btnNewCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            e.Handled = true;
            StartAthorization();

        }
      
        private void btnAuth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            e.Handled = true;
            StartAthorization();
            
        }
    }
}
