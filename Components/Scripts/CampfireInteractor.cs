using UnityEngine;
using NewHorizons.Utility;
using NewHorizons.Handlers;
using Origins.Components;
using Origins.Utility;

namespace Origins.Components.Scripts
{
    public class CampfireInteractor : MonoBehaviour
    {        
        private SingleInteractionVolume _interactVolume;
        // private InteractReceiver interactReceiver;

        private GameObject GreenFlame;
        private GameObject RedFlame;
        private GameObject YellowFlame;
        private Campfire campfire;
        private void Awake()
        {
            _interactVolume = this.GetRequiredComponent<SingleInteractionVolume>();
            _interactVolume._textID = UITextType.LightCampfirePrompt;
            
            _interactVolume.OnPressInteract += OnPressInteract;            

            campfire = SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/Controller_Campfire").GetComponent<Campfire>();            

           // interactReceiver = gameObject.GetComponent<InteractReceiver>();
        }

        private void Start()
        {            
            _interactVolume.DisableInteraction();            

            base.enabled = true;           

        }
        private void OnPressInteract()
        {
            GreenFlame = SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireGreen");
            RedFlame = SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireRed");
            YellowFlame = SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireYellow");

            if (gameObject != null)
            {
                if (GreenFlame.activeSelf)
                {
                    CopyColorFrom(GreenFlame);

                    WriteUtil.WriteError("Greem");                  

                }
                else if (RedFlame.activeSelf)
                {
                    CopyColorFrom(RedFlame);

                    WriteUtil.WriteError("Red");
                }
                else if (YellowFlame.activeSelf)
                {
                    CopyColorFrom(YellowFlame);

                    WriteUtil.WriteError("Yellow");
                }
                else
                {
                    WriteUtil.WriteError("Idk not working");
                }
            }          
            
        }

        private void CopyColorFrom(GameObject sourse)
        {
            campfire._flames.GetComponent<MeshRenderer>().materials[0].color = sourse.GetComponent<MeshRenderer>().materials[0].color;
            
            campfire.SetState(Campfire.State.LIT, true);
            
        }

    }
}
