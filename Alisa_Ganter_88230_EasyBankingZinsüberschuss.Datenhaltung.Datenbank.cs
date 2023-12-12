using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
using EasyBankingZinsüberschuss.Datenhaltung.Transfer;
using System.Data.SqlClient;
#nullable disable

namespace EasyBankingZinsüberschuss.Datenhaltung.Datenbank
{
    /// <summary>
    /// Mit dieser Klasse verschafft man sich Zugriff auf die Datenbank
    /// </summary>
    public static class Datenbank
    {
        private static int[] _periodenIDs = null;

        /// <summary>
        /// Gibt an, ob der Zugriff auf die Datenbank besteht. Wenn ja, dann wird true ausgegeben. Wenn nein, dann wird false ausgegeben
        /// </summary>
        public static bool IstGeladen
        {
            get
            {
                return _periodenIDs != null;
            }
        }

        /// <summary>
        /// Hier werden alle Perioden IDs aufgelistet. Wenn keine Datenbank geladen ist, dann wird eine Exception geworfen
        /// </summary>
        public static int[] PeriodenIDs
        {
            get
            {
                return IstGeladen ? _periodenIDs.Clone() as int[] : throw new Exception("Datenbank nicht geladen!");
            }
        }

        private static DataTable _tabelleKredite = new DataTable();
        private static DataTable _tabellePerioden = new DataTable();
        private static DataTable _tabelleVolumenNeugeschäft = new DataTable();
        private static DataTable _tabelleZinssätze = new DataTable();

        /// <summary>
        /// Hier wird die Datenbank 'Bank', mit ihren jeweiligen Tabellen 'Kredite', 'Perioden', 'VolumenNeugeschäft', 'Zinssätze' ausgelesen
        /// </summary>
        /// <param name="pfadZurDatenbank"></param>
        /// <exception cref="Exception"></exception>
        public static void DatenbankAuslesen(string pfadZurDatenbank)
        {
            if (!File.Exists(pfadZurDatenbank))
            {
                throw new Exception();
            }
            string connectionString = "provider=Microsoft.ACE.OLEDB.12.0; data source = " + pfadZurDatenbank;


            OleDbDataAdapter adapterKredite = new OleDbDataAdapter("Select * from Kredite", connectionString);
            DataSet dataSetKredite = new DataSet();
            try
            {
                adapterKredite.Fill(dataSetKredite, "Kredite");
            }
            catch (Exception ex)
            {
                throw new Exception("Laden fehlgeschlagen!", ex);
            }
            _tabelleKredite = dataSetKredite.Tables["Kredite"];


            OleDbDataAdapter adapterPerioden = new OleDbDataAdapter("Select * from Perioden", connectionString);
            DataSet dataSetPerioden = new DataSet();
            try
            {
                adapterPerioden.Fill(dataSetPerioden, "Perioden");
            }
            catch (Exception ex)
            {
                throw new Exception("Laden fehlgeschlagen!", ex);
            }
            _tabellePerioden = dataSetPerioden.Tables["Perioden"];


            OleDbDataAdapter adapterVolumenNeugeschäft = new OleDbDataAdapter("Select * from VolumenNeugeschäft", connectionString);
            DataSet dataSetVolumenNeugeschäft = new DataSet();
            try
            {
                adapterVolumenNeugeschäft.Fill(dataSetVolumenNeugeschäft, "VolumenNeugeschäft");
            }
            catch (Exception ex)
            {
                throw new Exception("Laden fehlgeschlagen!", ex);
            }
            _tabelleVolumenNeugeschäft = dataSetVolumenNeugeschäft.Tables["VolumenNeugeschäft"];


            OleDbDataAdapter adapterZinssätze = new OleDbDataAdapter("Select * from Zinssätze", connectionString);
            DataSet dataSetZinssätze = new DataSet();
            try
            {
                adapterZinssätze.Fill(dataSetZinssätze, "Zinssätze");
            }
            catch (Exception ex)
            {
                throw new Exception("Laden fehlgeschlagen!", ex);
            }
            _tabelleZinssätze = dataSetZinssätze.Tables["Zinssätze"];


            _periodenIDs = new int[_tabellePerioden.Rows.Count];
            for (int i = 0; i < _tabellePerioden.Rows.Count; ++i)
            {
                _periodenIDs[i] = Convert.ToInt32(_tabellePerioden.Rows[i]["ID"]);
            }
        }

        /// <summary>
        /// Liefert zur angegeben Periodennummer die zugehörige Zeile der Tabelle 'Kredite'
        /// </summary>
        /// <param name="periodenID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Kredit Kredit(int periodenID)
        {
            foreach (DataRow row in _tabelleKredite.Rows)
            {
                if (periodenID == Convert.ToInt32(row["ID"]))
                {
                    return new Kredit(Convert.ToDecimal(row["Überziehungskredit"]) * 1000000,
                                        Convert.ToDecimal(row["Verbindlichkeiten"]) * 1000000,
                                        Convert.ToDecimal(row["Forderungen"]) * 1000000);

                }

            }
            throw new Exception();
        }

        /// <summary>
        /// Liefert zur angegebenen Periodennummer die zugehörige Zeile der Tabelle 'Perioden'
        /// </summary>
        /// <param name="periodenID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Periode Periode(int periodenID)
        {
            foreach (DataRow row in _tabellePerioden.Rows)
            {
                if (periodenID == Convert.ToInt32(row["ID"]))
                {
                    return new Periode(Convert.ToInt32(row["ID"]),
                                        Convert.ToDateTime(row["Beginn"]),
                                        Convert.ToDateTime(row["Ende"]));

                }

            }
            throw new Exception();
        }

        /// <summary>
        /// Liefert zur angegebenen Periodennummer die zugehörige Zeile der Tabelle 'VolumenNeugeschäft'
        /// </summary>
        /// <param name="periodenID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static VolumenNeugeschäft VolumenNeugeschäft(int periodenID)
        {
            foreach (DataRow row in _tabelleVolumenNeugeschäft.Rows)
            {
                if (periodenID == Convert.ToInt32(row["ID"]))
                {
                    return new VolumenNeugeschäft(Convert.ToDecimal(row["Konsumkredite"]) * 1000000,
                                                    Convert.ToDecimal(row["Autokredite"]) * 1000000,
                                                    Convert.ToDecimal(row["Hypothekenkredite"]) * 1000000,
                                                    Convert.ToDecimal(row["Girokonten"]) * 1000000,
                                                    Convert.ToDecimal(row["Spareinlagen"]) * 1000000,
                                                    Convert.ToDecimal(row["Termingelder"]) * 1000000);

                }

            }
            throw new Exception();
        }

        /// <summary>
        /// Liefert zur angegebenen Periodennummer die zugehörige Zeile der Tabelle 'Zinssätze'
        /// </summary>
        /// <param name="periodenID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Zinssatz Zinssatz(int periodenID)
        {
            foreach (DataRow row in _tabelleZinssätze.Rows)
            {
                if (periodenID == Convert.ToInt32(row["ID"]))
                {
                    return new Zinssatz(Convert.ToDouble(row["Konsumkredite"]),
                                        Convert.ToDouble(row["Autokredite"]),
                                        Convert.ToDouble(row["Hypothekenkredite"]),
                                        Convert.ToDouble(row["Girokonten"]),
                                        Convert.ToDouble(row["Spareinlagen"]),
                                        Convert.ToDouble(row["Termingelder"]));

                }

            }
            throw new Exception();
        }
    }
}