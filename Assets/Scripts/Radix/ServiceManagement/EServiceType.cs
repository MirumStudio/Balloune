using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.Service
{
    public enum EServiceType
    {
        STANDARD,
        DEBUG,
        RELEASE,
        PRODUCTION,
        NULL,
        IN_DEVELLOPEMENT
    }

    public class ServiceType : Attribute
    {
        private List<EServiceType> mServiceTypes;

        public ServiceType(params EServiceType[] pType)
        {
            mServiceTypes = pType.ToList();
        }

        public bool Contains(List<EServiceType> pType)
        {
            bool containType = true;

            foreach(EServiceType type in pType)
            {
                if(!mServiceTypes.Contains(type))
                {
                    containType = false;
                }
            }

            return containType;
        }
    }
}
