using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using TheDebtBook_Assignment1.Models;

namespace TheDebtBook_Assignment1.Data
{
    public class Repository
    {
        internal static void ReadFile(string fileName, out ObservableCollection<Dept> depts)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dept>));
            TextReader reader = new StreamReader(fileName);
            // Deserialize all the agents.
            depts = (ObservableCollection<Dept>)serializer.Deserialize(reader);
            reader.Close();
        }

        internal static void SaveFile(string fileName, ObservableCollection<Dept> depts)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dept>));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize all the agents.
            serializer.Serialize(writer, depts);
            writer.Close();
        }
    }
}
