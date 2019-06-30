using ChiudiQuellaPortaDai.Properties;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<MoscoAudio> audio;
        private MoscoAudio chiudi;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            audio = new List<MoscoAudio>();
            foreach (string file in Directory.EnumerateFiles(Settings.Default.AudioFolder))
            {
                audio.Add(new MoscoAudio(file));
            }
            chiudi = audio.FirstOrDefault(a => a.Name == Settings.Default.ChiudiName);
            lvAudio.ItemsSource = audio;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ChiudiQuellaPorta();
        }

        private void ChiudiQuellaPorta()
        {
            if(chiudi != null)
            {
                mediaPlayer.Open(chiudi.Uri);
                mediaPlayer.Play();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChiudiQuellaPorta();
        }

        private void Btn_Audio_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri(((Button)sender).ToolTip.ToString(), UriKind.Absolute));
            mediaPlayer.Play();
        }

        private void TbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            new FilterWorker(audio, lvAudio, tbSearch).Run();
        }

        private void TabItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                ChiudiQuellaPorta();
            }
        }

        private void TabItem_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(lvAudio.Items.Count > 0)
                {
                    var a = (MoscoAudio)lvAudio.Items[0];
                    mediaPlayer.Open(a.Uri);
                    mediaPlayer.Play();
                }
            }
        }
    }
}
