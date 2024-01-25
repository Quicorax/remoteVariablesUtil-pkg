using System.Collections.Generic;
using UnityEngine;
using System;

namespace Services.Runtime.RemoteVariables
{
    public class RemoteVariablesService : IRemoteVariablesService
    {
        private const string DataPath = "RemoteVariables/RemoteData";

        private readonly Dictionary<string, string> _remoteVariables = new();
        private bool _isReady;

        public RemoteVariablesServicea()
        {
            var dependencies = Resources.LoadAsync(DataPath);
            dependencies.completed += _ => SetDependencies(dependencies);
        }
        
        public string GetString(string variableKey) => IsReady(()=> Get(variableKey));
        public int GetInt(string variableKey) => IsReady(()=> int.Parse(Get(variableKey)));
        public float GetFloat(string variableKey) => IsReady(()=> float.Parse(Get(variableKey)));

        private string Get(string variableKey)
        {
            if (!_remoteVariables.ContainsKey(variableKey))
            {
                Debug.LogError($"Remote Variable with key {variableKey} is not defined!");
                return null;
            }

            return _remoteVariables[variableKey];
        }
        
        private void SetDependencies(ResourceRequest asset)
        {
            if (asset == null)
            {
                Debug.LogError("No Remote Variables dependencies defined in the Resources folder!");
            }
            
            var serializedData = JsonUtility.FromJson<RemoteVariables>(asset?.asset.ToString());

            foreach (var remoteVariable in serializedData.data)
            {
                _remoteVariables.Add(remoteVariable.VariableKey, remoteVariable.Value);
            }
            
            _isReady = true;
        }
        
        private T IsReady<T>(Func<T> onReady)
        {
            if (_isReady)
            {
                return onReady.Invoke();
            }
    
            Debug.LogWarning("AudioService is not ready. Skipped call");
    
            return default;
        }
    }
}