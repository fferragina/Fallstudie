#region

using System.Configuration;
using System.Runtime.CompilerServices;
using HsrOrderApp.BL.Core.Helpers;
using HsrOrderApp.DAL.Data.Repositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

#endregion

namespace HsrOrderApp.BL.BusinessComponents.DependencyInjection
{
    public class DependencyInjectionHelper
    {
        private static IUnityContainer _unityContainer;

        public static IUnityConfigProvider ConfigProvider { get; set; }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void SetUnityContainer()
        {
            if (_unityContainer == null)
            {
                _unityContainer = new UnityContainer();

                if (ConfigProvider == null)
                {
                    ConfigProvider = new AppUnityConfigProvider();
                }

                var config = (UnityConfigurationSection)ConfigProvider.UnityConfiguration.GetSection("unity");
                config.Containers[ConfigurationHelper.OrMapper.ToString()].Configure(_unityContainer);
            }
        }

        internal class AppUnityConfigProvider : IUnityConfigProvider
        {
            public Configuration UnityConfiguration
            {
                get
                {
                    ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                    map.ExeConfigFilename = "unity.config";
                    return ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                }
            }
        }

        public static OrderBusinessComponent GetOrderBusinessComponent()
        {
            SetUnityContainer();
            OrderBusinessComponent orderBc = _unityContainer.Resolve<OrderBusinessComponent>();
            return orderBc;
        }

        public static CustomerBusinessComponent GetCustomerBusinessComponent()
        {
            SetUnityContainer();
            CustomerBusinessComponent customerBc = _unityContainer.Resolve<CustomerBusinessComponent>();
            return customerBc;
        }
        
        public static ProductBusinessComponent GetProductBusinessComponent()
        {
            SetUnityContainer();
            ProductBusinessComponent productBc = _unityContainer.Resolve<ProductBusinessComponent>();
            return productBc;
        }

        public static SupplierBusinessComponent GetSupplierBusinessComponent()
        {
            SetUnityContainer();
            SupplierBusinessComponent supplierBc = _unityContainer.Resolve<SupplierBusinessComponent>();
            return supplierBc;
        }

        public static SecurityBusinessComponent GetSecurityBusinessComponent()
        {
            SetUnityContainer();
            SecurityBusinessComponent securityBc = _unityContainer.Resolve<SecurityBusinessComponent>();
            return securityBc;
        }
    }

    public interface IUnityConfigProvider
    {
        Configuration UnityConfiguration { get; }
    }

    
}

