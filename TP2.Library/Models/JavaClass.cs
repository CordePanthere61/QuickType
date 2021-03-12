using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    class JavaClass : IClassTemplate
    {
        public string PlainText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Variable> Variables { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Method> Methods { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Generate(string className, string plainText)
        {
            throw new NotImplementedException();
        }
    }
}
