using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    interface IClassTemplate
    {
        string PlainText { get; set; } 
        List<Variable> Variables { get; set; }

        string Type { get; set; }

        List<Method> Methods { get; set; }

        string Name { get; set; }

        string Format()
        {
            return PlainText;
        }

        void Generate(string className, string plainText);
    }
}
