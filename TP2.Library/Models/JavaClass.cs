using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    class JavaClass : IClassTemplate
    {
        private string _name;
        private string _plainText;
        private string _type = "Java";
        private List<Variable> _variables;
        private List<Method> _methods;

        private JsonReader _reader;

        public List<Variable> Variables { get => _variables; set => _variables = value; }
        public List<Method> Methods { get => _methods; set => _methods = value; }
        public string Name { get => _name; set => _name = value; }
        public string Type { get => _type; set => _type = value; }
        public string PlainText { get => _plainText; set => _plainText = value; }

        public JavaClass()
        {
            _reader = new JsonReader();
        }

        public string FormatText()
        {
            return FormatClassName() + FormatVariables();
        }
        public string FormatClassName()
        {
            return $"public class {Name} {{\n";
        }

        public string FormatVariables()
        {
            string returnValue = "";
            foreach (Variable variable in Variables)
            {
                returnValue += $"    {variable.Format()}";
            }
            return returnValue;
        }

        public void Generate(string className, string plainText)
        {
            Name = className;
            _reader.Parse(plainText, this);
            PlainText = FormatText();
        }
    }
}
