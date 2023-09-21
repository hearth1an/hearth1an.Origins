using UnityEngine;
using NewHorizons.Utility;
using Origins.Utility;

namespace Origins.Components.Scripts
{
    public class EssenceTrigger : MonoBehaviour
    {
       
        public bool hasInteracted = false;
        private EssenceTrigger triggerOne;
        private EssenceTrigger triggerTwo;
        private EssenceTrigger triggerThree;

        private GameObject signalOne;
        private GameObject signalTwo;
        private GameObject signalThree;

        private AudioClip sfx;

        private void Start()
        {
            sfx = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/FadeOut.mp3");
            triggerOne = SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_1").GetComponent<EssenceTrigger>();
            triggerTwo = SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_2").GetComponent<EssenceTrigger>();
            triggerThree = SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_3").GetComponent<EssenceTrigger>();

            signalOne = SearchUtilities.Find("CoreDimension_Body/Sector/Essence_Signal_1");
            signalTwo = SearchUtilities.Find("CoreDimension_Body/Sector/Essence_Signal_2");
            signalThree = SearchUtilities.Find("CoreDimension_Body/Sector/Essence_Signal_3");


            WriteUtil.WriteLine("Added Essence tracker");
        }
        public void OnTriggerEnter(Collider hitCollider)
        {
            if (!hasInteracted)
            {
                if (hitCollider.attachedRigidbody == Locator.GetPlayerBody()._rigidbody)
                {
                    Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
                    {
                        EssenceEvent();
                       
                    });
                    
                    hasInteracted = true;
                    WriteUtil.WriteLine("Object hit. Object name:" + gameObject.name);
                    Invoke("ValidateAllDone", 1);
                }
            }

        }

        private void DisableSignal()
        {
            if (signalOne != null)
            {
                if (triggerOne.hasInteracted && signalOne.activeSelf)
                {
                    signalOne.SetActive(false);
                }
                if (triggerTwo.hasInteracted && signalTwo.activeSelf)
                {
                    signalTwo.SetActive(false);
                }
                if (triggerThree.hasInteracted && signalThree.activeSelf)
                {
                    signalThree.SetActive(false);
                }
            }
        }

        private void ValidateAllDone()
        {
            if (gameObject != null)
            {
                if (triggerOne.hasInteracted && triggerTwo.hasInteracted && triggerThree.hasInteracted)
                {
                    SearchUtilities.Find("Hazard_Core").SetActive(false);
                    SearchUtilities.Find("CoreDimension_Body/Sector/Core/FourthInteractionTrigger").SetActive(true);
                    SearchUtilities.Find("CoreDimension_Body/Sector/Core/FourthInteractionTrigger").GetComponent<SphereCollider>().enabled = true;
                }
                else
                {
                    WriteUtil.WriteLine("Not all essence collected yet");
                }
            }
        }

        private void EssenceEvent()
        {
            gameObject.GetComponent<Animator>().SetBool("isTriggered", true);           
            PlayerEffectController.PlayAudioExternalOneShot(sfx, 3f);
            PlayerEffectController.PlayAudioOneShot(AudioType.Sun_Explosion, 1);
            Invoke("DisableEssence", 5f);
            DisableSignal();
        }
        private void DisableEssence()
        {
            gameObject.SetActive(false);
            WriteUtil.WriteLine("Disabling the object");
        }
        public string CheckEntryName()
        {
            return gameObject.name;
        }
    }
}
