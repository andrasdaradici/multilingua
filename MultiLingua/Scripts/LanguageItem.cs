using TMPro;
using UnityEditor;
using UnityEngine;

namespace MultiLingua
{
    [CreateAssetMenu(fileName = "NewLanguageItem", menuName = "MultiLingua/Language Item")]
    public class LanguageItem : ScriptableObject
    {
        [Tooltip("Name of the language")]
        public string Name;
        [Tooltip("Font for the language (Japanese and Hindi f.e. may use different fonts due to having different characters")]
        public TMP_FontAsset LanguageFont;
        [Tooltip("Path to the language file (Example: \"/Resources/Locales/English\", the \"Resources\" folder is located in the \"Assets\" folder.")]
        public string LanguageFilePath;
        [Tooltip("Use XML File Type")]
        public bool XML;
        [Tooltip("Use JSON File Type")]
        public bool JSON;
        [Tooltip("Use YML File Type")]
        public bool YAML;
    }
}