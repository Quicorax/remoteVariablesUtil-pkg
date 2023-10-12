using System;
using System.Collections.Generic;

namespace Services.Runtime.RemoteVariables
{
    [Serializable]
    public class RemoteVariables
    {
        [Serializable]
        public class RemoteVariable
        {
            public string VariableKey;
            public string Type;
            public string Value;
        }

        public List<RemoteVariable> data = new();
    }
}