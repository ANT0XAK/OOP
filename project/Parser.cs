using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace project
{
    internal class Parser
    {
        private Dictionary<string, List<Cell>> _data;
        
        public void ReadInfo (string fileName)
        {
            if (fileName.Substring(fileName.Length - 3, 3) != "ini")
                throw new Exception("Wrong file format: use only .ini files!");
            if (!(File.Exists(fileName)))
                throw new Exception("File \"" + fileName + "\" not found!");
            
            _data = new Dictionary<string, List<Cell>>();
            var curSection = "DEFAULT";
            char[] charsToTrim = {' ', '\t'};
            using (var sr = new StreamReader(fileName))
            {
                while (sr.Peek() > -1)
                {
                    var lineInfo = sr.ReadLine().Replace("\t", "").Replace(" ", "");

                    if (string.IsNullOrEmpty(lineInfo))
                        continue;
                    if (lineInfo[0] == ';')
                        continue;
                    else if (lineInfo[0] == '[' && lineInfo[lineInfo.Length - 1] == ']')
                    {
                        curSection = lineInfo.Substring(1, lineInfo.Length - 2);
                        if (_data.ContainsKey(curSection))
                            throw new Exception("Wrong file format" + curSection);
                        _data.Add(curSection, new List<Cell>());
                        continue;
                    }

                    var position = lineInfo.IndexOf("=");
                    var name = lineInfo.Substring(0, position);
                    var value = lineInfo.Substring(position+1, lineInfo.Length - position - 1);
                    if (value.IndexOf(";") != -1)
                        value = value.Substring(0, value.IndexOf(";"));
                    name = name.Replace("\t", "");
                    value = value.Replace(" ", "");
                    if (curSection=="DEFAULT")
                        throw new Exception("Wrong file format");
                    _data[curSection].Add(new Cell(name, value));
                }
            }
        }
        


        public T GetValue<T>(string section, string name, string type = "Type")
        {
            if(!_data.ContainsKey(section))
                throw new Exception("No such name: " + section);
            var newname = name.Replace(" ", "");
            foreach (var c in _data[section])
            {
                if (c.Name == newname)
                {
                    //if (Is<T>(c.Value))
                        
                         return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(c.Value);
                    throw new Exception("Can't cast " + newname + " to type " + type);
                }
            }
            throw new Exception("No value with such name: " + newname);
        }
        
    }
}