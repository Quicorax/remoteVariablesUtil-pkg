using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

namespace Services.Runtime.RemoteVariables
{
    public class RemoteVariablesService : IRemoteVariablesService
    {
        private readonly Dictionary<string, string> _remoteVariables = new();

        public RemoteVariablesService(TextAsset dependencies)
        {
            var serializedData = JsonUtility.FromJson<RemoteVariables>(dependencies.ToString());

            foreach (var remoteVariable in serializedData.data)
            {
                _remoteVariables.Add(remoteVariable.VariableKey, remoteVariable.Value);
            }
        }
        
        public string GetString(string variableKey) => Get(variableKey);
        public int GetInt(string variableKey) => int.Parse(Get(variableKey));
        public float GetFloat(string variableKey) => float.Parse(Get(variableKey));

        private string Get(string variableKey)
        {
            if (!_remoteVariables.ContainsKey(variableKey))
            {
                Debug.LogError($"Remote Variable with key {variableKey} is not defined!");
                return null;
            }

            return _remoteVariables[variableKey];
        }
    }
}
