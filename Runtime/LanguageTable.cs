using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Com.A9.FileReader;
using UnityEditor;
using UnityEngine;

namespace Com.A9.Language
{
    [CreateAssetMenu(fileName = "LanguageTable", menuName = "LanguageTable")]
    public class LanguageTable : ScriptableObject
    {
        public List<LanguageTableItem> items = new List<LanguageTableItem>();

#if UNITY_EDITOR
        [ContextMenu("Read")]
        public void Read()
        {
            items = xmlReaderExtension.CreateArrayWithExcel<LanguageTableItem>(
                Application.dataPath + $"/Resources/GameData/Language.xlsx", "Common").ToList();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
