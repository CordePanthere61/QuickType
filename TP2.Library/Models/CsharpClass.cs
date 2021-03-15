using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    class CsharpClass : IClassTemplate
    {
        private string _name;
        private string _plainText;
        private string _type = "C#";
        private string _namespace = "QuickType";
        private List<Variable> _variables;
        private JsonReader _reader;

        public string Name { get => _name; set => _name = value; }
        public List<Variable> Variables { get => _variables; set => _variables = value; }
        public string PlainText { get => _plainText; set => _plainText = value; }
        public string Type { get => _type; set => _type = value; }
        public string Namespace { get => _namespace; set => _namespace = value; }

        public CsharpClass()
        {
            _reader = new JsonReader();
        }

        public void Generate(string className, string plainText)
        {
            Name = className;
            _reader.Parse(plainText, this);
            PlainText = FormatText();
        }

        public string FormatText()
        {
            return FormatNameSpace() + FormatClassName() + FormatVariables();
        }

        public string FormatNameSpace()
        {
            return $"namespace {Namespace} \n{{\n";
        }

        public string FormatClassName()
        {
            return $"    public class {Name}\n    {{\n";
        }

        public string FormatVariables()
        {
            string returnValue = "";
            foreach (Variable variable in Variables)
            {
                returnValue += $"        {variable.Format()}";
            }
            return returnValue;
        }
    }
}
