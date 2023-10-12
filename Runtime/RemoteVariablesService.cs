using System.Collections.Generic;
using UnityEngine;

namespace Services.Runtime.RemoteVariables
{
    public class RemoteVariablesService : IRemoteVariablesService
    {
        private const string DataPath = "RemoteData/RemoteVariables";

        private readonly Dictionary<string, string> _remoteVariables = new();

        public RemoteVariablesService()
        {
            var serializedData = JsonUtility.FromJson<RemoteVariables>(FetchDependencies());

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

        private string FetchDependencies()
        {
            var dependencies = Resources.Load("RemoteVariables/RemoteData").ToString();

            if (dependencies == null)
            {
                Debug.LogError("No Remote Variables dependencies defined in the Resources folder!");
            }
            
            return dependencies;
        }
    }
}