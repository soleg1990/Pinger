using System.Threading;

namespace Pinger.BL
{
    public interface IBackgroundWork
    {
        void Work(CancellationToken token);
    }
}