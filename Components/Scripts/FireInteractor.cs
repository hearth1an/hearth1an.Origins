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
        private GameObject _greenFire;
        private GameObject _yellowFire;
        private GameObject _redFire;
        

        private void Awake()
        {
            _interactVolume = this.GetRequiredComponent<SingleInteractionVolume>();
            _interactVolume._textID = UITextType.LightCampfirePrompt;
            _interactVolume._resetInteractionTime = 16f;
            _interactVolume.OnPressInteract += OnPressInteract;
            _interactVolume.OnGainFocus += OnGainFocus;
            _campfireInteractReciever = SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/AttachPoint").GetComponent<InteractReceiver>();
            _campfire = SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/Controller_Campfire").GetComponent<Campfire>();
            _fireVaseItem = SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2").GetComponent<FireVaseItem>();

            _greenFire = SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2/FireVase_New/FireGreen");
            _yellowFire = SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2/FireVase_New/FireYellow");
            _redFire = SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2/FireVase_New/FireRed");

            interactReceiver = gameObject.GetComponent<InteractReceiver>();
            interactReceiver.DisableInteraction();            
            _interactVolume.DisableInteraction();

        }

        private void Start()
        {
            _campfire.SetState(Campfire.State.UNLIT, true);
            _interactVolume.DisableInteraction();
            
            _campfireInteractReciever.gameObject.SetActive(false);
            _campfireInteractReciever.OnPressInteract += CheckFireColor; 

            Invoke("ChangePromt", 1f);
        }

        private void OnGainFocus()
        {
            if (_fireVaseItem != null)
            {
                if (!_fireVaseItem.isPickedUp)
                {
                    _interactVolume.DisableInteraction();
                }
                else
                {
                    _interactVolume.EnableInteraction();
                }
            }
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
                        _greenFire.SetActive(true);

                    });

                    WriteUtil.WriteError("Green");

                }
                if (gameObject.name == "HornetRed")
                {
                    Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
                    {
                        _redFire.SetActive(true);
                    });

                    WriteUtil.WriteError("Red");
                }
                if (gameObject.name == "HornetYellow")
                {
                    Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
                    {
                        _yellowFire.SetActive(true);
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
            _greenFire.SetActive(false);
            _redFire.SetActive(false);
            _yellowFire.SetActive(false);

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
            

            if (gameObject != null)
            {
                if (_greenFire.activeSelf)
                {
                    CopyColorFrom(_greenFire);

                    WriteUtil.WriteError("Greem");

                }
                else if (_redFire.activeSelf)
                {
                    CopyColorFrom(_redFire);

                    WriteUtil.WriteError("Red");
                }
                else if (_yellowFire.activeSelf)
                {
                    CopyColorFrom(_yellowFire);

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
