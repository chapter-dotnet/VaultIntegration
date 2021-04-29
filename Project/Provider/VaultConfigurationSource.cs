using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VaultAPI.Model;

namespace VaultAPI.Provider
{

    public class VaultConfigurationSource : IConfigurationSource
    {
        private VaultOptions _config;

        public VaultConfigurationSource(Action<VaultOptions> config)
        {
            _config = new VaultOptions();
            config.Invoke(_config);
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultConfigurationProvider(_config);
        }
    }
}
