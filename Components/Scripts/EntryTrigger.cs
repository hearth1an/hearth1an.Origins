using UnityEngine;
using Origins.Utility;
using NewHorizons.Utility;

namespace Origins.Components.Scripts
{
    public class EntryTrigger : MonoBehaviour
    {
        public bool hasInteracted = false;
        public bool exitCheckAllowed = false;
        public void OnTriggerEnter(Collider hitCollider)
        {
            if (!hasInteracted)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
                {
                    Locator.GetPlayerBody().GetComponent<PsionicInteractor>().Invoke("StartPsionicEvent", 3);


                    hasInteracted = true;
                    gameObject.SetActive(false);
                }
            }
            
        }

        public void OnTriggerExit(Collider hitCollider)
        {
            if (exitCheckAllowed)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
                {
                    FindObjectOfType<TransformController>().InvokeEnding();
                }
            }

        }
        public string CheckEntryName()
        {
            return gameObject.name;
        }
    }
}
