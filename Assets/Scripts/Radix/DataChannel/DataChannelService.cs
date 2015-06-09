/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Service;
using System.Collections.Generic;

namespace Radix.DataChannel
{
    public class DataChannelService : ServiceBase
    {
        public static DataChannelService Instance
        {
            get
            {
                return ServiceManager.Instance.GetService<DataChannelService>();
            }
        }

        public static string Param { get; set; }
        private List<DataChannel> mDataChannelList; 

        protected override void Init()
        {
            mDataChannelList = new List<DataChannel>();

            //List<Type> DataChannelType = GetAllDataChannelType();
            //DataChannelType.ForEach(CreateService);
        }

        protected override void Dispose()
        {
            
        }

        public void AddChannel(DataChannel pChannel)
        {
            mDataChannelList.Add(pChannel);
        }

        static public DataChannel Get<T>() where T : DataChannel
        {
            return null;
        }

       /* private List<Type> GetAllDataChannelType()
        {
            return TypeUtility.GetAllTypeFromNamespace(typeof(DataChannel), "Radix");
        }*/
    }
}
