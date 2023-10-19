using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ModelsLibrary
{
    public interface IStoreRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();

        public Task<T> Get(int? id);

        public Task Create(T t);

        public Task Update(T t);

        public Task Delete(int i);


        public Task<string> RetrieveImage(T t);

        public Task UploadImage(T t, IFormFile file);


        public Task<bool> ValidateFile(IFormFile file);


        public Task<Dictionary<int, string>> GetImages(List<T> t);

        public HttpClient CreateClient();

    }
}
