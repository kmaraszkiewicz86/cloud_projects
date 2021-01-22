using System;
using System.Collections.Generic;

namespace PhotoGallery.Shared.Exceptions
{
    public class InvalidResponseException : Exception
    {
        public Dictionary<string, string[]> ModelStateErrors { get; private set; }
        
        public InvalidResponseException(string message) : base(message)
        {
            
        }

        public InvalidResponseException(Dictionary<string, string[]> modelStateErors)
        {
            ModelStateErrors = modelStateErors;
        }
    }
}