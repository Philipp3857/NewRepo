using EasyBankingZinsüberschuss.Datenhaltung.Datenbank;
using EasyBankingZinsüberschuss.Datenverarbeitung;
using System.Reflection.Metadata.Ecma335;

namespace EasyBankingZinsüberschuss.Datenhaltung.Transfer
{

   

        public class VolumenNeugeschäft
        {
            private decimal _autokredite;
            public decimal Autokredite { get { return _autokredite; } }

            private decimal _girokonten;
            public decimal Girokonten { get { return _girokonten; } }

            private decimal _hypothekenkredite;
            public decimal Hypothekenkredite { get { return _hypothekenkredite; } }

            private decimal _konsumkredite;
            public decimal Konsumkredite { get { return _konsumkredite; } }

            private decimal _spareinlagen;
            public decimal Spareinlagen { get { return _spareinlagen; } }

            private decimal _termingelder;
            public decimal Termingelder { get { return _termingelder; } }

        public VolumenNeugeschäft(
decimal konsumkredite,
decimal autokredite,
decimal hypothekenkredite,
decimal girokonten,
decimal spareinlagen,
decimal termingelder
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
                VolumenNeugeschäft volumenNeugeschäft = obj as VolumenNeugeschäft;

                return volumenNeugeschäft != null && _autokredite.Equals(volumenNeugeschäft._autokredite) && _girokonten.Equals(volumenNeugeschäft._girokonten)
                    && _hypothekenkredite.Equals(volumenNeugeschäft._hypothekenkredite) && _konsumkredite.Equals(volumenNeugeschäft._konsumkredite)
                    && _spareinlagen.Equals(volumenNeugeschäft._spareinlagen) && _termingelder.Equals(volumenNeugeschäft._termingelder);
            }
            public override string ToString()
            {
                return $"Autokredite: {_autokredite} Girokonten: {_girokonten} Hypothekenkredite: {_hypothekenkredite}" +
                    "\n" + $"Konsumkredite: {_konsumkredite} Spareinlagen: {_spareinlagen} Termingelder: {_termingelder}";

            }
            public override int GetHashCode()
            {
                return _autokredite.GetHashCode() ^ _girokonten.GetHashCode() ^ _hypothekenkredite.GetHashCode() ^ _konsumkredite.GetHashCode() ^ _spareinlagen.GetHashCode() ^ _termingelder.GetHashCode();
            }
        }
    
}