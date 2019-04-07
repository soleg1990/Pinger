using System.Threading;
using System.Threading.Tasks;

namespace Pinger.BL
{
    public interface IBackgroundWork
    {
        Task StartAsync(CancellationToken token);
    }
}