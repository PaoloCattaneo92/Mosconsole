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
        private readonly MediaPlayer mediaPlayer = new MediaPlayer() { Volume = 1 };

        private Dictionary<string, List<MoscoAudio>> audios;
        private Dictionary<string, TextBox> tbSearch;

        private readonly List<MoscoAudio> mosconi;
        private readonly MoscoAudio chiudi;
        private readonly Dictionary<Key, int> NUMBER_KEYS = new Dictionary<Key, int>
        {
            { Key.NumPad1, 0 },
            { Key.D1, 0 },
            { Key.NumPad2, 1 },
            { Key.D2, 1 },
            { Key.NumPad3, 2 },
            { Key.D3, 2 },
            { Key.NumPad4, 3 },
            { Key.D4, 3 },
            { Key.NumPad5, 4 },
            { Key.D5, 4 },
            { Key.NumPad6, 5 },
            { Key.D6, 5 },
            { Key.NumPad7, 6 },
            { Key.D7, 6 },
            { Key.NumPad8, 7 },
            { Key.D8, 7 },
            { Key.NumPad9, 8 },
            { Key.D9, 8 },
            { Key.NumPad0, 9 },
            { Key.D0, 9 }
        };

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            mosconi = new List<MoscoAudio>();
            foreach (string file in Directory.EnumerateFiles(Settings.Default.MosconiAudioFolder))
            {
                mosconi.Add(new MoscoAudio(file));
            }
            chiudi = mosconi.FirstOrDefault(a => a.Name == Settings.Default.ChiudiName);
            if(chiudi == null)
            {
                MessageBox.Show("Chiudi quella porta file not found (looking for " + Settings.Default.ChiudiName + ")");
            }
            lvMosconi.ItemsSource = mosconi;
            tbSearchMosconi.Focus();
            tbCount.Text = $"{mosconi.Count} audio trovati";
            tbCount.ToolTip = $"Cercati nella cartella {Settings.Default.MosconiAudioFolder}";
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ChiudiQuellaPorta();
            btnChiudi.Focus();
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

        private void TbSearchMosconi_KeyUp(object sender, KeyEventArgs e)
        {
            new FilterWorker(mosconi, lvMosconi, tbSearchMosconi).Run();
        }

        private void TabItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                ChiudiQuellaPorta();
            }
        }

        private void TabItem_KeyUp_Mosconi(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(lvMosconi.Items.Count > 0)
                {
                    var a = (MoscoAudio)lvMosconi.Items[0];
                    mediaPlayer.Open(a.Uri);
                    mediaPlayer.Play();
                }
            }
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key != Key.Back)
            {
                try
                {
                    var i = NUMBER_KEYS[e.Key];
                    var a = (MoscoAudio)lvMosconi.Items[i];
                    mediaPlayer.Open(a.Uri);
                    mediaPlayer.Play();
                }
                catch (KeyNotFoundException)
                {
                    if (lvMosconi.Items.Count > 0)
                    {
                        var a = (MoscoAudio)lvMosconi.Items[0];
                        mediaPlayer.Open(a.Uri);
                        mediaPlayer.Play();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    if (lvMosconi.Items.Count > 0)
                    {
                        var a = (MoscoAudio)lvMosconi.Items[lvMosconi.Items.Count - 1];
                        mediaPlayer.Open(a.Uri);
                        mediaPlayer.Play();
                    }
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((TabControl)sender).SelectedIndex == 1)
            {
                tbSearchMosconi.Focus();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.F)
            {
                tbSearchMosconi.Focus();
            }
            if (Keyboard.IsKeyDown(Key.LeftShift) && e.Key != Key.Back)
            {
                switch (e.Key)
                {
                    case Key.D1:
                        tabControl.SelectedIndex = 0;
                        break;
                    case Key.D2:
                        tabControl.SelectedIndex = 1;
                        break;
                }
            }
        }
    }
}
