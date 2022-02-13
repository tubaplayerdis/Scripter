using System.Data;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Data_Management
{

    public class FileLocationsDataNotFoundException : Exception
    {
        public string LookLocation { get; }
        public FileLocationsDataNotFoundException(string messageparam, string looklocation)
            : base(messageparam)
        {
            LookLocation = looklocation;
        }
    }
    [Serializable]
    public static class Data
    {
        [XmlElement(ElementName = "FileLocations")]
        public static List<string> FileLocations { get; set; }
    }
    public static class LocalData
    {
        private static string AppDataFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Scripter_Data\Data.xml";

        
         
        public static void SerializeAndSaveData()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<string>));            
            CheckOrCreateAppDataFolderAndFile();
            StreamWriter writer = new StreamWriter(AppDataFile);
            xsSubmit.Serialize(writer, Data.FileLocations);
        }

        public static void InitRuntimeData()
        {
            Data.FileLocations = DeserilizeAndReadData();
        }

        public static List<string> DeserilizeAndReadData()
        {
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(List<string>));
                CheckOrCreateAppDataFolderAndFile();
                FileStream writer = new FileStream(path: AppDataFile, FileMode.Open);
                List<string> lol = (List<string>)xsSubmit.Deserialize(writer);
                return lol;
            } catch (Exception ex)
            {
                Engine_Work.Dialogs.ErrorBoxShow(ex.Message);
                return new List<string>();
            }
            
        }

        private static void CheckOrCreateAppDataFolderAndFile()
        {
            string preappdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //MessageBox.Show(preappdata);
            string appdata = preappdata + @"\Scripter_Data";
            if (Directory.Exists(appdata))
            {
                if (!File.Exists(AppDataFile))
                {
                    File.Create(AppDataFile);
                    SerializeAndSaveData();
                }
            }
            else
            {
                Directory.CreateDirectory(appdata);
                File.Create(AppDataFile);
                SerializeAndSaveData();
            }

        }
    } 
}