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
            Variables = new List<Variable>();
            Name = className;
            _reader.Parse(plainText, this);
            PlainText = FormatText();
        }

        public string FormatText()
        {
            return FormatNameSpace() + FormatClassName() + FormatVariables() + "}";
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
            List<Variable> classVariables = new List<Variable>();
            foreach (Variable variable in Variables)
            {
                RecursiveFindClassVariables(variable, classVariables);
            }
            return FormatPrimitiveVariables() + FormatClassVariables(classVariables);
        }

        public void RecursiveFindClassVariables(Variable variable, List<Variable> classVariables)
        {
            if (variable.Variables == null)
            {
                return;
            }
            classVariables.Add(variable);
            foreach (Variable innerVariable in variable.Variables)
            {
                RecursiveFindClassVariables(innerVariable, classVariables);
            }
        }

        public string FormatClassVariables(List<Variable> classVariables)
        {
            string returnValue = "";
            foreach (Variable variable in classVariables)
            {
                returnValue += $"    {variable.Visibility} partial class {char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)}\n    {{\n";
                foreach (Variable innerVariable in variable.Variables)
                {
                    returnValue += $"        [JsonProperty(\"{innerVariable.Name}\")]\n";
                    returnValue += $"        {innerVariable.Visibility} {char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)} {innerVariable.Name} {{ get; set; }}\n\n";
                }
                returnValue += "    }\n\n";
            }
            return returnValue;
        }

        public string FormatPrimitiveVariables()
        {
            string returnValue = "";
            foreach (Variable variable in Variables)
            {
                returnValue += $"        [JsonProperty(\"{variable.Name}\")]\n";
                if (variable.Variables != null)
                {
                    returnValue += $"        {variable.Visibility} {char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)} {variable.Name} {{ get; set; }}\n\n";
                } else
                {
                    returnValue += $"        {variable.Visibility} {variable.Type} {variable.Name} {{ get; set; }}\n\n";
                }
            }
            return returnValue + "    }\n\n";
        }
    }
}
