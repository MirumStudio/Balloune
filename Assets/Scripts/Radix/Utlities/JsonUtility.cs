using System.IO;
//using System.Runtime.Serialization.Json;

namespace Radix.Utilities
{
    public class JsonUtility
    {
        //JSON is not supported in unity#d plugion have to be added
        /*public static void SaveJsonToFile<T>(T aJsonObject, string aPath)
        {
            DeleteFileIfExist(aPath);

            FileStream fileStream = File.Create(aPath);

            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

           // ser.WriteObject(fileStream, aJsonObject);

            fileStream.Close();
        }

        public static T LoadJsonFromFile<T>(string aPath)
        {
            if (File.Exists(aPath))
            {
                FileStream fileStream = File.Open(aPath, FileMode.Open);

                fileStream.Position = 0;

                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

                return (T)ser.ReadObject(fileStream);
            }
            else
            {
                return default(T);
            }
        }

        private static void DeleteFileIfExist(string aPath)
        {
            if (File.Exists(aPath))
            {
                File.Delete(aPath);
            }
        }

        public static string ConvertObjectToJson<T>(T aObject)
        {

        }*/
    }
}
