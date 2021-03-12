﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Library.Models
{
    interface IClassTemplate
    {
        List<Variable> Variables
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }
    }
}
