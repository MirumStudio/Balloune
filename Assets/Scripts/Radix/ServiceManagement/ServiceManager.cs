using Radix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radix.Service
{
    public class ServiceManager
    {
        #region Singleton
        private static volatile ServiceManager instance;
        private static object syncRoot = new Object();

        private ServiceManager() { }

        public static ServiceManager Instance
        {
            get
            {
                if (instance == null) //check if singleton exist
                {
                    lock (syncRoot) //Double check if the singleton exist (multithreading bug)
                    {
                        if (instance == null)
                            instance = new ServiceManager();
                    }
                }

                return instance;
            }
        }
        #endregion

        private List<ServiceBase> m_serviceList;

        public void Init()
        {
            m_serviceList = new List<ServiceBase>();

            List<Type> serviceType = GetAllServiceType();

            foreach(Type type in serviceType)
            {
                CreateService(type);
            }

            foreach (ServiceBase service in m_serviceList)
            {
                service.CallInit();
            }
        }

        internal void Dispose()
        {
            CheckServiceListValidity();

            foreach (ServiceBase service in m_serviceList)
            {
                StopService(service);
                service.CallDispose();
            }

            m_serviceList.Clear();
            m_serviceList = null;
        }

        internal void StartAllServices()
        {
            CheckServiceListValidity();

            foreach (ServiceBase service in m_serviceList)
            {
                StartService(service);
            }
        }

        internal void StopAllServices()
        {
            CheckServiceListValidity();

            foreach (ServiceBase service in m_serviceList)
            {
                StopService(service);
            }
        }

        internal void StartService<T>() where T : ServiceBase
        {
            StartService(GetService<T>());
        }

        internal void StopService<T>() where T : ServiceBase
        {
            StopService(GetService<T>());
        }

        public T GetService<T>() where T : ServiceBase
        {
            T serviceBase = m_serviceList.FirstOrDefault((service) => service.GetType() == typeof(T)) as T;

            if (serviceBase != null)
            {
                return serviceBase;
            }
            else
            {
                throw new NullReferenceException("Cannot find an instance of " + typeof(T).Name);
            }
        }

        private void StartService(ServiceBase _service)
        {
            _service.CallStart();
        }

        private void StopService(ServiceBase _service)
        {
            _service.CallStop();
        }

        private void CreateService(Type _serviceType)
        {
            //To think what re the condition to create service
           /* List<EServiceType> types = new List<EServiceType>();
            types.Add(EServiceType.DEBUG);

            foreach (ServiceType atrr in _serviceType.GetCustomAttributes(typeof(ServiceType), true))
            {
                if(atrr.Contains(types))
                {

                }
            }*/

            ServiceBase service = Activator.CreateInstance(_serviceType) as ServiceBase;
            m_serviceList.Add(service);
        }

        private List<Type> GetAllServiceType()
        {
            return TypeUtility.GetAllTypeFromNamespace(typeof(ServiceBase), "Radix");
        }

        private void CheckServiceListValidity()
        {
            if(m_serviceList == null)
            {
                throw new NullReferenceException("The Service Manager contains no service's instance.");
            }
        }
    }
}
