using System;

namespace VaccineClassLib
{
    public class Vaccine
    {
        // Backing fields
        private string _producer;
        private int _price;
        private int _efficiency;


        // Properties
        public int Id { get; set; }

        public string Producer
        {
            get { return _producer; }

            set
            {
                if (value.Length < 4) // hvis længden på strengen er mindre end 4, skal vi throw
                {
                    throw new ArgumentException("Producer name too short");
                }
                else
                {
                    _producer = value;
                }
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value < 0) // tjekker på value - hvis value er mindre end 0 (den må være større og =)
                {
                    throw new ArgumentOutOfRangeException("Price must be 0 or higher");
                }
                else
                {
                    _price = value;
                }
                
            }
        }

        public int Efficiency
        {
            get { return _efficiency; }
            set
            {
                if (value < 50) // Efficiency skal være større el. lig med 50, samt mindre eller lig med 100
                {
                    throw new ArgumentOutOfRangeException("Efficiency must be at least 50");
                }
                else if (value > 100)
                {
                    throw new ArgumentOutOfRangeException("Efficiency must be 100 or below");
                }
                else
                {
                    _efficiency = value;
                }

                /* Det kan også skrives samlet på denne måde - ved test er ovenstående smartest, da man kan teste konkret på UNDER 50 og OVER 100, den er mere konkret i test
                if (value < 50 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("Efficiency must be between 50 and 100");
                }
                else
                {
                    _efficiency = value;

                } 
                */
            }
        }

        // Den automatiske måde at lave en ToString()
        // Extensions --> Resharper --> Edit --> Generate Code (alt. ALT + INSERT), vælg: "Formatting members", vælg de fire properties (ikke de tre backing fields)

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Producer)}: {Producer}, {nameof(Price)}: {Price}, {nameof(Efficiency)}: {Efficiency}";
        }

        // OPG 2 - UNIT TEST

        // Hvad er et godt Code Coverage? Det er fx når man får testet sine properties - grænseværdier, og at det virker, når vi opretter et alm. objekt.
    }
}
