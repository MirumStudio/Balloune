/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using System;

namespace Radix.Logging
{
    public enum ELogCategory
    {
        [LogCategoryAttribute(false)]
        NONE,

        [LogCategoryAttribute(false, "yellow")]
        RADIX,

        [LogCategoryAttribute(false)]
        GAMEPLAY,

        [LogCategoryAttribute(false)]
        INPUT,

        [LogCategoryAttribute(true, "green")]
        CHARACTER_STATE,
    }

    internal class LogCategoryAttribute : Attribute
    {
        internal LogCategoryAttribute(bool pActive)
        {
            Active = pActive;
            Color = "black";
        }
        
        internal LogCategoryAttribute(bool pActive, string pColor)
        {
            Active = pActive;
            Color = pColor;
        }
        internal bool Active { get; private set; }
        internal string Color { get; private set; }
    }
}
