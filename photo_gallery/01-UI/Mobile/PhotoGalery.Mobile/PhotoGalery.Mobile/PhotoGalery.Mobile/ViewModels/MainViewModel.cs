using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PhotoGalery.Http.Shared.Core.Implementations;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;

namespace PhotoGalery.Mobile.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PhotoGalleryResponse> PhotoGalleryResponses { get; set; }

        private readonly PhotoGaleryHttpService _photoGaleryHttpService;

        public MainViewModel(PhotoGaleryHttpService photoGaleryHttpService)
        {
            _photoGaleryHttpService = photoGaleryHttpService;

            PhotoGalleryResponses = new ObservableCollection<PhotoGalleryResponse>();
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
