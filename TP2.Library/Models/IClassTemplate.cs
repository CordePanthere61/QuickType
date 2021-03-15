using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    interface IClassTemplate
    {
        
        List<Variable> Variables { get; set; }

        List<Method> Methods { get; set; }
        string PlainText { get; set; }

        string Name { get; set; }
        string Type { get; set; }

        string Format()
        {
            return PlainText;
        }
        abstract void Generate(string className, string plainText);

        abstract string FormatText();

        abstract string FormatClassName();

        abstract string FormatVariables();
    }
}
