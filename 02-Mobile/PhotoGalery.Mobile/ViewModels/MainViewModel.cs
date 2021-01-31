using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using Xamarin.Forms;

namespace PhotoGalery.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
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

        public Command OnAddedNewItemCommand { get; set; }

        public Command OnDeleteItemCommand { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string NewItemName
        {
            get => _newItemName;
            set
            {
                _newItemName = value;

                OnAddedNewItemCommand?.ChangeCanExecute();

                OnPropertyChanged();
            }
        }

        private ObservableCollection<PhotoGalleryResponse> _photoGalleryResponses;

        private bool _isLoading;

        private string _newItemName;

        private readonly IPhotoGaleryHttpService _photoGaleryHttpService;

        public MainViewModel(IPhotoGaleryHttpService photoGaleryHttpService)
        {
            _photoGaleryHttpService = photoGaleryHttpService;

            PhotoGalleryResponses = new ObservableCollection<PhotoGalleryResponse>();

            OnAppearingCommand = new Command(OnAppearingCommandAction);
            OnAddedNewItemCommand = new Command(OnAddedNewItemAction,
                canExecute: () => !string.IsNullOrEmpty(NewItemName));
            OnDeleteItemCommand = new Command<string>(OnDeleteItemCommandAction);
        }

        private void OnAppearingCommandAction()
        {
            ErrorMessage = string.Empty;
            IsLoading = true;

            Task.Run(async () =>
            {
                await FetchItemsFromServerAsync();
            });
        }

        private void OnAddedNewItemAction()
        {
            IsLoading = true;

            Task.Run(async () =>
            {
                await TryCatchWorkAsync(async () =>
                {
                    await _photoGaleryHttpService.InsertAsync(new InsertPhotoGralleryRequest
                    {
                        Name = NewItemName
                    });
                });

                Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                    NewItemName = string.Empty;
                });

                await FetchItemsFromServerAsync();
            });
        }

        private void OnDeleteItemCommandAction(string id)
        {
            IsLoading = true;

            Task.Run(async () =>
            {
                await TryCatchWorkAsync(async () =>
                {
                    await _photoGaleryHttpService.DeleteAsync(new DeleteRequest
                    {
                        Id = id
                    });
                });

                Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });

                await FetchItemsFromServerAsync();
            });
        }

        private async Task FetchItemsFromServerAsync()
        {
            IEnumerable<PhotoGalleryResponse> photoGalleryResponsesFromServer = null;

            await TryCatchWorkAsync(async () =>
            {
                photoGalleryResponsesFromServer = await _photoGaleryHttpService.GetAllAsync();
            });

            Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                IsLoading = false;
            });

            if (photoGalleryResponsesFromServer == null || photoGalleryResponsesFromServer.Count() == 0)
            {
                return;
            }

            Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                PhotoGalleryResponses = new ObservableCollection<PhotoGalleryResponse>(
                    photoGalleryResponsesFromServer.ToList());
            });
        }
    }
}