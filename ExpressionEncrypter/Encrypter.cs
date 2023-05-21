using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Xml;
using System.Xml.Serialization;

namespace ExpressionEncrypter
{
    public class Encrypter
    {
        public object ObjectToEncrypt { get; set; }

        public Encrypter()
        {
            
        }

        public void Save(object objectToEncrypt, string path,
                         string key)
        {
            ObjectToEncrypt = objectToEncrypt;
            try
            {
                XmlSerializer serializer = new XmlSerializer(ObjectToEncrypt.GetType());
                MemoryStream memStream = new MemoryStream();
                serializer.Serialize(memStream, objectToEncrypt);
                var byteArray = memStream.ToArray();
                string base64String = Convert.ToBase64String(byteArray);
                StreamWriter textWriter = File.CreateText(path);
                textWriter.WriteLine(base64String);
                textWriter.Close();
            }
            catch (Exception ex)
            {
                Debug.Assert(false);
            }
        }

        public void Read(string path, out object decryptedObjectpt, Type type)
        {
            decryptedObjectpt = null;
            string currentDir = Environment.CurrentDirectory;
            if (File.Exists(path))
            {
                var lines = File.ReadLines(path);
                if ((lines != null) && (lines.Count() == 1))
                {
                    try
                    {
                        byte[] bytes = Convert.FromBase64String(lines.First());
                        string objectToDecrypt = BitConverter.ToString(bytes);
                        XmlSerializer serializer = new XmlSerializer(type);
                        MemoryStream stream = new MemoryStream(bytes);
                        decryptedObjectpt = serializer.Deserialize(stream);
                    }
                    catch (Exception ex)
                    {
                        Debug.Assert(false);
                    }
                }
            }
        }

    }
}
