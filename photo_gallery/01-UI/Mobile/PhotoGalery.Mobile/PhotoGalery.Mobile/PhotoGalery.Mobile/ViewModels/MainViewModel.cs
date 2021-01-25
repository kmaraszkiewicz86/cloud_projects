using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using Xamarin.Forms;

namespace PhotoGalery.Mobile.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<PhotoGalleryResponse> PhotoGalleryResponses
        {
            get => _photoGalleryResponses;
            set
            {
                _photoGalleryResponses = value;
                OnPropertyChanged();
            }
        }

        public Command OnAppearingCommand { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PhotoGalleryResponse> _photoGalleryResponses;

        private bool _isLoading;

        private readonly IPhotoGaleryHttpService _photoGaleryHttpService;

        public MainViewModel(IPhotoGaleryHttpService photoGaleryHttpService)
        {
            _photoGaleryHttpService = photoGaleryHttpService;

            PhotoGalleryResponses = new ObservableCollection<PhotoGalleryResponse>();
            OnAppearingCommand = new Command(OnAppearingCommandAction);
        }

        private void OnAppearingCommandAction()
        {
            IsLoading = true;

            Task.Run(async () =>
            {
                IEnumerable<PhotoGalleryResponse> photoGalleryResponsesFromServer =
                    await _photoGaleryHttpService.GetAllAsync();

                if (photoGalleryResponsesFromServer == null || photoGalleryResponsesFromServer.Count() == 0)
                {
                    return;
                }

                Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    PhotoGalleryResponses = new ObservableCollection<PhotoGalleryResponse>(
                        photoGalleryResponsesFromServer.ToList());

                    IsLoading = false;
                });
            });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}