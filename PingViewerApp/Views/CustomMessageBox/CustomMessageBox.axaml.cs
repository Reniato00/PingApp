using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PingViewerApp;

    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }
        
        public CustomMessageBox(string message,string topic, string title = "Information") : this()
        {
            MessageText.Text = message;
            TopicText.Text = topic;
            Title = title;
        }
        
        private void Ok_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
    }



