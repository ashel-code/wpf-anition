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
                mainFrame.Height = image.Height; // адаптируем высоту хоста под высоту картинки
                mainFrame.Children.Add(image); // ставиться картинка. Как написали бы ребята из Apple -- магия
                //using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate)) // оставь пусть будет пока что
                //{}
            }
        }

        void saveFrame()
        {
            string path = "F:\\file.jpg";
            double
                x1 = mainFrame.Margin.Left,
                x2 = mainFrame.Margin.Top,
                x3 = mainFrame.Margin.Right,
                x4 = mainFrame.Margin.Bottom;

            if (path == null) return;

            mainFrame.Margin = new Thickness(0, 0, 0, 0);

            Size size = new Size(mainFrame.Width, mainFrame.Height);
            mainFrame.Measure(size);
            mainFrame.Arrange(new Rect(size));

            RenderTargetBitmap renderBitmap =
             new RenderTargetBitmap(
               (int)size.Width,
               (int)size.Height,
               96,
               96,
               PixelFormats.Default);
            renderBitmap.Render(mainFrame);
            using (FileStream fs = File.Open(path, FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(fs);
            }
            mainFrame.Margin = new Thickness(x1, x2, x3, x4);
        }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            openImage();

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            saveFrame();
        }
    }
}
