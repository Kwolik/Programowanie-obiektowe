using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace Pilkarze_MVVM.ViewModel
{
    using Model;
    using BaseClass;

    internal class MainViewModel: ViewModelBase
    {
        private Pilkarz pilkarz = new Model.Pilkarz("Jas", "Fasola", 0, 0);
        private Lista lista_model = new Model.Lista();
        private static List<string> list(List<Pilkarz> list)
        {
            List<string> lista = new List<string>();
            for (int i = 0; i < list.Count; i++) lista.Add(list[i].ToString());
            return lista;
        }

        public List<string> Lista_lb { get => list(lista_model.GetPlayers); }


        #region Public interface
        private static string name = "";
        private static string surname = "";
        public string WEPImie { get; set; } = name;
        public string WEPNazwisko { get; set; } = surname;
        public uint sliderWiek { get; set; } = 15;
        public uint sliderWaga { get; set; } = 50;
        public int Index { get; set; } = -1;
        #endregion

        #region Commands
        private ICommand add = null;
        private ICommand remove = null;
        private ICommand modify = null;
        private ICommand load = null;
        private ICommand save = null;
        private ICommand clear = null;

        public ICommand Dodaj
        {
            get
            {
                if (add == null)
                {
                    add = new RelayCommand(
                        arg =>
                        {
                            var biezacyPilkarz = new Pilkarz(WEPImie, WEPNazwisko, sliderWiek, sliderWaga);
                            var czyJuzJestNaLiscie = false;
                            for (int i = 0; i < lista_model.Count(); i++)
                            {
                                Pilkarz p = lista_model.GetPlayers[i];
                                if (p.isTheSame(biezacyPilkarz) == true)
                                {
                                    czyJuzJestNaLiscie = true;
                                    break;
                                }
                            }

                            if (czyJuzJestNaLiscie == true)
                                MessageBox.Show($"{biezacyPilkarz.ToString()} już jest na liście.", "Uwaga");
                            else
                            {
                                lista_model.Add(biezacyPilkarz);
                                onPropertyChanged(nameof(Lista_lb));
                            }
                        },
                        arg => (!string.IsNullOrEmpty(WEPImie)) && (!string.IsNullOrEmpty(WEPNazwisko)) && (WEPImie != name) && (WEPNazwisko != surname) && WEPImie.All(Char.IsLetter) && WEPNazwisko.All(Char.IsLetter)
                     );
                }
                return add;
            }
        }

        public ICommand Usun
        {
            get
            {
                if (remove == null)
                    remove = new RelayCommand(
                        arg => {
                            var mb = MessageBox.Show("Czy na pewno chcesz usunąć zawodnika?", "Uwaga", MessageBoxButton.YesNo);
                            if (mb == MessageBoxResult.Yes)
                            {
                                lista_model.Remove(Index);
                                onPropertyChanged(nameof(Lista_lb));
                            }
                        },
                        arg => Index > -1
                     );
                return remove;
            }
        }

        public ICommand Edytuj
        {
            get
            {
                if (modify == null)
                    modify = new RelayCommand(
                        arg => {

                            var biezacyPilkarz = new Pilkarz(WEPImie, WEPNazwisko, sliderWiek, sliderWaga);
                            var czyJuzJestNaLiscie = false;
                            for (int i = 0; i < lista_model.Count(); i++)
                            {
                                Pilkarz p = lista_model.GetPlayers[i];
                                if (p.isTheSame(biezacyPilkarz) == true)
                                {
                                    czyJuzJestNaLiscie = true;
                                    break;
                                }
                            }
                            if (czyJuzJestNaLiscie == true)
                                MessageBox.Show($"{biezacyPilkarz.ToString()} już jest na liście.", "Uwaga");
                            else
                            {
                                var mb = MessageBox.Show("Czy na pewno chcesz zmienić dane tego zawodnika?", "Uwaga", MessageBoxButton.YesNo);
                                if (mb == MessageBoxResult.Yes)
                                {
                                    lista_model.Modify(biezacyPilkarz, Index);
                                    onPropertyChanged(nameof(Lista_lb));
                                }
                            }
                        },
                        arg => Index != -1 && (!string.IsNullOrEmpty(WEPImie)) && (!string.IsNullOrEmpty(WEPNazwisko)) && (WEPImie != name) && (WEPNazwisko != surname) && WEPImie.All(Char.IsLetter) && WEPNazwisko.All(Char.IsLetter)
                     );
                return modify;
            }
        }

        public ICommand Save
        {
            get
            {
                if (save == null)
                    save = new RelayCommand(arg => lista_model.Save(@"data.json"), arg => true);

                return save;
            }
        }

        public ICommand Load
        {
            get
            {
                if (load == null)
                    load = new RelayCommand(
                        arg =>
                        {
                            Pilkarz p = lista_model.GetPlayers[Index];
                            WEPImie = p.Imie; WEPNazwisko = p.Nazwisko;
                            sliderWiek = p.Wiek; sliderWaga = p.Waga;
                            onPropertyChanged(nameof(WEPImie), nameof(WEPNazwisko),
                                nameof(sliderWiek), nameof(sliderWaga));
                        },
                        arg => Index > -1
                    );

                return load;
            }
        }

        public ICommand Clear 
        {
            get
            {
                if (clear == null)
                    clear = new RelayCommand(
                        arg => {
                            WEPImie = null; WEPNazwisko = null; sliderWiek = 15; sliderWaga = 50;
                            onPropertyChanged(nameof(WEPImie), nameof(WEPNazwisko),
                                    nameof(sliderWiek), nameof(sliderWaga));
                        }, 
                        
                        arg => true);

                return clear;
            }
        }
        #endregion

    }
}
