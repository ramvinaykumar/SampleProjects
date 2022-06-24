using DI.Transient.Scoped.Singleton.DILifeCycle.Interface;
using System;

namespace DI.Transient.Scoped.Singleton.DILifeCycle.Services
{
    public class DIOperationService : ITransientService, IScopedService, ISingletonService
    {
        Guid id;
        public DIOperationService()
        {
            id = Guid.NewGuid();
        }
        public Guid GetOperationID()
        {
            return id;
        }
    }
}
