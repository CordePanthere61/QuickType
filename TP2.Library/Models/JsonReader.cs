using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

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
    }
}
