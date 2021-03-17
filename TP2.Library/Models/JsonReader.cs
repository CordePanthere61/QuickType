using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace TP2.Library.Models
{
    class JsonReader
    {
        private string _plainText;
        
        public JsonReader()
        {
            
        }

        public void Parse(string plainText, IClassTemplate generatedClass)
        {
            _plainText = plainText;
            var objects = JObject.Parse(plainText);
            generatedClass.Variables = ParseVariables(objects);
            JObject root = JObject.Parse(plainText);
            GenerateVariables(root, generatedClass);
        }

        private void GenerateVariables(JObject root, IClassTemplate generatedClass)
        {
            if (root.Count != 0)
            {
                foreach (var child in root.Properties())
                {
                    if (child.Type == JTokenType.Object)
                    {
                        RecursiveGenerateClassVariable(child, generatedClass);
                    }
                    generatedClass.Variables.Add(GeneratePrimitiveVariable(child));
                }
                
            }
        }

        private void RecursiveGenerateClassVariable(JProperty child, IClassTemplate generatedClass)
        {
            if (child.Type != JTokenType.Object)
            {
                return;
            }
            Variable classVariable = GenerateClassVariable(child);
            if (child.Count != 0)
            {
                foreach (var innerChild in child.Children())
                {
                    RecursiveGenerateClassVariable((JProperty)innerChild, generatedClass);
                    classVariable.Variables.Add(GeneratePrimitiveVariable((JProperty)innerChild));
                }
            }
        }

        private List<Variable> ParseVariables(JContainer objects)
        {
            List<Variable> variables = new List<Variable>();
            foreach (var value in objects) {
                variables.Add(GenerateVariable(value.ToString()));
            }
            return variables;
        }

        private Variable GenerateVariable(string value)
        {
            Variable variable = new Variable();
            var values = value.Split(":");
            variable.Visibility = "public";
            variable.Type = "string";
            variable.Name = values[0].Trim('"');
            variable.Value = values[1];
            return variable;
        }

        //TODO faire une seule methode pour ces deux la ci dessous

        private Variable GeneratePrimitiveVariable(JProperty child)
        {
            Variable variable = new Variable();
            variable.Visibility = "public";
            variable.Type = child.Type.ToString();
            variable.Name = child.Name;
            variable.Value = child.Value.ToString();
            variable.Variables = null;
            return variable;
        }

        private Variable GenerateClassVariable(JProperty child)
        {
            Variable variable = new Variable();
            variable.Visibility = "public";
            variable.Type = child.Type.ToString();
            variable.Name = child.Name;
            variable.Value = child.Value.ToString();
            variable.Variables = new List<Variable>();
            return variable;
        }
    }
}
