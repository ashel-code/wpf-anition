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

using System.IO;
using Microsoft.Win32;

namespace msla_project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Image image; // картинка которуб будем открывать

        //блок переменных для открытия файла
        OpenFileDialog openFileDialog;
        string filename;
        Nullable<bool> resultOpenDialog;

        void openImage ()
        {
 

            //System.brre
            openFileDialog = new OpenFileDialog(); // 
            // dialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*"; // шаблон записи нескольких расширений
            openFileDialog.Filter = "Image (*.png)|*.png|All files (*.*)|*.*"; // расширения файла
            openFileDialog.FilterIndex = 0; // индекс выбранного варианта расширения по умолчанию

            resultOpenDialog = openFileDialog.ShowDialog();

            if (resultOpenDialog == true)
            {
                // Open document
                filename = openFileDialog.FileName; // установка переменной значения пути

                image = new Image
                {
                    Width = mainFrame.Width, // определяет высоту картинки как высоту нашего холста, чтобы подогнать ее по его размерам (по ширине)
                    Source = new BitmapImage(new Uri(filename)) // ну собсвенно путь к картинке вбивается
                };

                mainFrame.Children.Add(image); // ставиться картинка. Как написали бы ребята из Apple -- магия
                //using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate)) // оставь пусть будет пока что
                //{}
            }
        }


        public MainWindow()
        {
            InitializeComponent();
        }


        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            openImage();

        }
    }
}
