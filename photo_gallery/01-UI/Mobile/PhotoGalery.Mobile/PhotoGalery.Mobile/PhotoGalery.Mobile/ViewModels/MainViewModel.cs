using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using Xamarin.Forms;

namespace PhotoGalery.Mobile.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PhotoGalleryResponse> PhotoGalleryResponses { get; set; }

        public Command OnAppearingCommand { get; set; }

        public string TestMessage
        {
            get => _testMessage;
            set
            {
                _testMessage = value;
                OnPropertyChanged();
            }
        }

        private string _testMessage;

        private readonly IPhotoGaleryHttpService _photoGaleryHttpService;

        public MainViewModel(IPhotoGaleryHttpService photoGaleryHttpService)
        {
            _photoGaleryHttpService = photoGaleryHttpService;

            PhotoGalleryResponses = new ObservableCollection<PhotoGalleryResponse>();
            OnAppearingCommand = new Command(OnAppearingCommandAction);
        }

        private void OnAppearingCommandAction()
        {
            TestMessage = "On loading test...";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}