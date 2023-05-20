using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace ExpressionEncrypter
{
    public class Encrypter
    {
        public object ObjectToEncrypt { get; set; }

        public Encrypter()
        {
            
        }

        public void Save(object objectToEncrypt, string path)
        {
            ObjectToEncrypt = objectToEncrypt;
            Stream stream = new FileStream( path, FileMode.OpenOrCreate);
            try
            {
                XmlSerializer serializer = new XmlSerializer(ObjectToEncrypt.GetType());
                MemoryStream memStream = new MemoryStream();
                serializer.Serialize(memStream, objectToEncrypt);
                var byteArray = memStream.ToArray();
                string base64String = Convert.ToBase64String(byteArray);
                serializer.Serialize(stream, ObjectToEncrypt);
                stream.Close();
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
                Stream? stream = null;
                try
                {
                    stream = new FileStream(path, FileMode.Open);
                }
                catch (Exception ex)
                {
                    Debug.Assert(false);
                }
                if (stream != null)
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(type);
                        decryptedObjectpt = serializer.Deserialize(stream);
                        stream.Close();
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
