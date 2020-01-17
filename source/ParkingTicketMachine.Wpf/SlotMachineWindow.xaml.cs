using System;
using System.Windows;
using ParkingTicketMachine.Core;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for SlotMachineWindow.xaml
    /// </summary>
    public partial class SlotMachineWindow
    {
        private SlotMachine _sm;
        private EventHandler<Ticket> _ticketReady;

        public SlotMachineWindow(string name, EventHandler<Ticket> ticketReady)
        {
            InitializeComponent();
            _ticketReady = ticketReady;
            _sm = new SlotMachine(name);
        }

        private void ButtonInsertCoin_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxCoins.SelectedItem != null)
            {
                _sm.InsertCoin(ListBoxCoins.SelectedIndex);
                if(_sm.CurrentTicket.PricePaid >= 50)
                {
                    TextBoxTimeUntil.Text = _sm.CurrentTicket.ValidThru.ToShortTimeString();
                }
            }
            else
            {
                MessageBox.Show("Münze auswählen!");
            }
        }

        private void ButtonPrintTicket_Click(object sender, RoutedEventArgs e)
        {
            _sm.PrintTicket(_ticketReady);

            TextBoxTimeUntil.Text = $"{_sm.CurrentTicket.ValidThru.ToShortTimeString()}";
            
            if(_sm.CurrentTicket.PricePaid >= 50)
            {
                MessageBox.Show($"Sie dürfen bis {_sm.CurrentTicket.ValidThru.ToString()} parken");
            }
            else
            {
                TextBoxTimeUntil.Text = "";
                MessageBox.Show("Nicht genügend Münzen eingeworfen");
            }
            _sm.Cancel();

            TextBoxTimeUntil.Text = "";
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            _sm.Cancel();
        }

    }
}
