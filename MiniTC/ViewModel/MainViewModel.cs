using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using resource = MiniTC.Properties.Resources;

namespace MiniTC.ViewModel
{
    using Model;
    using BaseClass;
    using System;
    using System.IO;

    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Left = new PanelTCModel();
            Right = new PanelTCModel();
            FileManager = new FileManagerTCModel();
            Panel = new PanelTCModel();

            LeftWolumin = Left.ListaDyskow[0];
            RightWolumin = Right.ListaDyskow[0];
        }

        private PanelTC _left;
        private PanelTC _right;
        private PanelTC _panel;
        private FileManagerTC _fileManager;

        public PanelTC Left
        {
            get { return _left; }
            set { _left = value; onPropertyChanged(nameof(Left)); }
        }

        public PanelTC Right
        {
            get { return _right; }
            set { _right = value; onPropertyChanged(nameof(Right)); }
        }

        public PanelTC Panel
        {
            get { return _panel; }
            set { _panel = value; onPropertyChanged(nameof(Panel)); }
        }

        public FileManagerTC FileManager
        {
            get { return _fileManager; }
            set { _fileManager = value; onPropertyChanged(nameof(FileManager)); }
        }

        //Lewy i Prawy Dysk
        public string[] LeftDyski
        {
            get { return Left.ListaDyskow; }
        }

        public string[] RightDyski
        {
            get { return Right.ListaDyskow; }
        }

        // Lewa i Prawa Lista Plików
        public List<string> LeftListaPlikow
        {
            get { return Left.Folder; }
        }

        public List<string> RightListaPlikow
        {
            get { return Right.Folder; }
        }

        //Lewa i Prawa Sciezka
        public string LeftSciezka
        {
            get { return Left.ObecnaSciezka; }
        }

        public string RightSciezka
        {
            get { return Right.ObecnaSciezka; }
        }

        //Lewy i Prawy plik
        private string _leftplik;
        private string _rightplik;
        public string LeftPlik
        {
            set { _leftplik = value; }
        }

        public string RightPlik
        {
            set { _rightplik = value; }
        }

        //Lewy i Prawy Wolumin
        private string _leftwolumin;
        private string _rightwolumin;

        public string LeftWolumin
        {
            get { return _leftwolumin; }
            set
            {
                if (value != null)
                {
                    _leftwolumin = value;
                    Left.Ustaw(_leftwolumin);
                    onPropertyChanged(nameof(LeftWolumin), nameof(LeftListaPlikow), nameof(LeftSciezka));
                }
            }
        }

        public string RightWolumin
        {
            get { return _rightwolumin; }
            set
            {
                if (value != null)
                {
                    _rightwolumin = value;
                    Right.Ustaw(_rightwolumin);
                    onPropertyChanged(nameof(RightWolumin), nameof(RightListaPlikow), nameof(RightSciezka));
                }
            }
        }

        #region Command
        private ICommand _leftpanel = null;
        public ICommand LeftPanel
        {
            get
            {
                if (_leftpanel == null)
                    _leftpanel = new RelayCommand(
                        arg => Panel = Left, 
                        arg => true);
                return _leftpanel;
            }
        }

        private ICommand _rightpanel = null;
        public ICommand RightPanel
        {
            get
            {
                if (_rightpanel == null)
                    _rightpanel = new RelayCommand(
                        arg => Panel = Right, 
                        arg => true);
                return _rightpanel;
            }
        }

        private ICommand _leftplikenter = null;
        public ICommand LeftPlikEnter
        {
            get
            {
                if (_leftplikenter == null)
                    _leftplikenter = new RelayCommand(
                        arg => LeftEnterPlik(),
                        arg => true
                        );
                return _leftplikenter;
            }
        }

        private ICommand _rightplikenter = null;
        public ICommand RightPlikEnter
        {
            get
            {
                if (_rightplikenter == null)
                    _rightplikenter = new RelayCommand(
                        arg => RightEnterPlik(),
                        arg => true
                        );
                return _rightplikenter;
            }
        }

        private ICommand _copy = null;
        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                    _copy = new RelayCommand(
                        arg => CopyFiles(),
                        arg => true);
                return _copy;
            }
        }
        #endregion

        private void RightEnterPlik()
        {
            if (!Right.EnterPlik(_rightplik))
            {
                MessageBox.Show(resource.PerrmissionError,
                                resource.Error,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else
            {
                onPropertyChanged(nameof(RightListaPlikow), nameof(RightSciezka));
            }
        }

        private void LeftEnterPlik()
        {
            if (!Left.EnterPlik(_leftplik))
            {
                MessageBox.Show(resource.PerrmissionError,
                                resource.Error,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else
            {
                onPropertyChanged(nameof(LeftListaPlikow), nameof(LeftSciezka));
            }
        }

        private void CopyFiles()
        {
            if (Panel == Left)
                FileManager.Copy(Left.ObecnaSciezka, _leftplik, Right.ObecnaSciezka);
            else
                FileManager.Copy(Right.ObecnaSciezka, _rightplik, Left.ObecnaSciezka);

            UpdateLB();

        }

        private void UpdateLB()
        {
            if (Panel == Left)
            {
                Right.Ustaw(Right.ObecnaSciezka);
                onPropertyChanged(nameof(RightListaPlikow));
            }
            else
            {
                Left.Ustaw(Left.ObecnaSciezka);
                onPropertyChanged(nameof(LeftListaPlikow));
            }
        }
    }
}
