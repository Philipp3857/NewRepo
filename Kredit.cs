

using System;
using System.ComponentModel;
using EasyBankingZinsüberschuss.Datenhaltung.Datenbank;
using EasyBankingZinsüberschuss.Datenverarbeitung;
namespace EasyBankingZinsüberschuss.Datenhaltung.Transfer
{

  
        public class Kredit
        {
            private decimal _forderungen;
            public decimal Forderungen { get { return _forderungen; } }

            private decimal _überziehungskredit;
            public decimal Überziehungskredit { get { return _überziehungskredit; } }

            private decimal _verbindlichkeiten;

            public decimal Verbindlichkeiten { get { return _verbindlichkeiten; } }

            public override bool Equals(object? obj)
            {
                Kredit kredit = obj as Kredit;
                if (kredit == null)
                {
                    return false;
                }
                return kredit != null && _forderungen.Equals(kredit._forderungen) && _überziehungskredit.Equals(kredit._überziehungskredit) && _verbindlichkeiten.Equals(kredit._verbindlichkeiten);
            }

            public override int GetHashCode()
            {
                return _forderungen.GetHashCode() ^ _verbindlichkeiten.GetHashCode();
            }
            public override string ToString()
            {
                return $"Verbindlichkeiten: {_verbindlichkeiten}" + "\n" + $"Forderungen: {_forderungen}";
            }
            public Kredit(decimal überziehungskredit, decimal verbindlichkeiten, decimal forderungen)
            {
                if (forderungen < 0 || überziehungskredit < 0 || verbindlichkeiten < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (forderungen == null || überziehungskredit == null || verbindlichkeiten == null)
                {
                    throw new ArgumentNullException();
                }
                _forderungen = forderungen;
                _überziehungskredit = überziehungskredit;
                _verbindlichkeiten = verbindlichkeiten;
            }
        }
    }
