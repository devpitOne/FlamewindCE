using System;
using System.IO;
using Editor;
using Editor.NWNEE;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EditorTests
{
    /// <summary>
    /// TODO: None of these assert anything!
    /// TODO: Set up a relative path filestream
    /// </summary>
    [TestClass]
    public class YAML_Tests
    {
        readonly string fileName = @"<full string path here>";

        [TestMethod]
        public void YAMLDeserialize_Test()
        {
            var myFileStream = new FileStream(fileName, FileMode.Open);
            var testClass = YAMLFactory.Deserialize(myFileStream);

        }

        [TestMethod]
        public void YAMLConvertToConv_Test()
        {
            var myFileStream = new FileStream(fileName, FileMode.Open);
            var testClass = YAMLFactory.Deserialize(myFileStream);
            var result = YAMLFactory.ConvertToConv(testClass);
        }

        [TestMethod]
        public void YAMLConvertBackToStruct_Test()
        {
            var myFileStream = new FileStream(fileName, FileMode.Open);
            var testClass = YAMLFactory.Deserialize(myFileStream);
            var result = YAMLFactory.ConvertToConv(testClass);
            //var result = Conversation.NewEmptyConversation();
            var ready = YAMLFactory.ConvertToStruct(result);
            var serialized = YAMLFactory.Serialize(ready);
        }

        [TestMethod]
        public void YAMLSerialize_Test()
        {
            var myFileStream = new FileStream(fileName, FileMode.Open);
            var testClass = YAMLFactory.Deserialize(myFileStream);
            var result = YAMLFactory.ConvertToConv(testClass);
            var serialized = YAMLFactory.Serialize((NWNStruct)result);
        }
    }
}
