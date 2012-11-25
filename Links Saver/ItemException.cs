using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AYarkov.LinksSaver
{
    public class ItemException : Exception
    {
        public ItemException() {}
        public ItemException(string message) : base(message) { }
    }
}
