using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Runtime.RemoteVariables
{
    public class RemoteVariablesUpdaterWindow : EditorWindow
    {
        private const string FrontURLKey = "FrontURLKey";
        private const string DataURLKey = "DataURLKey";

        private const int _windowHeight = 155;
        private const int _windowWidht = 400;

        private static string _frontURL;
        private static string _dataURL;

        [MenuItem("Quicorax/RemoteVariables Window")]
        public static void Init()
        {
            var window = GetWindow(typeof(RemoteVariablesUpdaterWindow), false, "Remote Variables");
            window.minSize = new Vector2(_windowWidht, _windowHeight);
            window.maxSize = new Vector2(_windowWidht, _windowHeight);

            if (EditorPrefs.HasKey(FrontURLKey))
            {
                _frontURL = EditorPrefs.GetString(FrontURLKey);
            }

            if (EditorPrefs.HasKey(DataURLKey))
            {
               _dataURL = EditorPrefs.GetString(DataURLKey);
            }
            
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Remote Variables URL:");
            GUILayout.BeginHorizontal();
            _frontURL = GUILayout.TextField(_frontURL, GUILayout.Height(28), GUILayout.Width(362),
                GUILayout.ExpandWidth(false));
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_UnityEditor.SceneView.png"),
                    GUILayout.Height(28), GUILayout.Width(28)))
            {
                System.Diagnostics.Process.Start(_frontURL);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.Label("Fetch Data URL:");
            _dataURL = GUILayout.TextField(_dataURL);
            GUILayout.Space(10);

            if (GUILayout.Button("Update Remote Variables", GUILayout.Height(40)))
            {
                SaveURLs();
                FetchData();
            }
        }

        private static void FetchData()
        {
            Debug.Log("UPDATING Remote Variables...");

            var request = new UnityWebRequest(_dataURL, "GET", new DownloadHandlerBuffer(), null);
            request.SendWebRequest().completed += _ =>
            {
                if (request.error != null)
                {
                    Debug.Log(request.error);
                    return;
                }

                Debug.Log("Remote Variables updated! -> " + request.downloadHandler.text);

                var remoteVariables =
                    JsonUtility.FromJson<RemoteVariables>(request.downloadHandler.text);

                File.WriteAllText(Application.dataPath + "/Resources/RemoteVariables/RemoteData.json",
                    JsonUtility.ToJson(remoteVariables));
                AssetDatabase.Refresh();
            };
        }
        
        private void SaveURLs()
        {
            EditorPrefs.SetString(FrontURLKey, _frontURL);
            EditorPrefs.SetString(DataURLKey, _dataURL);
        }
    }
}