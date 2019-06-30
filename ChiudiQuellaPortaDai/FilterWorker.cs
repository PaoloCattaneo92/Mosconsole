using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ChiudiQuellaPortaDai
{
    public class FilterWorker : BackgroundWorker
    {
        public List<MoscoAudio> Source { get; set; }
        public ItemsControl Destination { get; set; }
        public TextBox Search { get; set; }

        public FilterWorker(List<MoscoAudio> source, ItemsControl destination, TextBox search) : this()
        {
            Source = source;
            Destination = destination;
            Search = search;
        }

        public FilterWorker()
        {
            DoWork += FilterWorker_DoWork;
            RunWorkerCompleted += FilterWorker_RunWorkerCompleted;
        }

        public void Run()
        {
            if (!string.IsNullOrWhiteSpace(Search.Text))
            {
                RunWorkerAsync(Search.Text);
            }
            else
            {
                Destination.ItemsSource = null;
                Destination.ItemsSource = Source;
            }
        }

        private void FilterWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                Destination.ItemsSource = null;
                Destination.ItemsSource = (List<MoscoAudio>)e.Result;
            }
        }

        private void FilterWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Source?.Count > 0)
            {
                e.Result = Source.FindAll(m => m.Name.Replace(" ", "").IndexOf((string)e.Argument, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
    }
}
