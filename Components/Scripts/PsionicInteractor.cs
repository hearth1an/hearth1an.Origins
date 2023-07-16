using UnityEngine;
using NewHorizons.Handlers;
using NewHorizons.Utility;
using Origins.Utility;


namespace Origins.Components.Scripts
{
    public class PsionicInteractor : MonoBehaviour
    {
        private HUDHelmetAnimator _animator;          
        private GameObject _canvasHUD_1;
        private GameObject _canvasHUD_2;
        private GameObject _canvasNotification;
        private GameObject _distortionSphere;

        private GameObject _firstInteraction;
        private GameObject _secondInteraction;
        private GameObject _thirdInteraction;
        private GameObject _fourthInteraction;
        private GameObject _fifthInteraction;
        private GameObject _sixthInteraction;

        private NotificationData errorNotification;
        private NotificationData restoreNotification;

        private NotificationData firstNotification;
        private NotificationData secondNotification;
        private NotificationData thirdNotification;
        private NotificationData fourthNotification;
        private NotificationData fifthNotification;
        private NotificationData sixthNotification;

        private AudioClip firstNotificationSFX;
        private AudioClip secondNotificationSFX;
        private AudioClip thirdNotificationSFX;
        private AudioClip fourthNotificationSFX;
        private AudioClip fifthNotificationSFX;
        private AudioClip sixthNotificationSFX;

        private AudioSignal signal;



        private void Awake()
        {
            _animator = FindObjectOfType<HUDHelmetAnimator>();           
            _canvasHUD_1 = SearchUtilities.Find("PlayerHUD/HelmetOnUI/UICanvas/GaugeGroup");
            _canvasHUD_2 = SearchUtilities.Find("PlayerHUD/HelmetOnUI/UICanvas/SecondaryGroup");
            _canvasNotification = SearchUtilities.Find("PlayerHUD/HelmetOnUI/UICanvas/Notifications");
            _distortionSphere = SearchUtilities.Find("Player_Body/DistortionSphere");

            signal = SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_1/Essence_Signal_1").GetComponent<AudioSignal>();

            _firstInteraction = SearchUtilities.Find("CaveDimension_Body/Sector/Cave/FirstInteractionTrigger");
            _secondInteraction = SearchUtilities.Find("CaveDimension_Body/Sector/Cave/SecondInteractionTrigger");
            _thirdInteraction = SearchUtilities.Find("CoreDimension_Body/Sector/Core/ThirdInteractionTrigger");
            _fourthInteraction = SearchUtilities.Find("CoreDimension_Body/Sector/Core/FourthInteractionTrigger");
            _fifthInteraction = SearchUtilities.Find("HeartDimension_Body/Sector/heart/FifthInteractionTrigger");
            _sixthInteraction = SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/SixthInteractionTrigger");

            firstNotificationSFX = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/FirstInteraction.mp3");
            secondNotificationSFX = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/SecondInteraction.mp3");
            thirdNotificationSFX = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/ThirdInteraction.mp3");
            fourthNotificationSFX = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/FourthInteraction.mp3");
            fifthNotificationSFX = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/FifthInteraction.mp3");
            sixthNotificationSFX = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/SixthInteraction.mp3");

            errorNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("SYSTEM_ERROR", TranslationHandler.TextType.UI), 1.5f, true);
            restoreNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("SYSTEM_RESTORED", TranslationHandler.TextType.UI), 8f, true);

            firstNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("FIRST_INTERACTION", TranslationHandler.TextType.UI), 7f, true);
            secondNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("SECOND_INTERACTION", TranslationHandler.TextType.UI), 7f, true);
            thirdNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("THIRD_INTERACTION", TranslationHandler.TextType.UI), 7f, true);
            fourthNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("FOURTH_INTERACTION", TranslationHandler.TextType.UI), 7f, true);
            fifthNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("FIFTH_INTERACTION", TranslationHandler.TextType.UI), 7f, true);
            sixthNotification = new NotificationData(NotificationTarget.Player, TranslationHandler.GetTranslation("SIXTH_INTERACTION", TranslationHandler.TextType.UI), 7f, true);

            // firstNotification = 

            base.enabled = true;
        }

        public void StartPsionicEvent()
        {
            PostErrorNotification();
            _animator._hudCrashLength = 1.5f;

            Invoke("PlayFadeIn", 1f);
            Invoke("DoBlink", 1.5f);
            Invoke("StartInteraction", 1.5f);
        }

        private void PlayFadeIn()
        {
            PlayerEffectController.PlayAudioOneShot(AudioType.SingularityOnPlayerEnterExit, 1f);
        }

        private void PlayFadeOut()
        {
            PlayerEffectController.PlayAudioOneShot(AudioType.SingularityOnObjectExit, 3f);
        }
        private void DoBlink()
        {
            PlayerEffectController.Blink(0.5f);
        }

        private void StartInteraction()
        {
            _distortionSphere.SetActive(true);

            StartHudAnimation();
           // Locator.GetActiveCamera().transform.Find("ScreenEffects/LightFlickerEffectBubble").GetComponent<LightFlickerController>().FlickerOffAndOn(5, 5);

            Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
            {
                if (_firstInteraction.GetComponent<EntryTrigger>().hasInteracted && !_secondInteraction.GetComponent<EntryTrigger>().hasInteracted)
                {
                    PostNotification(firstNotification, firstNotificationSFX);
                }
                if (_secondInteraction.GetComponent<EntryTrigger>().hasInteracted && !_thirdInteraction.GetComponent<EntryTrigger>().hasInteracted)
                {
                    PostNotification(secondNotification, secondNotificationSFX);
                    SearchUtilities.Find("DarkNestDimension_Body/Sector/DarkNest_To_Core_Node").SetActive(true);
                }
                if (_thirdInteraction.GetComponent<EntryTrigger>().hasInteracted && !_fourthInteraction.GetComponent<EntryTrigger>().hasInteracted)
                {                    
                    PostNotification(thirdNotification, thirdNotificationSFX);
                    signal.IdentifyFrequency();                    
                }
                if (_fourthInteraction.GetComponent<EntryTrigger>().hasInteracted && !_fifthInteraction.GetComponent<EntryTrigger>().hasInteracted)
                {
                    PostNotification(fourthNotification, fourthNotificationSFX);                   
                }
                if (_fifthInteraction.GetComponent<EntryTrigger>().hasInteracted &&  !_sixthInteraction.GetComponent<EntryTrigger>().hasInteracted)
                {
                    PostNotification(fifthNotification, fifthNotificationSFX);
                }
                if (_sixthInteraction.GetComponent<EntryTrigger>().hasInteracted && _fifthInteraction.GetComponent<EntryTrigger>().hasInteracted)
                {
                    PostNotification(sixthNotification, sixthNotificationSFX);
                    PlayerData.SetPersistentCondition("ORIGINS_DONE", true);
                }
            });

            
        }

        private void StartHudAnimation()
        {
            _canvasHUD_1.SetActive(false);
            _canvasHUD_2.SetActive(false);
            _canvasNotification.transform.localPosition = new Vector3(0, -100, 0);
            _canvasNotification.transform.localScale = new Vector3(2, 2.5f, 2);

            _animator._hudDamageWobble = 10;
            _animator._hudCrashLength = 999;
            _animator._hudCrashing = true;            
        }

        private void PostNotification(NotificationData data, AudioClip clip)
        {
           
            NotificationManager.SharedInstance.PostNotification(data);
            PlayerEffectController.PlayAudioExternalOneShot(clip, 1f);
            
            Invoke("EndPsionicEvent", 7f);
        }

        private void PostErrorNotification()
        {
            
            NotificationManager.SharedInstance.PostNotification(errorNotification);
            PlayerEffectController.PlayAudioOneShot(AudioType.ShipDamageElectricalFailure, 1);
        }

        private void PostRestoreNotification()
        {
            _animator._hudCrashing = false;
            NotificationManager.SharedInstance.PostNotification(restoreNotification);
            PlayerEffectController.PlayAudioOneShot(AudioType.TH_ZeroGTrainingAllRepaired, 1);
        }

        private void EndPsionicEvent()
        {
            Invoke("EndInteraction",0.5f);
            DoBlink();
            
        }
       

        private void EndInteraction()
        {
            _distortionSphere.SetActive(false);
            PlayerEffectController.PlayAudioOneShot(AudioType.PlayerGasp_Heavy, 0.5f);
            _canvasNotification.transform.localPosition = new Vector3(0, 295, 0);
            _canvasNotification.transform.localScale = new Vector3(1, 1.5f, 1);
            _canvasHUD_1.SetActive(true);
            _canvasHUD_2.SetActive(true);
            _animator._hudCrashLength = 999;
            _animator._hudCrashing = true;
            Invoke("PostRestoreNotification", 1f);
        }
    }
}
