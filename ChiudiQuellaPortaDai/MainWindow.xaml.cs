using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChiudiQuellaPortaDai
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            tbSoundPath.Text = @"C:\Users\pcattaneo\Desktop\LEK\chiudiquellaporta.wav";
            Loaded += MainWindow_Loaded;
            KeyUp += MainWindow_KeyUp;        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space || e.Key == Key.Enter)
            {
                ChiudiQuellaPorta();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Button_Click(null, null);
        }

        private void ChiudiQuellaPorta()
        {
            mediaPlayer.Open(new Uri(tbSoundPath.Text, UriKind.Absolute));
            mediaPlayer.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChiudiQuellaPorta();
        }
    }
}
