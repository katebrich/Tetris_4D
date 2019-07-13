using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections;

namespace Tetris4D
{
    /// <summary>
    /// Formatter, using reflection to serialize or deserialize objects.
    /// </summary>
    class MyFormatter
    {
        private string separator = ";;;";
        string[] lines;
        int index = 0;
      
        /// <summary>
        /// Serializes given object using given writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="graph"></param>
        public void Serialize(TextWriter writer, object graph)
        {
            callOnSerializing(graph);

            writer.Write(graph.GetType().AssemblyQualifiedName);
            writer.Write(separator);

            serialize(writer, graph);

            callOnSerialized(graph);
        }

        /// <summary>
        /// Deserializes object from given reader and returns the result.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public object Deserialize(TextReader reader)
        {
            string all = reader.ReadToEnd();
            lines = all.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            string str = lines[index];
            index++;
            Type tp = Type.GetType(str);
            object o = Activator.CreateInstance(tp);

            callOnDeserializing(o);
            deserialize(o);
            callOnDeserialized(o);

            return o;
        }

        private void serialize(TextWriter writer, object graph)
        {
            Type tp = graph.GetType();
            FieldInfo[] infos = tp.GetFields(
                       BindingFlags.Instance |
                       BindingFlags.Static |
                       BindingFlags.NonPublic |
                       BindingFlags.Public
                       );
            foreach (var info in infos)
            {
                if (info.IsNotSerialized)
                    continue;

                Type type = info.FieldType;
                string[] namesp = type.Namespace.Split('.'); 
                if ((namesp.Length > 0 && namesp[0] == "System") || type.IsEnum || typeof(IEnumerable).IsAssignableFrom(type))
                {
                    string str = info.GetValue(graph).BinarySerializeToString();
                    writer.Write(str);
                    writer.Write(separator);
                }
                else
                {
                    writer.Write(info.GetValue(graph).GetType().AssemblyQualifiedName);
                    writer.Write(separator);
                    serialize(writer, info.GetValue(graph));
                }
            }
        }

        private void deserialize(object graph)
        {
            FieldInfo[] infos = graph.GetType().GetFields(
                       BindingFlags.Instance |
                       BindingFlags.Static |
                       BindingFlags.NonPublic |
                       BindingFlags.Public
                       );
            foreach (var info in infos)
            {
                if (info.IsNotSerialized)
                    continue;

                Type type = info.FieldType;
                string[] namesp = type.Namespace.Split('.');
                if ((namesp.Length > 0 && namesp[0] == "System") || type.IsEnum || typeof(IEnumerable).IsAssignableFrom(type))
                {
                    object deserObject = lines[index].BinaryDeserializeFromString();
                    index++;
                    info.SetValue(graph, deserObject);
                }
                else
                {
                    string str = lines[index];
                    index++;
                    Type tp = Type.GetType(str);
                    object o = Activator.CreateInstance(tp);
                    info.SetValue(graph, o);
                    deserialize(o);
                }

            }
        }

        
        /// <summary>
        /// Finds methods with OnDeserialized attribute and calls them.
        /// </summary>
        /// <param name="deserializedObject"></param>
        private void callOnDeserialized(object deserializedObject)
        {
            IEnumerable<MethodInfo> infos = deserializedObject.GetType().GetMethods().Where(
                m => m.GetCustomAttributes(typeof(OnDeserializedAttribute), true).Length > 0
                );
            foreach (var info in infos)
            {
                info.Invoke(deserializedObject, new object[] { new StreamingContext() });
            }
        }

        /// <summary>
        /// Finds methods with OnDeserializing attribute and calls them.
        /// </summary>
        /// <param name="deserializedObject"></param>
        private void callOnDeserializing(object deserializedObject)
        {
            IEnumerable<MethodInfo> infos = deserializedObject.GetType().GetMethods().Where(
                m => m.GetCustomAttributes(typeof(OnDeserializingAttribute), true).Length > 0
                );
            foreach (var info in infos)
            {
                info.Invoke(deserializedObject, new object[] { new StreamingContext() });
            }
        }

        /// <summary>
        /// Finds methods with OnSerialized attribute and calls them.
        /// </summary>
        /// <param name="serializedObject"></param>
        private void callOnSerialized(object serializedObject)
        {
            IEnumerable<MethodInfo> infos = serializedObject.GetType().GetMethods().Where(
                m => m.GetCustomAttributes(typeof(OnSerializedAttribute), true).Length > 0
                );
            foreach (var info in infos)
            {
                info.Invoke(serializedObject, new object[] { new StreamingContext() });
            }
        }

        /// <summary>
        /// Finds methods with OnSerializing attribute and calls them.
        /// </summary>
        /// <param name="serializedObject"></param>
        private void callOnSerializing(object serializedObject)
        {
            IEnumerable<MethodInfo> infos = serializedObject.GetType().GetMethods().Where(
                m => m.GetCustomAttributes(typeof(OnSerializingAttribute), true).Length > 0
                );
            foreach (var info in infos)
            {
                info.Invoke(serializedObject, new object[] { new StreamingContext() });
            }
        }
    }
}
