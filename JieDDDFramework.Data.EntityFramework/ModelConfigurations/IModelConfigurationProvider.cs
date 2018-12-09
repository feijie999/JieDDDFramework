using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Data.EntityFramework.ModelConfigurations.Services;

namespace JieDDDFramework.Data.EntityFramework.ModelConfigurations
{
    public interface IModelConfigurationProvider
    {
        IGlobalFilterService GetGlobalFilterService();
        IFixModelConfigurationService GetFixModelConfigurationService();
        IAutoApplyConfigurationService GetApplyConfigurationService();
    }
}
