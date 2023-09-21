using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using NewHorizons.Utility;
using Origins.Utility;

namespace Origins.Components.Scripts
{
    public class QuantumMoonFixer : MonoBehaviour
    {
        private GameObject modThingie;
        private GameObject dbStateObject;
        private bool isActiveState;
       

        public void Awake()
        {
            modThingie = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_DB/Interactables_DBState/Node_QM");
            dbStateObject = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_DB");
            modThingie.SetActive(false);

            WriteUtil.WriteLine("QM fixer added");

        }

        private void Update()
        {
            if (modThingie != null)
            {
                if (dbStateObject.activeSelf)
                {
                    isActiveState = true;
                    if (isActiveState && !modThingie.activeSelf)
                    {
                        WriteUtil.WriteLine("DB state of QM active, thing enabled");
                        modThingie.SetActive(true);
                       
                    }                   
                }
                if (!dbStateObject.activeSelf)
                {
                    isActiveState = false;
                    if (!isActiveState && modThingie.activeSelf)
                    {
                        WriteUtil.WriteLine("DB state of QM disabled, thing disabled");
                        modThingie.SetActive(false);
                       
                    }                   
                }

            }

        }
    }
}
