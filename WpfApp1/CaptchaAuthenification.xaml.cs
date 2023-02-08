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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CaptchaAuthenification.xaml
    /// </summary>
    public partial class CaptchaAuthenification : Window
    {
        string text = "";
        public CaptchaAuthenification()
        {
            InitializeComponent();
            generateCaptcha();



        }

        void generateCaptcha()
        {
            canvas.Children.Clear();
            text = "";
            Random random = new Random();
            int countLine = random.Next(10, 22);
            int countText = random.Next(7, 11);
            for (int i = 0; i < countLine; i++)
            {
                Brush brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(1, 255), (byte)random.Next(1, 255), (byte)random.Next(1, 233)));

                Line line = new Line
                {
                    X1 = random.Next(Convert.ToInt32(canvas.Width)),
                    Y1 = random.Next(Convert.ToInt32(canvas.Height)),
                    X2 = random.Next(Convert.ToInt32(canvas.Width)),
                    Y2 = random.Next(Convert.ToInt32(canvas.Height)),
                    Stroke = brush,
                    Fill = brush,
                };
                canvas.Children.Add(line);
            }
           
            for (int i = 0; i < countText; i++)
            {
                int selected = random.Next(0, 2); // 2 не включительно
                switch (selected)
                {
                    case 0:
                        text += Convert.ToString(random.Next(10));
                        break;
                    case 1:
                        // заглавная маленькая
                        selected = random.Next(2);
                        switch (selected)
                        {
                            case 0:
                                text += Convert.ToChar(random.Next('A', 'Z' + 1)); // с z вкдючительно
                                break;
                            case 1:
                                text += Convert.ToChar(random.Next('a', 'z' + 1)); // с z вкдючительно
                                break;
                        }

                        break;
                }




            }
            // делим область на прямоугольники
            int start = 0, end = 0; // начало и конец нашего прямоугольника
            int step = (int)(canvas.Width / text.Length); // ищем область, на которую делим
            for (int i = 0; i < text.Length; i++)
            {
                // делим нашу область на прямоугольники
                switch (i)
                {
                    case 0:
                        end = end + step; // нашли конец первoго
                        break;

                    default:
                        // начало следующим является конец предыдущего
                        start = end;
                        // находмм конец и соответственно начало следующего
                        end = end + step;

                        break;
                }
                // далее нам необходимо обрезать наш канвас на меньший прямоугольник, для того, чтобы 

                int width = random.Next(start, end);
                int height = random.Next((int)canvas.Height);
               // Width = "380" Height = "190"
                if (height > 170) height = height - 30;
                if (width > 360) end = end - 10;
                int selectedTwo = random.Next(3); // 3 невключительно
                switch (selectedTwo)
                {

                    case 0:
                        {
                            TextBlock block = new TextBlock
                            {
                                Text = Convert.ToString(text[i]),
                                FontSize = 15,
                                FontWeight = FontWeights.Bold,
                                Padding = new Thickness(width, height, 0, 0),


                            };
                            canvas.Children.Add(block);

                            break;
                        }
                    case 1:
                        {
                            TextBlock block = new TextBlock
                            {
                                Text = Convert.ToString(text[i]),
                                FontSize = 15,
                                FontWeight = FontWeights.Bold,
                                FontStyle = FontStyles.Italic,
                                Padding = new Thickness(width, height, 0, 0),

                            };
                            canvas.Children.Add(block);
                            break;
                        }

                    case 2:
                        {
                            TextBlock block = new TextBlock
                            {
                                Text = Convert.ToString(text[i]),
                                FontSize = 15,
                                FontStyle = FontStyles.Italic,
                                Padding = new Thickness(width, height, 0, 0),

                            };
                            canvas.Children.Add(block);
                            break;
                        }

                }

                // 380 / 190
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            if(tbCaptcha.Text == text)
            {
               var result =  MessageBox.Show("Успешное прохождение финальной аутентификации", "От Никиты", MessageBoxButton.OK, MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    MessageBox.Show("Владелец Никита Герасимов");
                    if (result == MessageBoxResult.OK)
                    {

                        var resultTwo =   MessageBox.Show("Прошу поставить 5", "От Никиты", MessageBoxButton.OKCancel);
                                      

                        if(resultTwo == MessageBoxResult.OK)
                        {
                            MessageBox.Show("Спасибо");
                        }
                        else
                        {
                            MessageBox.Show("Почему? =(");
                        }

                    }

                }
                this.Close();
           
            }
           
            else
            {
                MessageBox.Show("Не верно!");
                tbCaptcha.Text = "";
                generateCaptcha();
            }
        }

        private void btnNewCode_Click(object sender, RoutedEventArgs e)
        {
            tbCaptcha.Text = "";
            generateCaptcha();
        }
    }
}
