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
        private List<Variable> _variables;
        private List<Method> _methods;

        private JsonReader _reader;

        public string Name { get => _name; set => _name = value; }
        public List<Variable> Variables { get => _variables; set => _variables = value; }
        public List<Method> Methods { get => _methods; set => _methods = value; }
        public string PlainText { get => _plainText; set => _plainText = value; }
        public string Type { get => _type; set => _type = value; }


        public CsharpClass()
        {
            _reader = new JsonReader();
        }

        public void Generate(string className, string plainText)
        {
            Name = className;
            _reader.Parse(plainText, this);
            foreach (Variable variable in Variables)
            {
                PlainText += variable.Format();
            }
        }
    }
}
