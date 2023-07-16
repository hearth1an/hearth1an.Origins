using UnityEngine;
using NewHorizons.Utility;
using NewHorizons.Handlers;
using Origins.Components;
using Origins.Utility;

namespace Origins.Components.Scripts
{
    public class FireInteractor : MonoBehaviour
    {        
        private SingleInteractionVolume _interactVolume;
        private InteractReceiver interactReceiver;
        private Campfire _campfire;
        private InteractReceiver _campfireInteractReciever;
        private FireVaseItem _fireVaseItem;
        

        private void Awake()
        {
            _interactVolume = this.GetRequiredComponent<SingleInteractionVolume>();
            _interactVolume._textID = UITextType.LightCampfirePrompt;
            _interactVolume._resetInteractionTime = 16f;
            _interactVolume.OnPressInteract += OnPressInteract;
            _campfireInteractReciever = SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/AttachPoint").GetComponent<InteractReceiver>();
            _campfire = SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/Controller_Campfire").GetComponent<Campfire>();
            _fireVaseItem = SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2").GetComponent<FireVaseItem>();            

            interactReceiver = gameObject.GetComponent<InteractReceiver>();
            interactReceiver.DisableInteraction();
        }

        private void Start()
        {
            _campfire.SetState(Campfire.State.UNLIT, true);
            _interactVolume.DisableInteraction();
            
            _campfireInteractReciever.gameObject.SetActive(false);
            _campfireInteractReciever.OnPressInteract += CheckFireColor;
           
            this.enabled = false;

            Invoke("ChangePromt", 1f);
        }
        
        private void OnPressInteract()
        {
            WriteUtil.WriteError("Interaction");
            if (gameObject != null)
            {
                if (gameObject.name == "HornetGreen")
                {
                    Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
                    {
                        SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireGreen").SetActive(true);

                    });

                    WriteUtil.WriteError("Green");

                }
                if (gameObject.name == "HornetRed")
                {
                    Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
                    {
                        SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireRed").SetActive(true);
                    });

                    WriteUtil.WriteError("Red");
                }
                if (gameObject.name == "HornetYellow")
                {
                    Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
                    {
                        SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireYellow").SetActive(true);
                    });

                    WriteUtil.WriteError("Yellow");
                }

            }

            _campfireInteractReciever.gameObject.SetActive(true);
            _campfireInteractReciever._interactRange = 4;
            _campfireInteractReciever.EnableInteraction();

            PlayerEffectController.PlayAudioOneShot(AudioType.Artifact_Light, 0.5f);
            PlayerEffectController.PlayAudioOneShot(AudioType.DreamFire_Crackling_Loop, 0.5f);

            Invoke("DisableFlames", 15f);
        }

        private void DisableFlames()
        {
            SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireGreen").SetActive(false);
            SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireYellow").SetActive(false);
            SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireRed").SetActive(false);

            if (_campfire.GetState() == Campfire.State.LIT)
            {
                _campfireInteractReciever.gameObject.SetActive(true);
                _campfireInteractReciever.EnableInteraction();

            }
            if(_campfire.GetState() == Campfire.State.UNLIT)
            {
                _campfireInteractReciever.gameObject.SetActive(false);
                _campfireInteractReciever.DisableInteraction();
            }
            
            Locator.GetPlayerAudioController()._oneShotExternalSource.Stop();
            PlayerEffectController.PlayAudioOneShot(AudioType.Artifact_Extinguish, 0.5f);
            
        }

        private void ChangePromt()
        {
            interactReceiver.SetPromptText(UITextType.ItemPickUpPrompt, TranslationHandler.GetTranslation("HORNET_INTERACTION", TranslationHandler.TextType.UI));
        }

        private void CheckFireColor()
        {
            var GreenFlame = SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireGreen");
            var RedFlame = SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireRed");
            var YellowFlame = SearchUtilities.Find("Player_Body/PlayerCamera/ItemCarryTool/ItemSocket/FireVase_New2/FireVase_New/FireYellow");

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
            _campfire._flames.GetComponent<MeshRenderer>().materials[0].color = sourse.GetComponent<MeshRenderer>().materials[0].color;
            _campfire.SetState(Campfire.State.LIT, true);
            WriteUtil.WriteLine($"Current flame color is {_campfire._flames.GetComponent<MeshRenderer>().materials[0].color}");
        }

    }
}
