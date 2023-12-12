using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBankingZinsüberschuss.Datenhaltung.Datenbank;
using EasyBankingZinsüberschuss.Datenverarbeitung;
namespace EasyBankingZinsüberschuss.Datenhaltung.Transfer
{

    
        public class Periode
        {
            private DateTime _beginn;
            private DateTime _ende;
            private int _id;

            public DateTime Beginn { get { return _beginn; } }
            public DateTime Ende { get { return _ende; } }
            public int Id { get { return _id; } }

            public Periode(int nummer, DateTime beginn, DateTime ende)
            {
                if (nummer > 0)
                {
                    throw new ArgumentException();
                }
                _beginn = beginn;
                _ende = ende;
                _id = nummer;
            }
            public override int GetHashCode()
            {

                return _beginn.GetHashCode() ^ _ende.GetHashCode() ^_id.GetHashCode();
            }
            public override string ToString()
            {

                return $"Beginn: {_beginn.ToString()} Ende: {_ende.ToString()} ID: {_id.ToString()}";
            }
            public override bool Equals(object? obj)
            {
                Periode objperiode = obj as Periode;

                return objperiode != null && _beginn.Equals(objperiode._beginn) && _ende.Equals(objperiode._ende) && _ende.Equals(objperiode._id);
            }
        }
    }

