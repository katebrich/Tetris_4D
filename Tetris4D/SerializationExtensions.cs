using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tetris4D
{
    public static class SerializationExtensions
    {

        public static string BinarySerializeToString(this object objectInstance)
        {
            BinaryFormatter formatter = new BinaryFormatter();// objectInstance.GetType());

            string str;
            using (Stream stream = new FileStream("temp", FileMode.Create))
            {
                formatter.Serialize(stream, objectInstance);
                stream.Position = 0;
                //StreamReader reader = new StreamReader(stream);
                //str = reader.ReadToEnd();
                str = Convert.ToBase64String(ReadFully(stream)); //
                stream.Close();
            }             

            return str;
            
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        public static object BinaryDeserializeFromString(this string objectData)
        {
            var formatter = new BinaryFormatter();
            object result;

            byte[] byteArray = Convert.FromBase64String(objectData);

           //byte[] byteArray = Encoding.UTF8.GetBytes(objectData);
           Stream stream = new MemoryStream(byteArray);

            result = formatter.Deserialize(stream);

            stream.Close();

            return result;
        }

        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }       
    }
}
