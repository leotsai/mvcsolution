using System;
using System.IO;

namespace MvcSolution
{
    public class Base64Image
    {
        private readonly string _base64;
        private byte[] _imageBytes;
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
        }

        public Base64Image(string base64)
        {
            _base64 = base64;
        }

        public Base64Image Build()
        {
            var base64 = _base64.Substring(5);//delete "data:"
            _fileName = base64.Substring(0, base64.IndexOf(";base64"));
            base64 = base64.Substring(base64.IndexOf(",") + 1).Replace(" ", "+");
            _imageBytes = Convert.FromBase64String(base64);
            return this;
        }

        public MemoryStream GetStream()
        {
            var ms = new MemoryStream(_imageBytes, 0, _imageBytes.Length);
            ms.Write(_imageBytes, 0, _imageBytes.Length);
            return ms;
        }

    }
}
