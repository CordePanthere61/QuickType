using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    class Variable
    {
        public string Name { get; set; }
        //public string DataType { get; set; }
        public string Value { get; set; }
        public string Visibility { get; set; }
        public string Type { get; set; }
        public List<Variable> Variables { get; set; }
        public string Format()
        {
            return $"{Visibility} {Type} {Name} = {Value};\n";
        }
    }
}
