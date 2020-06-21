using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Randomizer.Properties;

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
            this.Left = Settings.Default.Left;
            this.Top = Settings.Default.Top;
        }
        private void SetUpNotifyIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.MouseDoubleClick += ShowMainWindow;
            _notifyIcon.MouseClick += TrayIcon_MouseClick;
            _notifyIcon.Icon = Properties.Resources.Bear;
            _notifyIcon.Visible = true;

            _notifyIcon.ContextMenuStrip = new ContextMenuStrip
            {
                ShowImageMargin = false
            };

            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += ExitApplication;

        }

        private void TrayIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Activate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                _notifyIcon.ContextMenuStrip.Show();
            }
        }
        private void ExitApplication(object sender, EventArgs e)
        {
            StoreWindowLocation();
            _notifyIcon.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        private void ShowMainWindow(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.IsVisible)
                    this.Hide();
                else
                    this.Show();
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((System.Windows.Controls.Label)sender).Content = new Random().Next(99);

            //make the widow sticky to the edges
            Point currentPoints = PointToScreen(Mouse.GetPosition(this));
            var workArea = SystemParameters.WorkArea;

            if (currentPoints.X < 25)
                this.Left = 0;
            else if (currentPoints.X > workArea.Right - this.Width - 25)
                this.Left = workArea.Right - this.Width;

            if (currentPoints.Y < 25)
                this.Top = 0;
            else if (currentPoints.Y > workArea.Height - this.Height - 25)
                this.Top = workArea.Height - this.Height;
        }

        private void MenuItem_Hide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            StoreWindowLocation();
            _notifyIcon.Dispose();
            this.Close();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            ((Window)sender).Topmost = true;
        }

        private void StoreWindowLocation()
        {
            Settings.Default.Top = this.Top;
            Settings.Default.Left = this.Left;
            Settings.Default.Save();
        }
    }
}
