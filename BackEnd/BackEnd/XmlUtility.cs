using System.IO;
using System.Xml.Serialization;

public static class XmlUtility
{
    public static void Save<T>(T data, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(fs, data);
        }
    }

    public static T Load<T>(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            return (T)serializer.Deserialize(fs);
        }
    }
}
