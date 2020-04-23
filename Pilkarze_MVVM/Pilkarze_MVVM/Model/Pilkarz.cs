using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilkarze_MVVM.Model
{
    class Pilkarz
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public uint Wiek { get; set; }  
        public uint Waga { get; set; }

        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }

        public override string ToString()
        {
            return $"{Nazwisko} {Imie} lat: {Wiek} waga: {Waga} kg";
        }

        public bool isTheSame(Pilkarz pilkarz)
        {
            if (pilkarz.Nazwisko != Nazwisko) return false;
            if (pilkarz.Imie != Imie) return false;
            if (pilkarz.Wiek != Wiek) return false;
            if (pilkarz.Waga != Waga) return false;
            return true;
        }

        public Pilkarz()
        { }

    }
}
