using System;

namespace SearchWebApplication
{
    public interface IHostConfigurationProvider
    {
        Uri GetHostUrl();
    }
}