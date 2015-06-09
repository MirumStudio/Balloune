/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.DatabaseManagement.Sqlite;
using Radix.ErrorMangement;
using Radix.Event;
using Radix.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Radix.Service
{
    public class ServiceManager
    {
        private bool mIsInit = false;

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

        #region ServiceRegister
        private static List<Type> mServiceTypes = new List<Type>();

        public static void RegisterService(Type pServiceType)
        {
			Assert.Check(!mServiceTypes.Contains(pServiceType), pServiceType + " is already registered");
			mServiceTypes.Add(pServiceType);
        }

        private void RegisterRadixService()
        {
            mServiceTypes.Add(typeof(EventService));
            mServiceTypes.Add(typeof(SqliteService));
            mServiceTypes.Add(typeof(LogService));
        }
        #endregion

        private List<ServiceBase> mServiceList;

        public void Init()
        {
            if (!mIsInit)
            {
                RegisterRadixService();
                mServiceList = new List<ServiceBase>();
                CreateAllServices();
                InitAllServices();
                mIsInit = true;

                Log.Create("ServiceManager initialized", ELogCategory.RADIX);
            }
        }

        internal void Dispose()
        {
            CheckServiceListValidity();

            foreach (ServiceBase service in mServiceList)
            {
                StopService(service);
                service.CallDispose();
            }

            mServiceList.Clear();
            mServiceList = null;

            Log.Create("ServiceManager disposed", ELogCategory.RADIX);
        }

		internal void CreateAllServices()
		{
			CheckServiceListValidity();
			
			foreach (Type type in mServiceTypes)
            {
                CreateService(type);
            }
		}
		
		internal void InitAllServices()
		{
			CheckServiceListValidity();

            foreach (ServiceBase service in mServiceList)
            {
                service.CallInit();
            }
		}
		
        internal void StartAllServices()
        {
            CheckServiceListValidity();

            foreach (ServiceBase service in mServiceList)
            {
                StartService(service);
            }
        }

        internal void StopAllServices()
        {
            CheckServiceListValidity();

            foreach (ServiceBase service in mServiceList)
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
            Assert.CheckNull(mServiceList);

            T serviceBase = mServiceList.FirstOrDefault((service) => service.GetType() == typeof(T)) as T;

            Assert.CheckNull(serviceBase, "Cannot find an instance of " + typeof(T).Name);

            return serviceBase;
        }

        private void StartService(ServiceBase pService)
        {
            pService.CallStart();
        }

        private void StopService(ServiceBase pService)
        {
            pService.CallStop();
        }

        private void CreateService(Type pServiceType)
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

            ServiceBase service = Activator.CreateInstance(pServiceType) as ServiceBase;
            mServiceList.Add(service);
        }

        private void CheckServiceListValidity()
        {
            if (mServiceList == null)
            {
                throw new NullReferenceException("The Service Manager contains no service's instance.");
            }
        }
    }
}
