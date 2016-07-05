using Recommender.Common.Logger;

namespace Recommender.Core
{
    public interface ILoggable
    {
        Logger Logger { get; set; }
    }
}