using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

namespace Services.Runtime.RemoteVariables
{
    public class RemoteVariablesService : IRemoteVariablesService
    {
        private readonly Dictionary<string, string> _remoteVariables = new();

        [Inject]
        public void Construct(TextAsset dependencies)
        {
            SetDependencies(dependencies);
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
        
        private void SetDependencies(TextAsset asset)
        {
            if (asset == null)
            {
                Debug.LogError("No Remote Variables dependencies defined in the Resources folder!");
            }
            
            var serializedData = JsonUtility.FromJson<RemoteVariables>(asset.ToString());

            foreach (var remoteVariable in serializedData.data)
            {
                _remoteVariables.Add(remoteVariable.VariableKey, remoteVariable.Value);
            }
        }
    }
}