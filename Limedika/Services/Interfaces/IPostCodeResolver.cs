using System.Threading.Tasks;

namespace Limedika.Services.Interfaces
{
    public interface IPostCodeResolver
    {
        Task<string> GetPostCode(string address);
    }
}