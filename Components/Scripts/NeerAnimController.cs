using UnityEngine;
using Origins.Utility;
using Origins.Components;
using NewHorizons.Utility;


namespace Origins.Components.Scripts
{
    public class NeerAnimController : MonoBehaviour
    {
        Animator animator;
        private CharacterDialogueTree dialogueTree;
        private GameObject distortionEffect;
        private GameObject gatesReveal;
        private GameObject distortionSphere;
        private AudioSource neerAudioSource;
        private AudioClip kneeFoley;
        private AudioClip castSpell;

        public void Start()
        {
            animator = GetComponent<Animator>();
            dialogueTree = SearchUtilities.Find("HeartDimension_Body/Sector/heart/NeerPivot/Neer/Ch50/ConversationZone").GetComponent<CharacterDialogueTree>();

            dialogueTree._timeFrozen = false;
            distortionEffect = SearchUtilities.Find("HeartDimension_Body/Sector/heart/NeerPivot/CastSpell");
            gatesReveal = SearchUtilities.Find("HeartDimension_Body/Sector/heart/Gates");
            distortionSphere = SearchUtilities.Find("Player_Body/DistortionSphere");

            neerAudioSource = gameObject.GetComponent<AudioSource>();
            kneeFoley = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/FoleyKnee.mp3");
            castSpell = Origins.Instance.ModHelper.Assets.GetAudio("planets/Content/Audio/CastSpell.mp3");

            dialogueTree.OnStartConversation += OnDialogueStart;
            dialogueTree.OnEndConversation += OnDialogueEnd;
        }

        public void OnDialogueStart()
        {
            PlayerData._settingsSave.freezeTimeWhileReadingConversations = false;

            animator.SetBool("IsDialogueStarted", true);
            PlayerEffectController.Blink(0.5f);
            Invoke("AfterBlink", 0.5f);            
            neerAudioSource.PlayOneShot(kneeFoley);

            

            
        }

        private void OnDialogueEnd()
        {
            animator.SetBool("IsDialogueEnded", true);
            Invoke("StartDialogueEndingEvent", 0.5f);
            dialogueTree.gameObject.SetActive(false);
            PlayerEffectController.Blink(0.5f);
            //Invoke("AfterBlink", 0.f);
            AfterBlink();


        }

        private void AfterBlink()
        {
            if (animator != null)
            {
                if (animator.GetBool("IsDialogueEnded") == false)
                {
                    PlayerEffectController.PlayAudioOneShot(AudioType.SingularityOnPlayerEnterExit, 1f);
                    distortionSphere.transform.localScale = new Vector3(30, 30, 30);
                    distortionSphere.SetActive(true);
                }
                else
                {
                    PlayerEffectController.PlayAudioOneShot(AudioType.PlayerGasp_Medium, 1f);
                    distortionSphere.transform.localScale = new Vector3(5, 5, 5);
                    distortionSphere.SetActive(false);
                    neerAudioSource.PlayOneShot(castSpell);
                }
            }  
        }


        private void StartDialogueEndingEvent()
        {
            gatesReveal.SetActive(true);
            distortionEffect.SetActive(true);

        }
    }
}
