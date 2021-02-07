using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PhotoGallery.Shared.Exceptions;

namespace PhotoGalery.Components
{
    public abstract class ComponentShared : ComponentBase
    {
        protected string ErrorMessage { get; private set; }

        protected Dictionary<string, string[]> ModelStateErrors { get; private set; }

        public ComponentShared()
        {
            ErrorMessage = string.Empty;
            ModelStateErrors = new Dictionary<string, string[]>();
        }

        protected async Task OnTryCatchAsync(Func<Task> funcAsync)
        {
            ErrorMessage = string.Empty;

            try
            {
                await funcAsync();
            }
            catch (InvalidResponseException invalidResponseException)
            {
                if (invalidResponseException.ModelStateErrors != null
                    && invalidResponseException.ModelStateErrors.Count > 0)
                {
                    ModelStateErrors = invalidResponseException.ModelStateErrors;

                    return;
                }

                ErrorMessage = invalidResponseException.Message;
            }
        }
    }
}