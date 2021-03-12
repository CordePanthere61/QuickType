using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    class CsharpClass : IClassTemplate
    {
        public string Name { get; set; }
        public List<Variable> Variables { get; set; }

        private JsonReader _reader;

        public CsharpClass(MainViewModel vm)
        {
            _reader = new JsonReader(vm.JsonText);
            Name = vm.ClassName;
            Variables = _reader.GetVariables();
        }
    }
}
