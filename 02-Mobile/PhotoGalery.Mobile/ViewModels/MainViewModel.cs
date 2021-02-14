using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhotoGalery.Http.Shared.Core.Interfaces;
using PhotoGalery.Mobile.Services.Interfaces;
using PhotoGallery.Shared.ApiModels.Api.PhotoAwsGallery;
using PhotoGallery.Shared.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoGalery.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ImageSource PhotoImageSource
        {
            get => _photoImageSource;
            set
            {
                _photoImageSource = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PhotoGalleryResponse> PhotoGalleryResponses
        {
            get => _photoGalleryResponses;
            set
            {
                _photoGalleryResponses = value;
                OnPropertyChanged();
            }
        }

        public ICommand OnAppearingCommand { get; set; }

        public ICommand OnAddedNewItemCommand { get; set; }

        public ICommand OnDeleteItemCommand { get; set; }

        public ICommand TakePhotoCommand { get; set; }

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

                ((Command)OnAddedNewItemCommand)?.ChangeCanExecute();

                OnPropertyChanged();
            }
        }

        private ImageSource _photoImageSource;

        private ObservableCollection<PhotoGalleryResponse> _photoGalleryResponses;

        private bool _isLoading;

        private string _newItemName;

        private readonly IPhotoGaleryHttpService _photoGaleryHttpService;

        private readonly ICameraService _cameraService;

        private readonly IUploadPhotoHttpService _uploadPhotoHttpService;

        public MainViewModel(IPhotoGaleryHttpService photoGaleryHttpService,
            ICameraService cameraService,
            IUploadPhotoHttpService uploadPhotoHttpService)
        {
            PhotoGalleryResponses = new ObservableCollection<PhotoGalleryResponse>();

            OnAppearingCommand = new Command(async () => await OnAppearingCommandActionAsync());
            OnAddedNewItemCommand = new Command(OnAddedNewItemAction,
                canExecute: () => !string.IsNullOrEmpty(NewItemName));
            OnDeleteItemCommand = new Command<string>(OnDeleteItemCommandAction);
            TakePhotoCommand = new Command<string>(TakePhotoCommandAction);

            _photoGaleryHttpService = photoGaleryHttpService;
            _cameraService = cameraService;
            _uploadPhotoHttpService = uploadPhotoHttpService;
        }

        private async Task OnAppearingCommandActionAsync()
        {
            await RequestRequiredPermissionsAsync();

            ErrorMessage = string.Empty;
            IsLoading = true;

            await FetchItemsFromServerAsync();
        }

        private async Task RequestRequiredPermissionsAsync()
        {
            PermissionStatus status = await Permissions.RequestAsync<Permissions.Camera>();

            CheckIfUserGrandedPermission(status);

            status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            CheckIfUserGrandedPermission(status);
        }

        private void CheckIfUserGrandedPermission(PermissionStatus status)
        {
            if (!status.HasFlag(PermissionStatus.Granted))
            {
                Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    ErrorMessage = $"The permission is not granded!";
                });

                return;
            }
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

        private void TakePhotoCommandAction(string id)
        {
            IsLoading = true;

            Task.Run(async () =>
            {
                await TryCatchWorkAsync(async () =>
                {
                    PhotoFromCameraModel photoFromCameraModel =
                        await _cameraService.TakePhotoAndGetBytes();

                    if (!photoFromCameraModel.IsValid)
                        throw new Exception(photoFromCameraModel.ErrorMessage);

                    UploadedPhotoResponse uploadedPhotoResponse
                        = await _uploadPhotoHttpService.UploadAsync(id, photoFromCameraModel.PhotoInBytes);

                    ImageSource photoImageSource
                        = ImageSource.FromStream(() => new MemoryStream(uploadedPhotoResponse.PhotoBytes));

                    Application.Current.MainPage.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        PhotoImageSource = photoImageSource;

                        IsLoading = false;
                    });
                });

            });
        }
    }
}