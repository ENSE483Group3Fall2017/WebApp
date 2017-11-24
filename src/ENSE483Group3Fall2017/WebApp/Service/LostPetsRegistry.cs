using System.Threading.Tasks;
using WebApp.DAL;
using FireSharp.Config;
using FireSharp;
using System;

namespace WebApp.Service
{
    public interface ILostPetsRegistry 
    {
        Task Add(DAL.Pet pet);

        Task Remove(DAL.Pet pet);
    }

    public class LostPetsRegistry : ILostPetsRegistry, IDisposable
    {
        public const string RegistryRoot = "lostPets";

        private readonly FirebaseClient _client;

        public LostPetsRegistry(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                throw new ArgumentException(basePath);

            _client = new FirebaseClient(new FirebaseConfig { BasePath = basePath });
        } 

        public Task Add(Pet pet) =>
            _client.PushAsync(GetRegistryPath(pet), pet);


        public Task Remove(Pet pet) =>
            _client.DeleteAsync(GetRegistryPath(pet));

        private static string GetRegistryPath(Pet pet) =>
            $"{RegistryRoot}/{pet.BeaconID}";

        public void Dispose() =>
            _client?.Dispose();
    }
}