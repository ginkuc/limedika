using System.Threading.Tasks;
using Limedika.Services.Interfaces;

namespace Limedika.Services
{
    public class PostItPostCodeResolver : IPostCodeResolver
    {
        public Task<string> GetPostCode(string address)
        {
            return Task.FromResult(string.Empty);
        }
    }
}