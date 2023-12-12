using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyBankingZinsüberschuss.Datenhaltung.Datenbank;
using EasyBankingZinsüberschuss.Datenverarbeitung;
using Microsoft.Win32;
using EasyBankingZinsüberschuss.Datenhaltung.Transfer;
namespace Zulassungsaufgabe_Prog2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Access Database Files (*.accdb)|*.accdb|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Datenbank.DatenbankAuslesen(openFileDialog.FileName);
            }
            else
            {
                throw new Exception("DatenBank konnte nicht geladen werden");
            }
            int neustePeriode = Datenbank.PeriodenIDs[Datenbank.PeriodenIDs.Length-1];

            PeriodenNummer.Content = neustePeriode;

            var volumenNeugeschäfte = new VolumenNeugeschäft[Datenbank.PeriodenIDs.Length+1];

            for(int i = Datenbank.PeriodenIDs.Length-4; i < Datenbank.PeriodenIDs.Length+1; i++){
                volumenNeugeschäfte[i] = Datenbank.VolumenNeugeschäft(i);
            }
            var zinssätze = new Zinssatz[Datenbank.PeriodenIDs.Length+1];

            for (int i = Datenbank.PeriodenIDs.Length -4 ; i < Datenbank.PeriodenIDs.Length+1 ; i++) {

                zinssätze[i] = Datenbank.Zinssatz(i);
                    
            }
            Kredit kreditAP = Datenbank.Kredit(Datenbank.PeriodenIDs.Length);
       
            Zinsüberschuss zinsüberschuss = new Zinsüberschuss(volumenNeugeschäfte[Datenbank.PeriodenIDs.Length ], volumenNeugeschäfte[Datenbank.PeriodenIDs.Length - 1],
             volumenNeugeschäfte[Datenbank.PeriodenIDs.Length - 2], volumenNeugeschäfte[Datenbank.PeriodenIDs.Length - 3], volumenNeugeschäfte[Datenbank.PeriodenIDs.Length - 4],
            zinssätze[Datenbank.PeriodenIDs.Length ], zinssätze[Datenbank.PeriodenIDs.Length -1], zinssätze[Datenbank.PeriodenIDs.Length - 2],
             zinssätze[Datenbank.PeriodenIDs.Length - 3], zinssätze[Datenbank.PeriodenIDs.Length - 4],kreditAP
              );

            Konsumkredit.Content = zinsüberschuss.ZinsKonsumkredit.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Autokredit.Content = zinsüberschuss.ZinsAutokredit.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Autokredit.Content = zinsüberschuss.ZinsAutokredit.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             Hypothekenkredit.Content = zinsüberschuss.ZinsHypothekenkredit.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             Kunden.Content = zinsüberschuss.ForderungenAnKunden.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             Girokonto.Content = zinsüberschuss.ZinsGirokonto.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Spareinlage.Content = zinsüberschuss.ZinsSpareinlage.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Termingeld.Content = zinsüberschuss.ZinsTermingeld.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             Kunden1.Content = zinsüberschuss.VerbindlichkeitenGegenüberKunden.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             Überziehungskredit.Content = zinsüberschuss.Überziehungskredit.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             VerbindlichkeitenKreditinstituten.Content = zinsüberschuss.VerbindlichkeitenGegenüberKreditinstituten.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Sonstiges1.Content = zinsüberschuss.Sonstiges.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             Zinsaufwand.Content = zinsüberschuss.Zinsaufwand.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
             Zinsüberschuss.Content = zinsüberschuss.ZinsüberschussBrutto.ToString("N2", new System.Globalization.CultureInfo("de-DE"));

            KundenSp2.Content = zinsüberschuss.ForderungenAnKunden.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Sonstiges.Content = zinsüberschuss.SonstigeForderungenAnKreditinstitute.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Zinsertrag.Content = zinsüberschuss.Zinsertrag.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Kunden1Sp2.Content = zinsüberschuss.VerbindlichkeitenGegenüberKunden.ToString("N2", new System.Globalization.CultureInfo("de-DE"));
            Sonstiges1Sp2.Content = zinsüberschuss.Sonstiges.ToString("N2", new System.Globalization.CultureInfo("de-DE"));

            ZinsaufwandSP3.Content = "-" + zinsüberschuss.Zinsaufwand.ToString("N2", new System.Globalization.CultureInfo("de-DE")) ;
        }
    }
}
