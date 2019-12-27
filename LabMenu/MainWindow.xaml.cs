using LabMenu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace LabGridMasterDetail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Movie> movies = new List<Movie>();

        private string defaultPath = "C:\\Users\\soblab\\Desktop\\LabMenu\\MovieInputFile.txt"; //Default Path

        public MainWindow()
        {
            InitializeComponent();
        }

        //User clicks open menu to search for a file to read
        private void OpenMenu_Clicked(object sender, RoutedEventArgs e)
        {
            string exeDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (Directory.Exists("d"))
            {
                dlg.InitialDirectory = defaultPath;
            }
            else
            {
                dlg.InitialDirectory = @"C:\";
            }
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt";
            

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                readFile(dlg.FileName);

            }
        }

        //Reads in from the file then writes into listbox
        private void readFile(string file)
        {
            try
            {
                Movie emp;
                Actor act;
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(file))
                {

                    while (!sr.EndOfStream)
                    {
                        emp = new Movie();
                        emp.Name = sr.ReadLine();

                        if (int.TryParse(sr.ReadLine(), out int score))
                        {
                            emp.RottenTomatosScore = score;
                        }
                        else
                        {
                            emp.RottenTomatosScore = -1;
                        }

                        emp.Review = sr.ReadLine();

                        emp.MoviePicture = sr.ReadLine();

                        for (int i = 0; i < 2; i++)
                        {
                            act = new Actor();
                            act.FirstName = sr.ReadLine();
                            act.LastName = sr.ReadLine();
                            emp.Actors.Add(act);
                        }
                        listBoxMovieName.Items.Add(emp);
                    }
                    sr.Close();
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        //User clicks save menu to save the current information
        private void SaveMenu_Clicked(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (Directory.Exists(defaultPath))
            {
                dlg.InitialDirectory = defaultPath;
            }
            else
            {
                dlg.InitialDirectory = @"C:\";
            }
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt";
            dlg.ShowDialog();
            string fileName = dlg.FileName;
            if (fileName != "")
            {
                //writeFile(dlg.FileName);
            }
        }

        //private void writeFile(string file)
        //{
        //    try
        //    {   // Open the text file using a stream reader.
        //        using (StreamWriter sr = new StreamWriter(file))
        //        {
        //            Movie emp = new Movie();
        //            emp.Id = textBoxMovieName.Text;
        //            emp.Department = textBoxMovieScore.Text;

        //            sr.WriteLine(emp.Id);
        //            sr.WriteLine(emp.Department);
        //            sr.Close();
        //        }

        //    }
        //    catch (IOException e)
        //    {
        //        Console.WriteLine("The file could not be written:");
        //        Console.WriteLine(e.Message);
        //    }
        //}

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBoxMovie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movie m = (Movie)e.AddedItems[0];
            ShowMovieDetails(m);
        }

        private void ShowMovieDetails(Movie m)
        {
            textBoxMovieName.Text = m.Name;
            textBoxMovieScore.Text = m.RottenTomatosScore.ToString();
            textBoxReview.Text = m.Review;

            string fullPathFileName = Environment.CurrentDirectory + "\\" + m.MoviePicture;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullPathFileName);
            bitmap.EndInit();
            imageMoviePoster.Source = bitmap;

            listViewActors.Items.Clear();
            foreach(Actor a in m.Actors)
            {
                listViewActors.Items.Add(a);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            readFile(defaultPath);
        }
    }
}
