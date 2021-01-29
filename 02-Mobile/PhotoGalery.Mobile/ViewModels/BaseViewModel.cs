using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PhotoGallery.Shared.Exceptions;

namespace PhotoGalery.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ReponseErrorExists));
            }
        }

        public bool ReponseErrorExists => string.IsNullOrEmpty(ErrorMessage);

        private string _errorMessage;

        protected async Task TryCatchWorkAsync(Func<Task> onActionWorkAsync)
        {
            try
            {
                await onActionWorkAsync();
            }
            catch (InvalidResponseException invalidResponseException)
            {
                if (invalidResponseException.ModelStateErrors != null
                    && invalidResponseException.ModelStateErrors.Count > 0)
                {
                    string[] modelErrors = invalidResponseException.ModelStateErrors.Select(error =>
                         $"{error.Key}: {(string.Join(";", error.Value))}").ToArray();

                    ErrorMessage = string.Join(";", modelErrors);
                }

                ErrorMessage = invalidResponseException.Message;
            }
            catch (Exception exception)
            {
                ErrorMessage = exception.Message;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}