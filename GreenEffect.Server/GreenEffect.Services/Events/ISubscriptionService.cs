using System.Collections.Generic;

namespace GreenEffect.Services.Events
{
    public interface ISubscriptionService
    {
        IList<IConsumer<T>> GetSubscriptions<T>();
    }
}
