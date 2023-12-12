//nasa1021, Sahra Napolitano
using System;
using EasyBankingZinsüberschuss.Datenhaltung.Transfer;
namespace EasyBankingZinsüberschuss.Datenverarbeitung
{
    public class Zinsüberschuss
    {
        public decimal ForderungenAnKunden { get; }
        public decimal SonstigeForderungenAnKreditinstitute { get; }
        public decimal Sonstiges { get; }
        public decimal Überziehungskredit { get; }
        public decimal VerbindlichkeitenGegenüberKreditinstituten { get; }
        public decimal VerbindlichkeitenGegenüberKunden { get; }
        public decimal Zinsaufwand { get; }
        public decimal ZinsAutokredit { get; }
        public decimal Zinsertrag { get; }
        public decimal ZinsGirokonto { get; }
        public decimal ZinsHypothekenkredit { get; }
        public decimal ZinsKonsumkredit { get; }
        public decimal ZinsSpareinlage { get; }
        public decimal ZinsTermingeld { get; }
        public decimal ZinsüberschussBrutto { get; }

        /// <summary>
        /// Berrechnung des Zinsüberschusses mit Zwischenrechnung die für den Zinsüberschuss gebraucht werden
        /// </summary>
        /// <param name="volumenNeugeschäftAP"></param>
        /// <param name="volumenNeugeschäftVP"></param>
        /// <param name="volumenNeugeschäftVVP"></param>
        /// <param name="volumenNeugeschäftVVVP"></param>
        /// <param name="volumenNeugeschäftVVVVP"></param>
        /// <param name="zinssatzAP"></param>
        /// <param name="zinssatzVP"></param>
        /// <param name="zinssatzVVP"></param>
        /// <param name="zinssatzVVVP"></param>
        /// <param name="zinssatzVVVVP"></param>
        /// <param name="kreditAP"></param>
        /// <exception cref="Exception">wenn Geschäftsperioden oder Zinssatzperioden oder Kreditperiode keinen Wert haben </exception>

        public Zinsüberschuss(VolumenNeugeschäft volumenNeugeschäftAP,
    VolumenNeugeschäft volumenNeugeschäftVP,
    VolumenNeugeschäft volumenNeugeschäftVVP,
    VolumenNeugeschäft volumenNeugeschäftVVVP,
    VolumenNeugeschäft volumenNeugeschäftVVVVP,
    Zinssatz zinssatzAP,
    Zinssatz zinssatzVP,
    Zinssatz zinssatzVVP,
    Zinssatz zinssatzVVVP,
    Zinssatz zinssatzVVVVP,
    Kredit kreditAP)
        //Wertzuweisung Parameter im Konstruktor
        {
            if (volumenNeugeschäftAP == null || volumenNeugeschäftVP == null || volumenNeugeschäftVVP == null || volumenNeugeschäftVVVP == null || volumenNeugeschäftVVVVP == null || zinssatzAP == null || zinssatzVP == null || zinssatzVVP == null || zinssatzVVVP == null || zinssatzVVVVP == null || kreditAP == null)
                throw new Exception();



            SonstigeForderungenAnKreditinstitute = kreditAP.Forderungen;

            Sonstiges = Überziehungskredit + VerbindlichkeitenGegenüberKreditinstituten;

            Überziehungskredit = kreditAP.Überziehungskredit;

            VerbindlichkeitenGegenüberKreditinstituten = kreditAP.Verbindlichkeiten;

            Sonstiges = Überziehungskredit + VerbindlichkeitenGegenüberKreditinstituten;

            ZinsGirokonto = (decimal)zinssatzAP.Girokonten * volumenNeugeschäftAP.Girokonten;

            ZinsSpareinlage = (decimal)zinssatzAP.Spareinlagen * (decimal)volumenNeugeschäftAP.Spareinlagen;
            ZinsTermingeld = ((((decimal)zinssatzVP.Termingelder + (decimal)1.0) * (decimal)volumenNeugeschäftVP.Termingelder) * (decimal)zinssatzVP.Termingelder) + ((decimal)volumenNeugeschäftAP.Termingelder * (decimal)zinssatzAP.Termingelder);


            VerbindlichkeitenGegenüberKunden = ZinsGirokonto + ZinsSpareinlage + ZinsTermingeld;

            Zinsaufwand = VerbindlichkeitenGegenüberKunden + Sonstiges;
            ZinsKonsumkredit = (decimal)zinssatzAP.Konsumkredite * (decimal)volumenNeugeschäftAP.Konsumkredite;

            ZinsAutokredit = ((decimal)zinssatzVP.Autokredite * (volumenNeugeschäftVP.Autokredite * (decimal)0.5)) + ((decimal)zinssatzAP.Autokredite * volumenNeugeschäftAP.Autokredite);
            //Teilvariabeln von ZinsHypothekenkredit
            decimal ZinsVVVVP, ZinsVVVP, ZinsVVP, ZinsVP, ZinsAP;
            ZinsVVVVP = (decimal)zinssatzVVVVP.Hypothekenkredite * ((decimal)volumenNeugeschäftVVVVP.Hypothekenkredite * (decimal)0.2);
            ZinsVVVP = (decimal)zinssatzVVVP.Hypothekenkredite * ((decimal)volumenNeugeschäftVVVP.Hypothekenkredite * (decimal)0.4);
            ZinsVVP = (decimal)zinssatzVVP.Hypothekenkredite * ((decimal)volumenNeugeschäftVVP.Hypothekenkredite * (decimal)0.6);
            ZinsVP = (decimal)zinssatzVP.Hypothekenkredite * ((decimal)volumenNeugeschäftVP.Hypothekenkredite * (decimal)0.8);
            ZinsAP = (decimal)zinssatzAP.Hypothekenkredite * (decimal)volumenNeugeschäftAP.Hypothekenkredite;
            ZinsHypothekenkredit = ZinsVVVVP + ZinsVVVP + ZinsVVP + ZinsVP + ZinsAP;

            ForderungenAnKunden = ZinsKonsumkredit + ZinsAutokredit + ZinsHypothekenkredit;

            Zinsertrag = ForderungenAnKunden + SonstigeForderungenAnKreditinstitute;

            ZinsüberschussBrutto = Zinsertrag - Zinsaufwand;
        }


    }
}