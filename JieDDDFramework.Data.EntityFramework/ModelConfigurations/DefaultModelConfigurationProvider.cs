using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations.Services;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations
{
    public class DefaultModelConfigurationProvider : IModelConfigurationProvider
    {
        private readonly IAutoApplyConfigurationService _autoApplyConfigurationService;
        private readonly IFixModelConfigurationService _fixModelConfigurationService;
        private readonly IGlobalFilterService _globalFilterService;

        public DefaultModelConfigurationProvider(IAutoApplyConfigurationService autoApplyConfigurationService, IFixModelConfigurationService fixModelConfigurationService, IGlobalFilterService globalFilterService)
        {
            _autoApplyConfigurationService = autoApplyConfigurationService;
            _fixModelConfigurationService = fixModelConfigurationService;
            _globalFilterService = globalFilterService;
        }
        public IAutoApplyConfigurationService GetApplyConfigurationService()
        {
            return _autoApplyConfigurationService;
        }

        public IFixModelConfigurationService GetFixModelConfigurationService()
        {
            return _fixModelConfigurationService;
        }

        public IGlobalFilterService GetGlobalFilterService()
        {
            return _globalFilterService;
        }
    }
}
