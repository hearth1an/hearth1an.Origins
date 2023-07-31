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
        public void OnTriggerEnter(Collider hitCollider)
        {
            if (!hasInteracted && !isProxFixer)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
                {
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
