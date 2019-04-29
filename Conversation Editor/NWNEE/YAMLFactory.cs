using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace Editor.NWNEE
{

    public class NWNStructConverter : IYamlTypeConverter
    {
        //
        // Summary:
        //     Gets a value indicating whether the current converter supports converting the
        //     specified type.
        public bool Accepts(Type type)
        {
            if (type == typeof(NWNStruct))
                return true;
            else return false;
        }
        //
        // Summary:
        //     Reads an object's state from a YAML parser. NOT USED
        public object ReadYaml(IParser parser, Type type)
        {
            var rightNow = parser.Peek<NodeEvent>();
            parser.MoveNext();
            ParsingEvent nextUp = parser.Peek<Scalar>();
            parser.MoveNext();
            nextUp = parser.Peek<Scalar>();
            parser.MoveNext();
            nextUp = parser.Peek<Scalar>();
            parser.MoveNext();
            nextUp = parser.Peek<Scalar>();
            parser.MoveNext();
            nextUp = parser.Peek<Scalar>();
            parser.MoveNext();
            nextUp = parser.Peek<NodeEvent>();
            parser.MoveNext();
            nextUp = parser.Peek<Scalar>();
            return new object();
        }
        //
        // Summary:
        //     Writes the specified object's state to a YAML emitter.
        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            throw new NotImplementedException();
        }
    }

    public class NWNTextConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            if (type == typeof(NWNText))
                return true;
            else return false;
        }

        public object ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var nwnText = (NWNText)value;
            //Indent for the child nodes
            emitter.Emit(new MappingStart(null, null, true, MappingStyle.Block));
            //insert the key, scalars should come in pairs.
            emitter.Emit(new Scalar(null, null, "type", ScalarStyle.Plain, true, false));
            //insert the value
            emitter.Emit(new Scalar(null, null, nwnText.type, ScalarStyle.Plain, true, false));
            //insert the key, scalars should come in pairs.
            if (!string.IsNullOrEmpty(nwnText.str_ref)){
                emitter.Emit(new Scalar(null, null, "str_ref", ScalarStyle.Plain, true, false));
                //insert the value
                emitter.Emit(new Scalar(null, null, nwnText.str_ref, ScalarStyle.Plain, true, false));
            }
            //insert the key
            emitter.Emit(new Scalar(null, null, "value", ScalarStyle.Plain, true, false));
            //indent for the key's children
            emitter.Emit(new MappingStart(null, null, true, MappingStyle.Block));
            foreach (var pair in nwnText.value)
            {
                emitter.Emit(new Scalar(pair.Key));
                emitter.Emit(new Scalar(null, null, pair.Value, ScalarStyle.Plain, true, true));
            }
            //Close off our indents
            emitter.Emit(new MappingEnd());
            emitter.Emit(new MappingEnd());
        }
    }

    /// <summary>
    /// Used to serialize the NWNStruct Key/value pairs as the YAML file
    /// </summary>
    public class DictionaryConverter : IYamlTypeConverter
    {
        //
        // Summary:
        //     Gets a value indicating whether the current converter supports converting the
        //     specified type.
        public bool Accepts(Type type)
        {
            if (type == typeof(Dictionary<string, string>))
                return true;
            else return false;
        }
        //
        // Summary:
        //     Reads an object's state from a YAML parser. NOT USED
        public object ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Writes the specified object's state to a YAML emitter.
        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var dict = (Dictionary<string, string>)value;
            emitter.Emit(new MappingStart(null, "TEST", true, MappingStyle.Flow));
            foreach (var pair in dict)
            {
                emitter.Emit(new Scalar(null, pair.Key, pair.Key, ScalarStyle.Plain, true, false));
                emitter.Emit(new Scalar(null, pair.Key, pair.Value, ScalarStyle.Plain, true, false));
            }
            emitter.Emit(new MappingEnd());
        }
    }

    /// <summary>
    /// Orders the properties to match the YAML file
    /// </summary>
    public class NWNStructInspector : TypeInspectorSkeleton
    {
        private readonly ITypeInspector _innerTypeInspector;

        public NWNStructInspector(ITypeInspector innerTypeInspector)
        {
            _innerTypeInspector = innerTypeInspector;
        }

        public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
        {
            var result = _innerTypeInspector.GetProperties(type, container).OrderBy(x => x.Name);
            var lookup = result.ToList();
            var indexAL = lookup.FindIndex(x => x.Name == "AnimLoop");
            var indexA = lookup.FindIndex(x => x.Name == "Animation");
            if (indexAL > -1 && indexA > -1)
            {
                var Animation = lookup[indexA];
                lookup.Insert(indexAL + 1, lookup.ElementAt(indexA));
                lookup.Remove(Animation);
            }
            return lookup;
        }
    }

    public static class YAMLFactory
    {
        public static int idNum;
        public static NWNStruct master;

        #region Deserialization
        public static NWNStruct Deserialize(FileStream myFileStream)
        {
            NWNStruct messyObject = null;
            using (StreamReader reader = new StreamReader(myFileStream))
            {
                var deB = new DeserializerBuilder();
                deB.WithTagMapping("!nwn-lib.elv.es,2013-07:struct", typeof(NWNStruct));
                var deserializer = deB.Build();
                messyObject = (NWNStruct)deserializer.Deserialize(reader);
                reader.Close();
            }
            return messyObject;
        }

        public static Conversation ConvertToConv(NWNStruct messy)
        {
            messy.VersionNumber = 2;
            idNum = 1;
            if (messy.StartingList.value.Count > 0)
            {
                master = messy;
                master.subNodes = ReadStructs(messy.StartingList.value, true);
                master.subNodes = ReadStructsLinks(messy.subNodes);
                messy = master;
                master = null;
            }
            return messy;
        }

        /// <summary>
        /// TODO: Read in order that nodes are to keep file from changing around
        /// Not perfectly encapsulated, relies on master(the variable)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static List<ContentNode> ReadStructs(List<NWNStruct> list, bool entry)
        {
            var orderNum = 1;
            var returnList = new List<ContentNode>();
            //Tag is the link, Node is the linked to text
            foreach (var entryTag in list)
            {
                var newNode = ContentNode.NewContentNode(idNum, orderNum);
                NWNStruct oldNode = null;
                if (entry)
                {

                    int index = master.EntryList.value.FindIndex(x => x.__struct_id == entryTag.Index["value"]);
                    //TODO: Are we actually going to use these?
                    if (master.EntryList.value[index].idNum == null && (entryTag.IsChild == null || entryTag.IsChild["value"] == "0"))
                        master.EntryList.value[index].idNum = idNum;
                    oldNode = master.EntryList.value[index];
                }
                else
                {
                    int index = master.ReplyList.value.FindIndex(x => x.__struct_id == entryTag.Index["value"]);
                    if (master.ReplyList.value[index].idNum == null && (entryTag.IsChild == null || entryTag.IsChild["value"] == "0"))
                        master.ReplyList.value[index].idNum = idNum;
                    oldNode = master.ReplyList.value[index];
                }
                idNum++;
                if (entryTag.IsChild != null && entryTag.IsChild["value"] == "1")
                {
                    newNode.conversationComments = entryTag.LinkComment["value"];
                    newNode.idNum = 0;
                    newNode.linkTo = -1;
                    newNode.additionalData.Add(new Info("linkTo", oldNode.__struct_id));
                }
                else
                {
                    if (oldNode.Text.str_ref != null)
                        newNode.additionalData.Add(new Info("str_ref", oldNode.Text.str_ref));
                    if (oldNode.Text.value != null && oldNode.Text.value.Count > 0)
                        newNode.conversationText = oldNode.Text.value.FirstOrDefault().Value;
                    //StartingList Attributes
                    if (entryTag.Active != null)
                        newNode.additionalData.Add(new Info("Active", entryTag.Active["value"]));
                    //Reply/EntryList Attributes
                    if (oldNode.AnimLoop != null)
                        newNode.additionalData.Add(new Info("AnimLoop", oldNode.AnimLoop["value"]));
                    if (oldNode.Animation != null)
                        newNode.additionalData.Add(new Info("Animation", oldNode.Animation["value"]));
                    newNode.conversationComments = oldNode.Comment["value"];
                    if (oldNode.Delay != null)
                        newNode.additionalData.Add(new Info("Delay", oldNode.Delay["value"]));
                    if (oldNode.Quest != null)
                        newNode.additionalData.Add(new Info("Quest", oldNode.Quest["value"]));
                    if (oldNode.Script != null)
                        newNode.additionalData.Add(new Info("Script", oldNode.Script["value"]));
                    if (oldNode.Sound != null)
                        newNode.additionalData.Add(new Info("Sound", oldNode.Sound["value"]));
                    //EntryList Properties
                    if (oldNode.Speaker != null)
                        newNode.additionalData.Add(new Info("Speaker", oldNode.Speaker["value"]));

                    if (oldNode.EntriesList != null && oldNode.EntriesList.value.Count > 0)
                    {
                        newNode.subNodes = ReadStructs(oldNode.EntriesList.value, true);
                    }
                    else if (oldNode.RepliesList != null && oldNode.RepliesList.value.Count > 0)
                    {
                        newNode.subNodes = ReadStructs(oldNode.RepliesList.value, false);
                    }
                }
                newNode.nodeType = entry ? ConversationNodeType.NPC : ConversationNodeType.PC;
                orderNum++;
                returnList.Add(newNode);
            }
            return returnList;
        }

        /// <summary>
        /// Not perfectly encapsulated, relies on master
        /// </summary>
        private static List<ContentNode> ReadStructsLinks(List<ContentNode> conv)
        {
            foreach (var cNode in conv)
            {
                if (cNode.linkTo == -1)
                {
                    NWNList nodeList;
                    if (cNode.nodeType == ConversationNodeType.NPC)
                    {
                        nodeList = master.EntryList;
                    }
                    else
                    {
                        nodeList = master.ReplyList;
                    }
                    foreach (var ymlNode in nodeList.value)
                    {
                        var structidIndex = cNode.additionalData.FindIndex(x => x.variableName == "linkTo");
                        if (ymlNode.__struct_id == cNode.additionalData[structidIndex].variableValue)
                        {
                            cNode.linkTo = ymlNode.idNum.Value;
                            cNode.additionalData.RemoveAt(structidIndex);
                            break;
                        }
                    }
                    if (cNode.linkTo == -1)
                        throw new Exception("Could not import a conversation link.");
                }
                ReadStructsLinks(cNode.subNodes);
            }
            return conv;
        }


        #endregion

        #region Serialization
        public static string Serialize(NWNStruct conv)
        {
            var seB = new SerializerBuilder();
            seB.WithTagMapping("!nwn-lib.elv.es,2013-07:struct", typeof(NWNStruct));
            seB.WithTypeConverter(new DictionaryConverter());
            seB.WithTypeConverter(new NWNTextConverter());
            seB.WithTypeInspector(x => new NWNStructInspector(x));
            var serializer = seB.Build();
            return "--- " + serializer.Serialize(conv);
        }

        public static NWNStruct ConvertToStruct(Conversation conv)
        {
            try
            {
                master = (NWNStruct)conv;
            }
            catch (InvalidCastException)
            {
                //It is a new conversation to be converted for the first time
                master = new NWNStruct
                {
                    subNodes = conv.subNodes
                };
            }
            if (string.IsNullOrEmpty(master.__data_type))
                master.__data_type = "DLG";
            if (string.IsNullOrEmpty(master.__struct_id))
                master.__struct_id = "4294967295";
            if (master.DelayEntry == null)
            {
                master.DelayEntry = TypeValue("dword", "0");
            }
            if (master.DelayReply == null)
            {
                master.DelayReply = TypeValue("dword", "0");
            }
            if (master.EndConverAbort == null)
            {
                master.EndConverAbort = TypeValue("resref", "nw_walk_wp");
            }
            if (master.EndConversation == null)
            {
                master.EndConversation = TypeValue("resref", "nw_walk_wp");
            }
            //TODO: Fix NumWords, should be a word count? The toolset seems to automatically update it so it may not matter
            if (master.NumWords == null)
            {
                master.NumWords = TypeValue("dword", "0");
            }
            else
            {
                //Do something here too
            }
            if (master.PreventZoomIn == null)
            {
                master.PreventZoomIn = TypeValue("byte", "0");
            }
            master.EntryList = new NWNList();
            master.ReplyList = new NWNList();
            conv.subNodes = WriteStructs(conv.subNodes);
            conv.subNodes = WriteLinks(conv.subNodes);
            master.StartingList = new NWNList();
            int structIter = 0;
            foreach (var rootNode in conv.subNodes)
            {
                var newNode = new NWNStruct
                {
                    __struct_id = structIter.ToString()
                };
                Info checkFor;
                newNode.Active = TypeValue("resref", (checkFor = rootNode.additionalData.Find(x => x.variableName == "Active")) != null ? checkFor.variableValue : "");
                newNode.Index = TypeValue("dword", rootNode.additionalData.Find(x => x.variableName == "__struct_id").variableValue);
                master.StartingList.value.Add(newNode);
                structIter++;
            }
            return master;
        }

        private static List<ContentNode> WriteStructs(List<ContentNode> conv)
        {
            foreach (var oldNode in conv)
            {
                if (!oldNode.isLink)
                {
                    NWNStruct newNode = new NWNStruct();
                    //Get values from old node if present
                    Info checkFor;
                    newNode.AnimLoop = TypeValue("byte", (checkFor = oldNode.additionalData.Find(x => x.variableName == "AnimLoop")) != null ? checkFor.variableValue : "1");
                    newNode.Animation = TypeValue("dword", (checkFor = oldNode.additionalData.Find(x => x.variableName == "Animation")) != null ? checkFor.variableValue : "0");
                    newNode.Delay = TypeValue("dword", (checkFor = oldNode.additionalData.Find(x => x.variableName == "Delay")) != null ? checkFor.variableValue : "0");
                    newNode.Quest = TypeValue("cexostr", (checkFor = oldNode.additionalData.Find(x => x.variableName == "Quest")) != null ? checkFor.variableValue : "");
                    newNode.Script = TypeValue("resref", (checkFor = oldNode.additionalData.Find(x => x.variableName == "Script")) != null ? checkFor.variableValue : "");
                    newNode.Sound = TypeValue("resref", (checkFor = oldNode.additionalData.Find(x => x.variableName == "Sound")) != null ? checkFor.variableValue : "");
                    //fill out
                    newNode.Comment = TypeValue("cexostr", oldNode.conversationComments);
                    var str_ref = (checkFor = oldNode.additionalData.Find(x => x.variableName == "str_ref")) != null ? checkFor.variableValue : null;
                    newNode.Text = new NWNText
                    {
                        type = "cexolocstr",
                        str_ref = str_ref,
                        value = new Dictionary<string, string>()
                    };
                    if (oldNode.conversationText != null)
                        newNode.Text.value.Add("0", oldNode.conversationText);

                    if (oldNode.nodeType == ConversationNodeType.NPC)
                    {
                        newNode.__struct_id = master.EntryList.value.Count.ToString();
                        newNode.Speaker = TypeValue("cexostr", (checkFor = oldNode.additionalData.Find(x => x.variableName == "Speaker")) != null ? checkFor.variableValue : "");
                        newNode.RepliesList = new NWNList();
                        master.EntryList.value.Add(newNode);
                    }
                    else
                    {
                        newNode.__struct_id = master.ReplyList.value.Count.ToString();
                        newNode.EntriesList = new NWNList();
                        master.ReplyList.value.Add(newNode);
                    }
                    oldNode.additionalData.Add(new Info("__struct_id", newNode.__struct_id));
                    oldNode.subNodes = WriteStructs(oldNode.subNodes);
                }
                else
                {
                    //Note this to be added as a link later?
                }
            }
            return conv;
        }

        /// <summary>
        /// Not encapsulated, relise on master for linkTos
        /// </summary>
        /// <param name="conv"></param>
        /// <returns></returns>
        private static List<ContentNode> WriteLinks(List<ContentNode> conv)
        {
            foreach (var oldNode in conv)
            {
                NWNList masterList;
                NWNStruct newNode;
                int index;
                if (oldNode.nodeType == ConversationNodeType.NPC)
                    masterList = master.EntryList;
                else
                    masterList = master.ReplyList;
                //Temporary fix until I finish handling linkTos
                var structIdNode = oldNode.additionalData.Find(y => y.variableName == "__struct_id");
                if (structIdNode == null)
                {
                    throw new Exception("No matching struct exists for this node. Report this error.");
                }
                index = masterList.value.FindIndex(x => x.__struct_id == structIdNode.variableValue);

                newNode = masterList.value[index];
                int structIter = 0;
                foreach (var child in oldNode.subNodes)
                {
                    var newReply = new NWNStruct
                    {
                        __struct_id = structIter.ToString()
                    };
                    //Does Active need to carry a value?
                    newReply.Active = TypeValue("resref", "");
                    var structIdNodeChild = child.additionalData.Find(y => y.variableName == "__struct_id");
                    if (structIdNodeChild == null)
                    {
                        if (child.linkTo > 0)
                        {
                            var linkedNode = SearchNodeLists(master.subNodes, child.linkTo);
                            structIdNodeChild = linkedNode.additionalData.Find(y => y.variableName == "__struct_id");
                            child.additionalData.Add(new Info("__struct_id", structIdNodeChild.variableValue));
                        }
                    }
                    newReply.Index = TypeValue("dword", structIdNodeChild.variableValue);
                    newReply.IsChild = TypeValue("byte", child.linkTo > 0 ? "1" : "0");
                    newReply.LinkComment = child.linkTo > 0 ? TypeValue("cexostr", child.conversationComments) : null;
                    if (oldNode.nodeType == ConversationNodeType.NPC)
                    {
                        newNode.RepliesList.value.Add(newReply);
                    }
                    else newNode.EntriesList.value.Add(newReply);
                    structIter++;
                }
                masterList.value[index] = newNode;
                WriteLinks(oldNode.subNodes);
            }
            return conv;
        }

        private static Dictionary<string, string> TypeValue(string type, string value)
        {
            return new Dictionary<string, string> {
                    { "type", type },
                    {"value", value }
                };
        }

        private static ContentNode SearchNodeLists(List<ContentNode> conv, int idNum)
        {
            ContentNode returnNode;
            if ((returnNode = conv.Find(x => x.idNum == idNum)) != null)
            {
                return returnNode;
            }
            else
            {
                foreach (var node in conv)
                {
                    var found = SearchNodeLists(node.subNodes, idNum);
                    if (found != null)
                        return found;
                }
                return null;
            }
        }
        #endregion
    }
}
