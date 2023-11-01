using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEditor;
using MultiLingua;
using UnityEngine;
using System.Xml;
using System.IO;
using TMPro;
using System;

public class LoadLanguage : MonoBehaviour
{
    [HideInInspector] public string Version = "1.0.0";
    [HideInInspector] public string NewestVersion = "1.0.0";
    [HideInInspector] public string VersionCheckURL = "http://www.lobby.nhely.hu/Assets/MultiLingua/LatestVersion.txt";
    [HideInInspector] public bool NeedToUpdate;
    [HideInInspector] public TextMeshProUGUI TextToChangeLanguage;
    //"Assets/Resources/Locales/"
    [HideInInspector] public string PathToLanguageItems = "";
    [HideInInspector] public List<LanguageItem> Languages;
    //[HideInInspector] public int LanguageIndex = 0;
    private int langIndex;
    [HideInInspector]
    public int LanguageIndex
    {
        get { return langIndex; }
        set
        {
            if (langIndex != value)
            {
                langIndex = value;
                if (Languages[langIndex].XML) ReadValueXML();
                if (Languages[langIndex].JSON) ReadValueJSON();
            }
        }
    }
    [HideInInspector] public string Node;
    [HideInInspector] public string Element;
    [HideInInspector] public string ErrorLoad;
    [HideInInspector] public int LoadErrorCode;
    [HideInInspector] public string ErrorRead;
    [HideInInspector] public int ReadErrorCode;

    public void CheckUpdate()
    {
        StartCoroutine(CheckVersion());
    }

    public IEnumerator CheckVersion()
    {
        UnityWebRequest www = UnityWebRequest.Get(VersionCheckURL);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;
        if (Version == data) NeedToUpdate = false;
        else
        {
            NeedToUpdate = true;
            NewestVersion = data;
        }
    }

    void Start()
    {
        LoadLanguages();
        if (LanguageIndex != PlayerPrefs.GetInt("CurrentLanguage")) LanguageIndex = PlayerPrefs.GetInt("CurrentLanguage");
        if (Languages[langIndex].XML) ReadValueXML();
        if (Languages[langIndex].JSON) ReadValueJSON();
    }

    private void Update()
    {
        if(LanguageIndex != PlayerPrefs.GetInt("CurrentLanguage")) LanguageIndex = PlayerPrefs.GetInt("CurrentLanguage");
        if (LanguageIndex < 0) LanguageIndex = 0;
        if (LanguageIndex >= Languages.Count) LanguageIndex = Languages.Count-1;
    }

    public void LoadLanguages()
    {
        ErrorLoad = "";
        if(Languages.Count >=1)Languages.Clear();
        string[] guids = AssetDatabase.FindAssets("t:LanguageItem", new[] { PathToLanguageItems });
        if (guids.Length == 0)
        {
            ErrorLoad = "Path is invalid";
            LoadErrorCode = 1;
            Debug.LogError("No LanguageItem assets found at path: " + PathToLanguageItems);
        }
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            LanguageItem languageItem = AssetDatabase.LoadAssetAtPath<LanguageItem>(assetPath);
            if (languageItem != null)
            {
                Languages.Add(languageItem);
                ErrorLoad = "Succesfully loaded " + guids.Length + " Language Item files.";
                LoadErrorCode = 0;
            }
        }
    }

    void ReadValueXML()
    {
        ErrorRead = "";
        if (LanguageIndex >= 0 && LanguageIndex < Languages.Count)
        {
            LanguageItem languageItem = Languages[LanguageIndex];
            string xmlFilePath = languageItem.LanguageFilePath + ".xml";

            TextAsset xmlFile = AssetDatabase.LoadAssetAtPath<TextAsset>(xmlFilePath);
            if (xmlFile != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlFile.text);
                XmlNode root = xmlDoc.SelectSingleNode("/Root/" + Node);

                if (root != null)
                {
                    XmlNode elementNode = root.SelectSingleNode(Element);

                    if (elementNode != null && !string.IsNullOrEmpty(elementNode.InnerText))
                    {
                        string value = elementNode.InnerText;
                        TextToChangeLanguage.text = value;
                        ErrorRead = "Successfully read data";
                        ReadErrorCode = 0;
                    }
                    else
                    {
                        ErrorRead = "Invalid or missing element: \"" + Element + "\"";
                        ReadErrorCode = 4;
                    }
                }
                else
                {
                    ErrorRead = "Invalid or missing node: \"" + Node + "\"";
                    ReadErrorCode = 3;
                }
                /*XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlFile.text);
                XmlNodeList nodes = xmlDoc.SelectNodes("/Root/" + Node);

                foreach (XmlNode node in nodes)
                {
                    string value = node.SelectSingleNode(Element).InnerText;
                    TextToChangeLanguage.text = value;
                    ErrorRead = "Succesfully read data";
                    ReadErrorCode = 0;
                }*/
            }
            else
            {
                ErrorRead = "XML file path is invalid, check the path in the Language Item at index " + LanguageIndex.ToString();
                ReadErrorCode = 2;
            }
        }
        else
        {
            ErrorRead = "Invalid Language Index";
            ReadErrorCode = 1;
        }
    }

    void ReadValueJSON()
    {
        ErrorRead = "";
        try
        {
            LanguageItem languageItem = Languages[LanguageIndex];
            string jsonFilePath = languageItem.LanguageFilePath + ".json";
            if (File.Exists(jsonFilePath))
            {
                string jsonFile = File.ReadAllText(jsonFilePath);
                JObject jsonData = JObject.Parse(jsonFile);
                string path = "Root." + Node + "." + Element;
                string text = GetTextAtPath(jsonData, path);
                if (text == "") return;
                TextToChangeLanguage.text = text;
            }
            else
            {
                ErrorRead = "JSON file not found at path: " + jsonFilePath;
                ReadErrorCode = 2;
            }
        }
        catch (Exception ex)
        {
            ErrorRead = "Error reading JSON data: " + ex.Message;
            ReadErrorCode = 1;
        }
    }

    string GetTextAtPath(JObject jsonData, string path)
    {
        try
        {
            JToken token = jsonData.SelectToken(path);
            if (token != null)
            {
                ErrorRead = "Successfully read data";
                ReadErrorCode = 0;
                return token.ToString();
            }
            else
            {
                ErrorRead = "Text not found at path: " + path;
                ReadErrorCode = 3;
                return "";
            }
        }
        catch (Exception ex)
        {
            ErrorRead = "Error accessing JSON data: " + ex.ToString();
            ReadErrorCode = 4;
            return "";
        }
    }

    public void SetLanguageIndex(int newIndex)
    {
        PlayerPrefs.SetInt("CurrentLanguage", newIndex);
    }
}
