/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using Radix.Event;
namespace Radix.InternalEvent
{
    internal enum ETestEvent
    {
        [EventHandlerAttribute(typeof(VoidDelegate))]
        ENGINE_STARTED,
    }
}
