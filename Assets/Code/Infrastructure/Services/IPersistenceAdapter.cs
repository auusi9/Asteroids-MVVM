using System.Threading.Tasks;
using UnityEngine.Events;

namespace Services.Ports
{
    public interface IPersistenceAdapter<T> where T : class
    {
        Task SaveData(T dataToSave, string path);
        Task<T> LoadData(string path);
    }
}