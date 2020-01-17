using ParkingTicketMachine.Core;
using System;
using System.Text;
using System.Windows;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private FastClock _clock = FastClock.Instance;
        private DateTime _time = new DateTime();

        public MainWindow()
        {
            InitializeComponent();
            _clock.OneMinuteIsOver += _clock_OneMinuteIsOver;
            _clock.IsRunning = true;
            _time = _clock.Time;
        }

        private void _clock_OneMinuteIsOver(object sender, DateTime e)
        {
            _time = _time.AddMinutes(1);
            Title = $"Parkscheinzentrale, {_time.ToShortTimeString()}";
        }
   

        private void Window_Initialized(object sender, EventArgs e)
        {
            Title = $"Parkscheinzentrale,  {_clock.Time.ToShortTimeString()}";

            SlotMachineWindow window1 = new SlotMachineWindow("Limesstrasse", OnTicketPrint);
            window1.Show();
            window1.Title = "Limesstrasse";
            SlotMachineWindow window2 = new SlotMachineWindow("Landstrasse", OnTicketPrint);
            window2.Show();
            window2.Title = "Landstraße";
        }

        private void OnTicketPrint(object sender, Ticket ticket)
        {
            TextBlockLog.Text = $"{ticket.TimePrinted.ToShortTimeString()}      {ticket.Location}: {ticket.TimePrinted.ToString()} {ticket.PricePaid} Cent";
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            SlotMachineWindow windowNew = new SlotMachineWindow(TextBoxAddress.Text, OnTicketPrint);
            windowNew.Owner = this;
            windowNew.Title = TextBoxAddress.Text;
            windowNew.Show();
        }
    }
}
