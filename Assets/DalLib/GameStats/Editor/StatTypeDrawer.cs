using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace DaleranGames.GameStats
{
    [CustomPropertyDrawer(typeof(StatType), false)]
    public class StatTypeDrawer : PropertyDrawer
    {
        string name;
        SerializedProperty statValue, statName, statIcon, statDescription;

        static List<StatType> statTypes = Enumeration.GetAll<StatType>().ToList();
        string[] statNames = GetNames(statTypes);
        int index;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

                name = property.displayName;
                statName = property.FindPropertyRelative("_name");
                statValue = property.FindPropertyRelative("_value");
                statIcon = property.FindPropertyRelative("_statIcon");
                statDescription = property.FindPropertyRelative("_description");
                for (int i = 0; i < statNames.Length; i++)
                {
                    if (statNames[i] == statName.stringValue)
                        index = i;
                }

            
            // Begin/end property & change check make each field
            // behave correctly when multi-object editing.
            EditorGUI.BeginProperty(position, label, property);
            {
                EditorGUI.BeginChangeCheck();
                if (label == GUIContent.none)
                    index = EditorGUI.Popup(position, index, statNames);
                else
                    index = EditorGUI.Popup(position, name, index, statNames);

                if (EditorGUI.EndChangeCheck())
                {
                    StatType selected = Enumeration.FromName<StatType>(statNames[index]);

                    statName.stringValue = selected.Name;
                    statValue.intValue = selected.Value;
                    statIcon.stringValue = selected.Icon;
                    statDescription.stringValue = selected.Description;
                }               
            }
            EditorGUI.EndProperty();
        }

        static string[] GetNames(List<StatType> types)
        {
            List<string> names = new List<string>();
            for (int i = 0; i < types.Count; i++)
            {
                names.Add(types[i].Name);
            }
            return names.ToArray();
        }
    }
}
