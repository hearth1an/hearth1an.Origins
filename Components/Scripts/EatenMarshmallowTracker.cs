using UnityEngine;
using Origins.Utility;
using NewHorizons.Handlers;
using NewHorizons.Utility;

namespace Origins.Components.Scripts
{
    public class EatenMarshmallowTracker: MonoBehaviour
    {
        private int marshmallowCount;
        private Campfire campfire;
        public bool threeMarshmallowsEaten;
        private AudioClip audioClip;

        private void Awake()
       {
            campfire = SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/Controller_Campfire").GetComponent<Campfire>();
            marshmallowCount = 0;
            audioClip = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/FadeOut.mp3");
            threeMarshmallowsEaten = false;
       }       

        private void Update ()
        {
            if (!threeMarshmallowsEaten && campfire._isPlayerRoasting)
            {
                if (marshmallowCount != 3)
                {
                    if (OWInput.IsNewlyPressed(InputLibrary.interact, InputMode.Roasting))
                    {
                        marshmallowCount++;
                        WriteUtil.WriteLine("Added marshmallow count");
                    }                  
                }

                else
                {
                    SearchUtilities.Find("CaveDimension_Body/Sector/Cave/SecondInteractionTrigger").SetActive(true);
                    WriteUtil.WriteLine("MarshmallowsEaten");                    
                    PlayerEffectController.PlayAudioExternalOneShot(audioClip, 1f);
                    threeMarshmallowsEaten = true;                   
                };
            }

        }
    }
}
