/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using System.IO;

namespace Radix.Json
{
    public class JsonUtility
    {
        public static void SaveToFile<T, O>(O pObject, string pPath) where T:JsonParser<O>, new()
        {
            var parser = new T();
            string json = parser.Parse(pObject);

            //save (to put in a service or somewhere else
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WP8_1 && !UNITY_ANDROID
            DeleteFileIfExist(pPath);

            StreamWriter writer = new StreamWriter(pPath);
            writer.WriteLine(json);
            writer.Close();
#endif
        }

        private static void DeleteFileIfExist(string aPath)
        {
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WP8_1 && !UNITY_ANDROID
            if (File.Exists(aPath))
            {
                File.Delete(aPath);
            }
#endif
        }
    }
}
