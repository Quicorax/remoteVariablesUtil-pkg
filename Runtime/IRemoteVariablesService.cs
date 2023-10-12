namespace Services.Runtime.RemoteVariables
{
    public interface IRemoteVariablesService
    {
        string GetString(string variableKey);
        int GetInt(string variableKey);
        float GetFloat(string variableKey) ;
    }
}