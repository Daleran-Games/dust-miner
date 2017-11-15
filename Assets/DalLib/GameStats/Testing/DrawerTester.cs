using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.GameStats
{
    public class DrawerTester : MonoBehaviour
    {
        [SerializeField]
        StatType testStat;

        [Header("Modifier Tests")]
        [SerializeField]
        Modifier testMod;
        [SerializeField] bool showTypeName = true;
        [SerializeField] bool showIcon = true;
        [SerializeField] bool showDescription = false;
        [SerializeField] bool colorBasedOnValue = false;
        [SerializeField] bool withPlusSign = false;

        [ContextMenu("Print Stat")]
        void PrintStat()
        {
            Debug.Log(testStat.ToStringAll());
        }

        [ContextMenu("Print Modifier")]
        void PrintMod()
        {
            Debug.Log(testMod.ToDisplayString(showTypeName, showIcon, showDescription, colorBasedOnValue, withPlusSign));
        }
    }
}

