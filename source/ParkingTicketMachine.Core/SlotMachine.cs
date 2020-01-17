using System;

namespace ParkingTicketMachine.Core
{
    public class SlotMachine
    {
        private FastClock _clock = FastClock.Instance;
        private double _currentInput = 0;
        private int[] _acceptedValues = new int[] { 10, 20, 50, 100, 200 };
        private string _location;
        public Ticket CurrentTicket { get; private set; } = null;
        
        public SlotMachine(string location)
        {
            _location = location;
        }

        public void InsertCoin(int selectedIndex)
        {
            _clock.IsRunning = false;
            _currentInput += _acceptedValues[selectedIndex];
            CurrentTicket = new Ticket(_currentInput, _location);
        }

        public void PrintTicket(EventHandler<Ticket> _ticketReady)
        {
            CurrentTicket.TicketPrint += _ticketReady;
            CurrentTicket.OnTicketPrint();
            _clock.IsRunning = true;
        }

        public void Cancel()
        {
            CurrentTicket = null;
            _currentInput = 0;
            FastClock.Instance.IsRunning = true;
        }
    }
}
