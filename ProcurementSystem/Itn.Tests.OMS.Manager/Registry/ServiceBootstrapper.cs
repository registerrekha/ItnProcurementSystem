﻿using StructureMap;

namespace Itn.Tests.OMS.Manager.Registry
{
    public class ServiceBootstrapper
    {
        private static bool _hasStarted;

        public void BootstrapStructureMap(StructureMap.Configuration.DSL.Registry registry)
        {
            ObjectFactory.Initialize(x => x.AddRegistry(registry));
        }

        public static void Restart(StructureMap.Configuration.DSL.Registry registry)
        {
            if (_hasStarted)
            {
                ObjectFactory.ResetDefaults();
            }
            else
            {
                Bootstrap(registry);
                _hasStarted = true;
            }
        }

        public static void Bootstrap(StructureMap.Configuration.DSL.Registry registry)
        {
            new ServiceBootstrapper().BootstrapStructureMap(registry);
        }

        public static void BootstrapFromDefault()
        {
            new ServiceBootstrapper().BootstrapStructureMap(new BaseServicesRegistry());
        }
    }
}