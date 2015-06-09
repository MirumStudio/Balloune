/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */


namespace Radix.ErrorMangement
{
#if UNITY_EDITOR
    public class Assert
    {
        private Assert() { }

        public static bool Check(bool pCondititon)
        {
            return Check(pCondititon, "Normal Assert Fail");
        }

        public static bool Check(bool pCondititon, string pMessage)
        {
            if (UnityEngine.Debug.isDebugBuild && !string.IsNullOrEmpty(pMessage))
            {
                if (!pCondititon)
                {
                    AssertFail(pMessage);
                }
            }
            return pCondititon;
        }

        public static bool CheckNull(object pObject)
        {
            return CheckNull(pObject, "Null Assert Fail");
        }

        public static bool CheckNull(object pObject, string pMessage)
        {
            return Check(pObject != null, pMessage);
        }

        public static bool CheckNullAndEmpty(string pObject)
        {
            return CheckNullAndEmpty(pObject, "Null or Empty Assert Fail");
        }

        public static bool CheckNullAndEmpty(string pObject, string pMessage)
        {
            return Check(!string.IsNullOrEmpty(pObject), pMessage);
        }

        private static void AssertFail(string pMessage)
        {
            Error.Create(pMessage, EErrorSeverity.ASSERT);
        }
    }
#else
    public class Assert
    {
        private Assert() { }
        public static bool Check(bool pCondititon){ return true; }
        public static bool Check(bool pCondititon, string pMessage) { return true; }
        public static bool CheckNull(object pObject) { return true; }
        public static bool CheckNull(object pObject, string pMessage) { return true; }
        public static bool CheckNullAndEmpty(string pObject) { return true; }
        public static bool CheckNullAndEmpty(string pObject, string pMessage) { return true; }
        private static void AssertFail(string pMessage) {}
    }
#endif
}
