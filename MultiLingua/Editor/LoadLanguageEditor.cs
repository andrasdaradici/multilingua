using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.Net;

[CustomEditor(typeof(LoadLanguage))]
public class LoadLanguageEditor : Editor
{
    SerializedProperty Languages;

    bool HasAssignedText;
    bool HasAssignedPath;

    LoadLanguage script;
    void OnEnable()
    {
        Languages = serializedObject.FindProperty("Languages");
    }

    public bool IsConnectedToInternet()
    {
        try
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply pingReply = ping.Send("8.8.8.8", 2000);

            if (pingReply != null && pingReply.Status == IPStatus.Success)
            {
                return true;
            }
        }
        catch (SocketException)
        {

        }

        return false;
    }

    public override void OnInspectorGUI()
    {
        script = (LoadLanguage)target;
        CheckVersion();
        CheckAssignmentOfText();
        CheckAssignmentOfPath();
        DrawVariables();
    }

    void CheckVersion()
    {
        if (IsConnectedToInternet())
        {

            script.CheckUpdate();
            if (script.NeedToUpdate)
            {
                EditorGUILayout.LabelField("Current version: " + script.Version, EditorStyles.boldLabel);
                GUI.color = Color.green;
                EditorGUILayout.LabelField("Update available, newest version: " + script.NewestVersion, EditorStyles.boldLabel);
                GUI.color = Color.white;
            }
            else
            {
                EditorGUILayout.LabelField("Current version: " + script.Version, EditorStyles.boldLabel);
            }
        }
        else if (IsConnectedToInternet() == false)
        {
            GUI.color = Color.yellow;
            EditorGUILayout.LabelField(new GUIContent("Current version: " + script.Version, "You are not connected to the internet so the asset cannot check for any updates!"), EditorStyles.boldLabel);
            GUI.color = Color.white;
        }
    }

    void Separator()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        Rect lineRect = EditorGUILayout.GetControlRect(false, 3);
        EditorGUI.DrawRect(lineRect, Color.gray);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }

    void CheckAssignmentOfText()
    {
        if (script.TextToChangeLanguage == null)
        {
            GUI.color = Color.red;
            EditorGUILayout.LabelField("No Text Mesh Pro text assigned", EditorStyles.boldLabel);
            GUI.color = Color.white;
            HasAssignedText = false;
            
        }
        else if (script.TextToChangeLanguage != null)
        {
            HasAssignedText = true;
        }
        script.TextToChangeLanguage = (TextMeshProUGUI)EditorGUILayout.ObjectField("Text object", script.TextToChangeLanguage, typeof(TextMeshProUGUI), true);
    }

    void CheckAssignmentOfPath()
    {
        if (HasAssignedText is false) return;

        if (script.PathToLanguageItems == "")
        {
            GUI.color = Color.red;
            EditorGUILayout.LabelField("No Language Item path assigned", EditorStyles.boldLabel);
            GUI.color = Color.white;
            HasAssignedPath = false;

        }
        else if (script.PathToLanguageItems != "")
        {
            HasAssignedPath = true;
        }
        script.PathToLanguageItems = EditorGUILayout.TextField(new GUIContent("Path to Language Items: ", "Example of path: Assets/Resources/Locales/"), script.PathToLanguageItems);
    }

    void DrawVariables()
    {
        if (HasAssignedPath is false) return;
        Separator();
        if(EditorApplication.isPlaying) EditorGUILayout.LabelField("Error:", EditorStyles.boldLabel);
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.textColor = HexToColor("#FFFFFF");
        buttonStyle.hover.textColor = HexToColor("#C3C3C3");
        HandleLoadError();
        if (GUILayout.Button("Load language files", buttonStyle))
        {
            script.LoadLanguages();
        }
        EditorGUILayout.PropertyField(Languages, true);
        string CurrentLanguage = "";
        if (EditorApplication.isPlaying)
        {
            CurrentLanguage = script.Languages[script.LanguageIndex].Name;
            EditorGUILayout.LabelField("Language index: " + script.LanguageIndex.ToString() + " (" + CurrentLanguage + ")", EditorStyles.boldLabel);
        } 
        serializedObject.ApplyModifiedProperties();
        if(script.Languages.Count >= 1)
        {
            script.Node = EditorGUILayout.TextField(new GUIContent("Node", "\"Menu\" f.e. (see the provided files in the examples)"), script.Node);
            script.Element = EditorGUILayout.TextField(new GUIContent("Node element", "\"GameTitle\" f.e. (see the provided files in the examples)"), script.Element);
            HandleReadError();
        }
        if (GUILayout.Button(new GUIContent("Documentation", "Stuck? Need in help? \nClick to view the Documentation."), buttonStyle))
        {
            Application.OpenURL("https://github.com/andrasdaradici/multilingua/wiki/01-Overview");
        }
    }

    void HandleLoadError()
    {
        if (script.ErrorLoad != "")
        {
            switch (script.LoadErrorCode)
            {
                case 0:
                    GUI.color = HexToColor("#00BD0D");
                    EditorGUILayout.LabelField(script.ErrorLoad, EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
                case 1:
                    GUI.color = HexToColor("#D90019");
                    EditorGUILayout.LabelField(script.ErrorLoad, EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
                default:
                    GUI.color = HexToColor("#D90019");
                    EditorGUILayout.LabelField("This error shouldn't show up.", EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
            }
        }
    }

    void HandleReadError()
    {
        if (script.ErrorRead != "")
        {
            switch (script.ReadErrorCode)
            {
                case 0:
                    GUI.color = HexToColor("#00BD0D");
                    EditorGUILayout.LabelField(script.ErrorRead, EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
                case 1:
                    GUI.color = HexToColor("#D90019");
                    EditorGUILayout.LabelField(script.ErrorRead, EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
                case 2:
                    GUI.color = HexToColor("#D90019");
                    EditorGUILayout.LabelField(script.ErrorRead, EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
                case 3:
                    GUI.color = HexToColor("#D90019");
                    EditorGUILayout.LabelField(script.ErrorRead, EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
                case 4:
                    GUI.color = HexToColor("#D90019");
                    EditorGUILayout.LabelField(script.ErrorRead, EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
                default:
                    GUI.color = HexToColor("#D90019");
                    EditorGUILayout.LabelField("This error shouldn't show up.", EditorStyles.boldLabel);
                    GUI.color = Color.white;
                    break;
            }
        }
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
