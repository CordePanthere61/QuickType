using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    interface IClassTemplate
    {
        
        List<Variable> Variables { get; set; }


        string PlainText { get; set; }

        string Name { get; set; }
        string Type { get; set; }

        string Format()
        {
            return PlainText;
        }

        abstract void Generate(string className, string JsonText);

        abstract string FormatText();

        abstract string FormatClassName();

        abstract string FormatVariables();

        abstract string FormatPrimitiveVariables();

        abstract string FormatClassVariables(List<Variable> variables);

        abstract void RecursiveFindClassVariables(Variable variable, List<Variable> classVariables);

    }
}
