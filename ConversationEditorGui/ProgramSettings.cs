using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace ConversationEditorGui
{
    [Serializable]
    public class ProgramSettings
    {
        private const string FILENAME = @"CELocalSettings.config";

        private string filePath = "";

        [XmlArrayItem("ExpandCollection")]
        public List<ExpandSettings> ExpandCollection = new List<ExpandSettings>();

        public ProgramSettings()
        {
            this.filePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            int length = this.filePath.LastIndexOfAny(new char[2] { '\\', '/' });
            this.filePath = this.filePath.Substring(0, length + 1);
        }

        public static ProgramSettings LoadSettings()
        {
            ProgramSettings toReturn = new ProgramSettings();
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
            FileStream myFileStream = null;
            if (File.Exists(toReturn.filePath + FILENAME))
            {
                try
                {
                    myFileStream = new FileStream(toReturn.filePath + FILENAME, FileMode.Open);
                    if (myFileStream.Length > 0)
                    {
                        toReturn = (ProgramSettings)serializer.Deserialize(myFileStream);
                    }
                }
                catch (Exception ex)
                {
                    string strErr = String.Format("Unable to open xml file. Error:\n{0}", ex.Message);
                    MessageBox.Show(strErr, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (myFileStream != null)
                    {
                        myFileStream.Close();
                    }
                }
                return toReturn;
            }
            return new ProgramSettings();
        }

        public void Save()
        {
            StreamWriter writer = null;
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(ProgramSettings));
                writer = new StreamWriter(filePath + FILENAME);
                ser.Serialize(writer, this);
            }
            catch (Exception ex)
            {
                string strErr = "Unable to save expand settings. Error: " + ex.Message;
                MessageBox.Show(strErr, "Save File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                writer = null;
            }
        }

        public void AddSetting(string key, List<int> value)
        {
            if (ContainsKey(key))
            {
                this.Remove(key);
            }
            ExpandSettings tempSetting = new ExpandSettings(key, value);
            ExpandCollection.Add(tempSetting);
        }

        public void Remove(string key)
        {
            if (ExpandCollection != null)
            {
                List<ExpandSettings> duplicates = new List<ExpandSettings>();
                foreach (ExpandSettings setting in ExpandCollection)
                {
                    if (setting.fileName == key)
                    {
                        duplicates.Add(setting);
                    }
                }
                foreach (ExpandSettings setting in duplicates)
                {
                    ExpandCollection.Remove(setting);
                }
            }
        }
        
        public bool ContainsKey(string key)
        {
            if (ExpandCollection != null)
            {
                foreach (ExpandSettings setting in ExpandCollection)
                {
                    if (setting.fileName == key)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<int> GetValue(string key)
        {
            if (ExpandCollection != null)
            {
                foreach (ExpandSettings setting in ExpandCollection)
                {
                    if (setting.fileName == key)
                    {
                        return setting.expandList;
                    }
                }
            }
            return null;
        }
    }
}
