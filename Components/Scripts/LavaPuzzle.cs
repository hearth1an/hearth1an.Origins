using NewHorizons.Utility;
using Origins.Utility;
using UnityEngine;

namespace Origins.Components.Scripts
{
    internal class LavaPuzzle: MonoBehaviour   
        
    {
        private OWRigidbody shipBody;
        private ShipDamageController shipDmgController;
        private GameObject moltenCore;
        private bool isPuzzleSolved = false;

        public void Start()
        {            
            moltenCore = SearchUtilities.Find("EnteranceDimension_Body/Sector/MoltenCore");
            shipBody = Locator.GetShipBody();
            shipDmgController = Locator.GetShipBody().GetComponent<ShipDamageController>();

            moltenCore.transform.localPosition = new Vector3(16.1794f, -599.5986f, 20.0453f);
        }

        private void Update()
        {     
            if (moltenCore != null && !isPuzzleSolved)
            {
                if (shipDmgController._cockpitDetached && !SearchUtilities.Find("Module_Cockpit_Body").activeSelf && moltenCore.activeSelf  )
                {
                    moltenCore.SetActive(false);
                    WriteUtil.WriteLine("Lava puzzle");
                    isPuzzleSolved=true;
                    PlayerEffectController.PlayAudioOneShot(AudioType.Splash_Lava, 1);
                    SearchUtilities.Find("PlayerHUD/HelmetOffUI/HelmetOffLockOn").SetActive(false);

                }
            }
        }
    }
}
