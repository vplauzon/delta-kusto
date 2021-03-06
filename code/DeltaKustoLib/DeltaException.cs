﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DeltaKustoLib
{
    public class DeltaException : Exception
    {
        public string Script { get; }

        public DeltaException(string message, Exception? innerException = null)
            : this(message, string.Empty, innerException)
        {
        }

        public DeltaException(string message, string script, Exception? innerException = null)
            : base(message, innerException)
        {
            Script = script;
        }
    }
}