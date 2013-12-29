using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ProjectBussinessApplications.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using BusinessApplicationsProject.View;
using System.ComponentModel.DataAnnotations;
using BusinessApplicationsProject.Model;
using Microsoft.Win32;
using System.IO;
using System.Windows;


namespace BusinessApplicationsProject.ViewModel
{
    class BandsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Bands"; }
        }
        public BandsVM()
        {
            Fill();
        }

        private void Fill()
        {
            Bands = Band.GetBands();
            Genres = Genre.GetGenres();
            ButtonEnabled = true;
            AddBandButton = true;
            BandButton = false;
            ListEnabled = true;
        }
        private ObservableCollection<BandsToGenre> _bandsToGenre;

        public ObservableCollection<BandsToGenre> ListBandsToGenre
        {
            get { return _bandsToGenre; }
            set { _bandsToGenre = value; OnPropertyChanged("ListBandsToGenres"); }
        }

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; OnPropertyChanged("Bands"); }
        }
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set { _genres = value; OnPropertyChanged("Genres"); }
        }
        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set { _selectedBand = value; OnPropertyChanged("SelectedBand"); }
        }
        private Genre _selectedGenre;

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set { _selectedGenre = value; OnPropertyChanged("SelectedGenre"); }
        }
        private Genre _selectedBandGenre;

        public Genre SelectedBandGenre
        {
            get { return _selectedBandGenre; }
            set { _selectedBandGenre = value; }
        }
        private bool _bandButton;

        public bool BandButton
        {
            get { return _bandButton; }
            set { _bandButton = value; OnPropertyChanged("BandButton"); }
        }
        private bool _addBandButton;

        public bool AddBandButton
        {
            get { return _addBandButton; }
            set { _addBandButton = value; OnPropertyChanged("AddBandButton"); }
        }
        private bool _listEnabled;

        public bool ListEnabled
        {
            get { return _listEnabled; }
            set { _listEnabled = value; OnPropertyChanged("ListEnabled"); }
        }

        

        
        public ICommand OpenImageCommand
        { get { return new RelayCommand(OpenImage, () => { return SelectedBand != null; }); } }

        private void OpenImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (ofd.ShowDialog() == true)
            {
                FileStream fs = File.OpenRead(ofd.FileName);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                SelectedBand.Picture = bytes;
                OnPropertyChanged("SelectedBand");
            }
        }
        public ICommand AnnuleerBandCommand
        { get { return new RelayCommand(AnnuleerBand); } }

        private void AnnuleerBand()
        {
            Bands.Remove(SelectedBand);
            BandButton = false;
            AddBandButton = true;
            ButtonEnabled = true;
            ListEnabled = true;
        }
        public ICommand AddNewBandCommand
        {
            get { return new RelayCommand(AddNewBand); }
        }

        private void AddNewBand()
        {
            Band nieuw = new Band();
            nieuw.Name="(leeg)";
            nieuw.Id = Band.ZoekId();
            nieuw.Genres = new ObservableCollection<Genre>();
            SelectedBand = nieuw;
            Bands.Add(SelectedBand);
            BandButton = true;
            AddBandButton = false;
            ButtonEnabled = false;
            ListEnabled = false;

            
            
        }
        public ICommand RemoveCommand
        {
            get { return new RelayCommand(Remove, () => { return SelectedBand != null; }); }
        
        }

        private void Remove()
        {
            string n = SelectedBand.Name;
            Band.RemoveGenres(SelectedBand);
            Band.DeleteBand(SelectedBand);
            Bands.Remove(SelectedBand);
            MessageBox.Show("De band '"+ n + "' is succesvol verwijdert.");
        }

        

       
        private bool _buttonEnabled;

        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set { _buttonEnabled = value; OnPropertyChanged("ButtonEnabled"); }
        }
        public ICommand BandBewerkenCommand
        {
            get { return new RelayCommand(BandBewerken, () => { return SelectedBand != null; }); }
        }

        private void BandBewerken()
        {
            byte[] NoIm = ZoekPic();
            string n = SelectedBand.Name;
            Band.UpdateBand(SelectedBand, NoIm);
            Band.RemoveGenres(SelectedBand);
            Band.AddGenres(SelectedBand);
            MessageBox.Show("De band "+ n+" is succesvol bijgewerkt.");
        }
        public ICommand AddBandCommand
        {

            get
            {
                return new RelayCommand(AddBand);
            }
        }

        private void AddBand()
        {
            byte[] NoIm=ZoekPic();
            Band.VoegBandToe(SelectedBand, NoIm);
            Band.RemoveGenres(SelectedBand);
            Band.AddGenres(SelectedBand);
            BandButton = false;
            AddBandButton = true;
            ButtonEnabled = true;
            ListEnabled = true;



        }
        public ICommand ToevoegenCommand
        { get { return new RelayCommand(Toevoegen, () => (SelectedGenre != null && SelectedBand != null)); } }
        
    
        private void Toevoegen()
        {


            if (Check()) SelectedBand.Genres.Add(SelectedGenre);
             
            
            
        }

        private bool Check()
        {
            if (SelectedBand.Genres == null)
            {

            }
            else
            {
                foreach (Genre g in SelectedBand.Genres)
                {
                    if (SelectedGenre.Name == g.Name)
                    {
                        return false;

                    }

                }
            }
            
            return true;
        }
        public ICommand VerwijderenCommand
        {  get { return new RelayCommand(Verwijderen, () => { return SelectedBandGenre != null; }); } }

        private void Verwijderen()
        {
            SelectedBand.Genres.Remove(SelectedBandGenre);

        }

        private static byte[] ZoekPic()
        {
            FileStream fs = File.OpenRead("NoImage.jpg");
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
            return bytes;
        }
        /*
         *  public ICommand AnnuleerCommand
        {
            get { return new RelayCommand(Annuleer); }
        }

        private void Annuleer()
        {
            ListEnabled = true;
            ButtonEnabled = true;
            ContactAdd = false;
            Contacten.Remove(SelectedCont);
        }
         * */




    }
}
