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
                         bool base64, string key)
        {
            ObjectToEncrypt = objectToEncrypt;
            try
            {
                XmlSerializer serializer = new XmlSerializer(ObjectToEncrypt.GetType());
                //if (base64)
                //{
                    MemoryStream memStream = new MemoryStream();
                    serializer.Serialize(memStream, objectToEncrypt);
                    var byteArray = memStream.ToArray();
                    string base64String = Convert.ToBase64String(byteArray);
                string pathBase64 = "64_" + path;
                    StreamWriter textWriter = File.CreateText(pathBase64);
                    textWriter.WriteLine(base64String);
                textWriter.Close();
                //}
                //else
                //{
                    Stream stream = new FileStream(path, FileMode.OpenOrCreate);
                    serializer.Serialize(stream, objectToEncrypt);
                //}
            }
            catch (Exception ex)
            {
                Debug.Assert(false);
            }
        }

        public void Read(string path, out object decryptedObjectpt, Type type)
        {
            decryptedObjectpt = null;
            string pathBase64 = "64_" + path;
            string currentDir = Environment.CurrentDirectory;
            if (File.Exists(pathBase64))
            {
                var lines = File.ReadLines(pathBase64);
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
