using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.Testing
{
    public class CSVTest : MonoBehaviour
    {
        [ContextMenu("Print Single Header CSV")]
        void PrintSingleHeader()
        {
            CSVData csv = new CSVData("Single", Application.dataPath + "/DalLib/Core/Testing/CSV/singleHeader.txt");
            csv.Print();
        }

        [ContextMenu("Print Multiple Header CSV")]
        void PrintMultipleHeader()
        {
            List<CSVData> csv = CSVData.ParseMultipleCSVSheet(new List<string>() {"Type1","Type2"}, Application.dataPath + "/DalLib/Core/Testing/CSV/multipleHeader.txt");

            foreach(CSVData dat in csv)
            {
                dat.Print();
            }
            
        }

    }
}

