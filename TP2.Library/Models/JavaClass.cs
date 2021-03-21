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

        private JsonReader _reader;

        public List<Variable> Variables { get => _variables; set => _variables = value; }
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



        public void Generate(string className, string plainText)
        {
            Variables = new List<Variable>();
            Name = className;
            _reader.Parse(plainText, this);
            PlainText = FormatText();
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

        public string FormatPrimitiveVariables()
        {
            List<String> gettersSetters = new List<string>();
            string returnValue = "";
            foreach (Variable variable in Variables)
            {
                string getterAndSetter = "";
                if (variable.Variables != null)
                {
                    returnValue += $"    {variable.Visibility} {char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)} {variable.Name};\n";
                    getterAndSetter += $"    {variable.Visibility} {char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)} get{char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)}() {{ return {variable.Name};}}\n";
                    getterAndSetter += $"    {variable.Visibility} void set{char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)}({char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)} value) {{this.{variable.Name} = value; }}\n";
                }
                else
                {
                    returnValue += $"    {variable.Visibility} {variable.Type} {variable.Name};\n";
                    getterAndSetter += $"    {variable.Visibility} {variable.Type} get{char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)}() {{ return {variable.Name};}}\n";
                    getterAndSetter += $"    {variable.Visibility} void set{char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)}({variable.Type} value) {{this.{variable.Name} = value; }}\n";
                }
                gettersSetters.Add(getterAndSetter);
            }
            returnValue += "\n";
            foreach (string getterSetter in gettersSetters)
            {
                returnValue += getterSetter + "\n";
            }
            
            return returnValue + "}\n\n";
        }

        public string FormatClassVariables(List<Variable> classVariables)
        {
            
            string returnValue = "";
            foreach (Variable variable in classVariables)
            {
                List<String> gettersSetters = new List<string>();
                returnValue += $"{variable.Visibility} class {char.ToUpper(variable.Name[0]) + variable.Name.Substring(1)} {{\n";
                foreach (Variable innerVariable in variable.Variables)
                {
                    string getterAndSetter = "";
                    if (innerVariable.Variables != null)
                    {
                        returnValue += $"    {innerVariable.Visibility} {char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)} {innerVariable.Name};\n";
                        getterAndSetter += $"    {innerVariable.Visibility} {char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)} get{char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)}() {{ return {innerVariable.Name};}}\n";
                        getterAndSetter += $"    {innerVariable.Visibility} void set{char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)}({char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)} value) {{this.{innerVariable.Name} = value; }}\n";
                    }
                    else
                    {
                        returnValue += $"    {innerVariable.Visibility} {innerVariable.Type} {innerVariable.Name};\n";
                        getterAndSetter += $"    {innerVariable.Visibility} {innerVariable.Type} get{char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)}() {{ return {innerVariable.Name};}}\n";
                        getterAndSetter += $"    {innerVariable.Visibility} void set{char.ToUpper(innerVariable.Name[0]) + innerVariable.Name.Substring(1)}({innerVariable.Type} value) {{this.{innerVariable.Name} = value; }}\n";
                    }
                    gettersSetters.Add(getterAndSetter);
                }
                returnValue += "\n";
                foreach (string getterSetter in gettersSetters)
                {
                    returnValue += getterSetter + "\n";
                }
            }
            return returnValue + "}\n\n";
        }
    }
}
