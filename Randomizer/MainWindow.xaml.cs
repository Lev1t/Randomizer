using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Randomizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            SetUpNotifyIcon();
            Closing += ExitApplication;
            var workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = workArea.Right - this.Width;
            this.Top = workArea.Bottom - this.Height;
        }
        private void SetUpNotifyIcon()
        {
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
            _notifyIcon.Click += (s, args) => this.Activate();
            _notifyIcon.Icon = Randomizer.Properties.Resources.Bear;
            _notifyIcon.Visible = true;

            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip
            {
                ShowImageMargin = false
            };
            _notifyIcon.ContextMenuStrip.Items.Add("Randomizer").Click += (s, e) => ShowMainWindow();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication(s, e);
        }
        private void ExitApplication(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            Application.Current.Shutdown();
        }

        private void ShowMainWindow()
        {
            if (this.IsVisible)
                this.Hide();
            else
                this.Show();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((Label)sender).Content = new Random().Next(99);
        }

        private void MenuItem_Hide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            ((Window)sender).Topmost = true;
        }
    }
}
