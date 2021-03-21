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
            JObject root = JObject.Parse(plainText);
            GenerateVariables(root, generatedClass);
        }

        private void GenerateVariables(JObject root, IClassTemplate generatedClass)
        {
            if (root.Count != 0)
            {
                foreach (var child in root.Properties())
                {
                    if (child.Value.Type == JTokenType.Object)
                    {
                        RecursiveGenerateClassVariable(child, null, generatedClass);
                    } else
                    {
                        generatedClass.Variables.Add(GenerateVariable(child, true));
                    }
                }
            }
        }

        private void RecursiveGenerateClassVariable(JProperty child, Variable parent, IClassTemplate generatedClass)
        {
            if (child.Value.Type != JTokenType.Object)
            {
                return;
            }
            Variable classVariable = GenerateVariable(child, false);
            if (child.Count != 0)
            {
                JObject jObject = (JObject)child.Value;
                foreach (var innerChild in jObject.Properties())
                {
                    RecursiveGenerateClassVariable(innerChild, classVariable, generatedClass);
                    if (innerChild.Value.Type != JTokenType.Object)
                    {
                        classVariable.Variables.Add(GenerateVariable(innerChild, true));
                    }
                }
            }
            if (parent == null)
            {
                generatedClass.Variables.Add(classVariable);
                return;
            } 
            parent.Variables.Add(classVariable);
        }

        private Variable GenerateVariable(JProperty child, bool isPrimitive)
        {
            Variable variable = new Variable();
            variable.Visibility = "public";
            variable.Type = child.Value.Type.ToString();
            variable.Name = child.Name;
            variable.Value = child.Value.ToString();
            if (!isPrimitive)
            {
                variable.Variables = new List<Variable>();
                return variable;
            }
            variable.Variables = null;
            return variable;
        }
    }
}
