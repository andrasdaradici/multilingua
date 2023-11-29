using MultiLingua;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LanguageItem))]
public class LanguageItemEditor : Editor
{
    bool HasAssignedName;
    bool HasAssignedPath;
    bool HasAssignedExtension;
    bool HasAssignedFont;

    LanguageItem script;

    public override void OnInspectorGUI()
    {
        script = (LanguageItem)target;
        CheckLanguageName();
        CheckPath();
        CheckExtension();
        CheckFont();
        Percentage();
        Documentation();
    }

    public void CheckLanguageName()
    {
        if (script.Name == "")
        {
            HasAssignedName = false;
        }
        else if (script.Name != null)
        {
            HasAssignedName = true;
        }
        script.Name = EditorGUILayout.TextField(new GUIContent("Language Name: ", "Example of name: English, French, German, etc."), script.Name);
        
        if (HasAssignedName) Separator();
    }


    public void CheckPath()
    {
        if (HasAssignedName is false) return;
        if(script.LanguageFilePath == "")
        {
            HasAssignedPath = false;
        }
        else if(script.LanguageFilePath != null) 
        { 
            HasAssignedPath = true;
        }
        script.LanguageFilePath = EditorGUILayout.TextField(new GUIContent("Language File Path: ", "Example of path: /Resources/Locales/English \n(NOTE: Do not add the file extension into the path)"), script.LanguageFilePath);
    }

    public void CheckExtension()
    {
        if (HasAssignedPath is false) return;
        
        if (script.JSON == false && script.XML == false && script.YAML == false)
        {
            script.XML = EditorGUILayout.Toggle("XML: ", script.XML);
            script.JSON = EditorGUILayout.Toggle("JSON: ", script.JSON);
            script.YAML = EditorGUILayout.Toggle("YAML: ", script.YAML);
        }
        HasAssignedExtension = false;
        if (script.XML == true)
        {
            script.XML = EditorGUILayout.Toggle("XML: ", script.XML); 
            HasAssignedExtension = true;
        }

        if (script.JSON == true)
        {
            script.JSON = EditorGUILayout.Toggle("JSON: ", script.JSON);
            HasAssignedExtension = true;
        }
        if (script.YAML == true)
        {
            script.YAML = EditorGUILayout.Toggle("YAML: ", script.YAML);
            HasAssignedExtension = true;
        }

        if (HasAssignedExtension == true) Separator();
    }

    public void CheckFont()
    {
        if (HasAssignedExtension is false) return;
        if (script.LanguageFont == null) HasAssignedFont = false;
        else if (script.LanguageFont != null) HasAssignedFont = true;
        script.LanguageFont = (TMP_FontAsset)EditorGUILayout.ObjectField(new GUIContent("Language Font: ", "The font used by the language"), script.LanguageFont, typeof(TMP_FontAsset), true);
        Separator();
    }

    public void Documentation()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.textColor = HexToColor("#FFFFFF");
        buttonStyle.hover.textColor = HexToColor("#C3C3C3");
        if (GUILayout.Button(new GUIContent("Documentation", "Stuck? Need in help? \nClick to view the Documentation."), buttonStyle))
        {
            Application.OpenURL("https://github.com/andrasdaradici/multilingua/wiki/01-Overview");
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

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    void Percentage()
    {
        int step = 0;
        if (HasAssignedName is false) step = 0;
        else if(HasAssignedName is true)
        {
            if (HasAssignedPath is false) step = 1;
            else if (HasAssignedPath is true)
            {
                if (HasAssignedExtension is false) step = 2;
                else if (HasAssignedExtension is true)
                {
                    if (HasAssignedFont is false) step = 3;
                    else if(HasAssignedFont is true) step = 4;
                }
            }
        }
        switch(step)
        {
            case 0:
                GUI.color = Color.red;
                EditorGUILayout.LabelField("File setup 0% completed.", EditorStyles.boldLabel);
                GUI.color = Color.white;
                break;
            case 1:
                GUI.color = Color.yellow;
                EditorGUILayout.LabelField("File setup 25% completed.", EditorStyles.boldLabel);
                GUI.color = Color.white;
                break;
            case 2:
                GUI.color = Color.yellow;
                EditorGUILayout.LabelField("File setup 50% completed.", EditorStyles.boldLabel);
                GUI.color = Color.white;
                break;
            case 3:
                GUI.color = Color.yellow;
                EditorGUILayout.LabelField("File setup 75% completed.", EditorStyles.boldLabel);
                GUI.color = Color.white;
                break;
            case 4:
                GUI.color = Color.green;
                EditorGUILayout.LabelField("File setup 100% completed.", EditorStyles.boldLabel);
                GUI.color = Color.white;
                break;
            default:
                GUI.color = Color.red;
                EditorGUILayout.LabelField("This should not appear.", EditorStyles.boldLabel);
                GUI.color = Color.white;
                break;
        }
    }
}
