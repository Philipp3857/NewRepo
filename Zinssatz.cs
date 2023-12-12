using EasyBankingZinsüberschuss.Datenhaltung.Datenbank;
using EasyBankingZinsüberschuss.Datenverarbeitung;

namespace EasyBankingZinsüberschuss.Datenhaltung.Transfer
{

    
        public class Zinssatz
        {
            private double _autokredite;
            public double Autokredite { get { return _autokredite; } }

            private double _girokonten;
            public double Girokonten { get { return _girokonten; } }

            private double _hypothekenkredite;
            public double Hypothekenkredite { get { return _hypothekenkredite; } }

            private double _konsumkredite;
            public double Konsumkredite { get { return _konsumkredite; } }

            private double _spareinlagen;
            public double Spareinlagen { get { return _spareinlagen; } }

            private double _termingelder;
            public double Termingelder { get { return _termingelder; } }

        public Zinssatz(
double konsumkredite,
double autokredite,
double hypothekenkredite,
double girokonten,
double spareinlagen,
double termingelder
)
        { 
                _autokredite = autokredite;

                _girokonten = girokonten;

                _hypothekenkredite = hypothekenkredite;

                _konsumkredite = konsumkredite;

                _spareinlagen = spareinlagen;

                _termingelder = termingelder;

            }
            public override bool Equals(object? obj)
            {
                Zinssatz zinssatz = obj as Zinssatz;

                return zinssatz != null && _autokredite.Equals(zinssatz._autokredite) && _girokonten.Equals(zinssatz._girokonten) && _hypothekenkredite.Equals(zinssatz._hypothekenkredite)
                    && _konsumkredite.Equals(zinssatz._konsumkredite) && _spareinlagen.Equals(zinssatz._spareinlagen) && _termingelder.Equals(zinssatz._termingelder);
            }

            public override int GetHashCode()
            {
                return _autokredite.GetHashCode() ^ _girokonten.GetHashCode() ^ _hypothekenkredite.GetHashCode() ^ _konsumkredite.GetHashCode()
                    ^ _spareinlagen.GetHashCode() ^ _termingelder.GetHashCode();
            }
            public override string ToString()
            {
                return $"Autokredite: {_autokredite} Girokonten: {_girokonten} Hypothekenkredite: {_hypothekenkredite}"
                + "\n" + $"KonsumKredite: {_konsumkredite} Spareinlagen: {_spareinlagen} Termingelder: {_termingelder}";
            }
        }
    
}
