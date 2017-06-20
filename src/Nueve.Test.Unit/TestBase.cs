using System;
using Microsoft.Extensions.DependencyInjection;
using Nueve.Common;
using Microsoft.Extensions.Configuration;
using Moq;
using Nueve.Test;

namespace Nueve.Test
{
    public abstract class TestsBase : IDisposable
    {
        private IServiceCollection _serviceCollection;
        public IServiceProvider Services { get; set; }

        protected TestsBase()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.RegisterUnitTestServices();
            Services = _serviceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
        }
    }

}
