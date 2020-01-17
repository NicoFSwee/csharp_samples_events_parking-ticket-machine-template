using System;

namespace ParkingTicketMachine.Core
{
    public class Ticket
    {
        public DateTime ValidThru { get; private set; }
        public DateTime TimePrinted { get; private set; }
        public string Location { get; set; }
        public double PricePaid { get; set; }

        public event EventHandler<Ticket> TicketPrint;

        public Ticket(double _currentInput, string location)
        {
            PricePaid = _currentInput;
            TimePrinted = FastClock.Instance.Time;
            ValidThru = CalculateValidThru(PricePaid);
            Location = location;
        }

        private DateTime CalculateValidThru(double _input)
        {
            if(_input >= 150)
            {
                _input = 150;
            }

            DateTime result = TimePrinted;

            double minutesToAdd = 6 * _input / 10;
            result = result.AddMinutes(minutesToAdd);

            if(result.Hour >= 18 && result.Minute > 0 || result.Hour > 18)
            {
                int hoursOver = result.Hour - 18;

                for (int i = TimePrinted.Hour; i != 8; i = result.Hour)
                {
                    result = result.AddHours(1);
                }
                result = result.AddHours(hoursOver);
            }
            else if(result.Hour < 8)
            {
                result = result.AddMinutes(-result.Minute);
                for (int i = TimePrinted.Hour; i != 8; i = result.Hour)
                {
                    result = result.AddHours(1);
                }
                result = result.AddMinutes(minutesToAdd);
            }
            return result;
        }

        public void OnTicketPrint()
        {
            if(PricePaid >= 50)
            {
                TicketPrint?.Invoke(this, this);
            }
        }
    }
}
