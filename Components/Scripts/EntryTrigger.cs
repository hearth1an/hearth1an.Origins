using UnityEngine;
using Origins.Utility;
using NewHorizons.Utility;

namespace Origins.Components.Scripts
{
    public class EntryTrigger : MonoBehaviour
    {
        public bool hasInteracted = false;
        public bool exitCheckAllowed = false;    
        public bool isProxFixer = false;
        public bool isLavaPuzzle = false;
        public bool isEnding = false;

        private ShipDamageController shipDmgController;
        private GameObject moltenCore;        
        private OWRigidbody shipBody;


        private void Start()
        {            
            moltenCore = SearchUtilities.Find("EnteranceDimension_Body/Sector/MoltenCore");
            shipDmgController = Locator.GetShipBody().GetComponent<ShipDamageController>();
            shipBody = Locator.GetShipBody();
        }

        private void TriggerEnding() => FindObjectOfType<TransformController>().InvokeEnding();

        private void AddVinesPhysics()
        {
            SearchUtilities.Find("DarkBramble_Body/ControlledByProxy_DB_Body/ControlledByProxy_DB/ShardPivot (2)").AddComponent<NewHorizons.Components.AddPhysics>().Mass = 1000;
            SearchUtilities.Find("DarkBramble_Body/ControlledByProxy_DB_Body/ControlledByProxy_DB/ShardPivot (9)").AddComponent<NewHorizons.Components.AddPhysics>().Mass = 1000;
        }
        public void OnTriggerEnter(Collider hitCollider)
        {
            if (!isProxFixer && !isLavaPuzzle && !isEnding && !hasInteracted)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
                {
                   // gameObject.GetComponent<BoxCollider>().enabled = true;

                    Locator.GetPlayerBody().GetComponent<PsionicInteractor>().Invoke("StartPsionicEvent", 3);

                    hasInteracted = true;
                    gameObject.SetActive(false);
                }
            }

            if (isProxFixer)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody || hitCollider.attachedRigidbody == Locator.GetShipBody()._rigidbody)
                {
                    SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Effects/InnerWarpFogSphere").SetActive(true);

                    if (isEnding)
                    {
                        SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Effects").SetActive(false);
                        SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Terrain_DB_BrambleSphere_Inner_v2/fogbackdrop_v2").SetActive(false);
                        SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node").GetComponent<InnerFogWarpVolume>()._warpRadius = 0f;
                       
                     
                        SearchUtilities.Find("DarkBramble_Body/FieldDetector_DB").SetActive(false);
                        SearchUtilities.Find("DarkBramble_Body/GravityWell_DB").SetActive(false);

                        SearchUtilities.Find("DarkBramble_Body/Sector_DB/Exit_Node").GetComponent<InnerFogWarpVolume>()._warpRadius = 0;

                       // Invoke("AddVinesPhysics", 1);
                        Invoke("TriggerEnding", 9f);
                    }
                }
            }

            if (isLavaPuzzle)
            {
                if (!shipDmgController._cockpitDetached)
                {
                    if (hitCollider.attachedRigidbody == Locator.GetShipBody()._rigidbody)
                    {
                        moltenCore.SetActive(false);
                        WriteUtil.WriteLine("Lava puzzle");
                        isLavaPuzzle = false;
                        PlayerEffectController.PlayAudioOneShot(AudioType.Splash_Lava, 1);
                        SearchUtilities.Find("PlayerHUD/HelmetOffUI/HelmetOffLockOn").SetActive(false);

                        shipDmgController.Explode();

                    }
                    
                }
                else
                {
                    if (hitCollider.attachedRigidbody != null)
                    {
                        if (hitCollider.attachedRigidbody == SearchUtilities.Find("Module_Cockpit_Body").GetComponent<OWRigidbody>()._rigidbody)
                        {
                            moltenCore.SetActive(false);
                            WriteUtil.WriteLine("Lava puzzle");
                            isLavaPuzzle = false;
                            PlayerEffectController.PlayAudioOneShot(AudioType.Splash_Lava, 1);
                            SearchUtilities.Find("PlayerHUD/HelmetOffUI/HelmetOffLockOn").SetActive(false);
                        }
                    }
                    
                }
            }



        }

        public void OnTriggerExit(Collider hitCollider)
        {

            if (exitCheckAllowed && !isProxFixer)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
                {
                    FindObjectOfType<TransformController>().InvokeEnding();
                }
            }

            if (isProxFixer)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody || hitCollider.attachedRigidbody == Locator.GetShipBody()._rigidbody)
                {
                    SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Effects/InnerWarpFogSphere").SetActive(false);
                }
            }

        }
        public string CheckEntryName()
        {
            return gameObject.name;
        }
    }
}
