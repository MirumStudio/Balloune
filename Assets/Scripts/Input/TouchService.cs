using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if UNITY_IOS
public class TouchService : iOSTouchService
#else
public class TouchService : DefaultTouchService
#endif
{
}
