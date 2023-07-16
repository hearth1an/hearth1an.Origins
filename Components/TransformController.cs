using NewHorizons.Utility;
using Origins.Utility;
using Origins.Components.Scripts;
using UnityEngine;

namespace Origins.Components
{
    public class TransformController : MonoBehaviour
    {

        public void TransformThings()
        {
           DoTransform();
           AddComponents();
          // Invoke("AddPhysics", 3f);
        }

        private void MakeFogDarker(string spherePath)
        {
            SearchUtilities.Find(spherePath).GetComponent<PlanetaryFogController>()._fogColorRampIntensity = -10f;
        }

        public void DoTransform()
        {
            var signal = SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_DB/Interactables_DBState/Node_QM/Escape_Pod_QM").GetComponent<AudioSignal>()._frequency;
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node").SetActive(false);

            if (PlayerData.GetPersistentCondition("ENTRY_DISCOVERED") == true)
            {
                DarkBrambleTransform();
                SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node").SetActive(true);
            }
            else
            {
                Origins.Instance.ModHelper.Events.Unity.RunWhen(() => PlayerData.KnowsFrequency(frequency: signal), () =>
                {
                    DarkBrambleTransform();
                    PlayerData.SetPersistentCondition("ENTRY_DISCOVERED", true);
                    SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node").SetActive(true);
                });
            }
           
           
            // custom item
            var fireVaseNew = SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2");
            fireVaseNew.DestroyAllComponents<SharedStone>();
            fireVaseNew.AddComponent<FireVaseItem>();
            SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New").transform.parent = SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2/AnimRoot").transform.parent;
            SearchUtilities.Find("CaveDimension_Body/Sector/FireVase_New2/AnimRoot").SetActive(false);

            // custom item 2
            var heartItem = SearchUtilities.Find("HeartDimensionSmall_Body/Sector/HeartItem_Stone");
            heartItem.DestroyAllComponents<SharedStone>();
            heartItem.AddComponent<HeartItem>();
            heartItem.GetComponent<SphereCollider>().radius = 6;
            SearchUtilities.Find("HeartDimensionSmall_Body/Sector/HeartItem").transform.parent = SearchUtilities.Find("HeartDimensionSmall/Sector/HeartItem_Stone/AnimRoot").transform.parent;
            SearchUtilities.Find("HeartDimensionSmall_Body/Sector/HeartItem_Stone/AnimRoot").SetActive(false);
            var artifact = SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/Artifact");
            artifact.SetActive(false);
            heartItem.transform.position = artifact.transform.position;
            SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/DirectionalLight").SetActive(false);
            SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/NeerPivot/CastSpell").SetActive(true);
            SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/NeerPivot/CastSpell").transform.localPosition = new Vector3 (10,10,10);

            SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/Props_HEA_Campfire/Campfire_Flames").transform.localScale = new Vector3(2, 2, 2);
            

            var node = SearchUtilities.Find("HeartDimension_Body/Sector/Heart_To_HeartSmall_Node");
            var portal = SearchUtilities.Find("HeartDimension_Body/Sector/heart/Gates/pCube5");
            node.transform.parent = portal.transform.parent;
            node.GetComponent<InnerFogWarpVolume>()._warpRadius = 10;
            node.GetComponent<InnerFogWarpVolume>()._exitRadius = 10;
            node.transform.localPosition = new Vector3(0, 0.018f, 0);
            node.transform.localScale = new Vector3 (0001f, 0001f, 0001f);
            WriteUtil.WriteLine("Cave Dimension setup completed.");

            SearchUtilities.Find("HeartDimensionSmall_Body/Sector").AddComponent<SmoothMinimiser>();

            SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_1").AddComponent<EssenceTrigger>();
            SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_2").AddComponent<EssenceTrigger>();
            SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_3").AddComponent<EssenceTrigger>();

            var deadNomai_1 = SearchUtilities.Find("CoreDimension_Body/Sector/Dead_Nomai_1");
            var deadNomai_2 = SearchUtilities.Find("CoreDimension_Body/Sector/Dead_Nomai_2");
            deadNomai_1.AddComponent<InteractReceiver>().SetPromptText(UITextType.RefillPrompt_0);
            deadNomai_1.AddComponent<PlayerRecoveryPoint>();
            deadNomai_2.AddComponent<InteractReceiver>().SetPromptText(UITextType.RefillPrompt_0);
            deadNomai_2.AddComponent<PlayerRecoveryPoint>();

            SearchUtilities.Find("EnteranceDimension_Body/Sector/MoltenCore").transform.localPosition = new Vector3(24.345f, -623.6233f, 35.4008f);

            SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/HazardVolume").GetComponent<HazardVolume>()._damagePerSecond = 1;

            /*
            var essenceSignal_1 = SearchUtilities.Find("CoreDimension_Body/Sector/Essence_Signal_1");
            var essenceSignal_2 = SearchUtilities.Find("CoreDimension_Body/Sector/Essence_Signal_2");
            var essenceSignal_3 = SearchUtilities.Find("CoreDimension_Body/Sector/Essence_Signal_3");

            essenceSignal_1.transform.parent = SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_1").transform.parent;
            essenceSignal_2.transform.parent = SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_2").transform.parent;
            essenceSignal_3.transform.parent = SearchUtilities.Find("CoreDimension_Body/Sector/EssenceObject_3").transform.parent;

            essenceSignal_1.transform.localPosition = new Vector3(0, 0, 0);
            essenceSignal_2.transform.localPosition = new Vector3(0, 0, 0);
            essenceSignal_3.transform.localPosition = new Vector3(0, 0, 0);

            essenceSignal_1.transform.localScale = new Vector3(1, 1, 1);
            essenceSignal_2.transform.localScale = new Vector3(1, 1, 1);
            essenceSignal_3.transform.localScale = new Vector3(1, 1, 1);
            */

            SearchUtilities.Find("HeartDimensionSmall_Body/Sector/BrambleCrash_SFX").SetActive(false);
            SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_DB/Interactables_DBState/Node_QM/Essence_Signal_1").SetActive(false);
            SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_DB/Interactables_DBState/Node_QM/Essence_Signal_2").SetActive(false);
            SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_DB/Interactables_DBState/Node_QM/Essence_Signal_3").SetActive(false);
            SearchUtilities.Find("QuantumMoon_Body/Sector_QuantumMoon/State_DB/Interactables_DBState/Node_QM/Escape_Pod_Signal").SetActive(false);
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Essence_Signal_1").SetActive(false);
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Essence_Signal_2").SetActive(false);
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Essence_Signal_3").SetActive(false);
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Escape_Pod_Signal").SetActive(false);

            SearchUtilities.Find("EnteranceDimension_Body/Sector/To_DarkNest/Essence_Signal_1").SetActive(false);
            SearchUtilities.Find("EnteranceDimension_Body/Sector/To_DarkNest/Essence_Signal_2").SetActive(false);
            SearchUtilities.Find("EnteranceDimension_Body/Sector/To_DarkNest/Essence_Signal_3").SetActive(false);

            SearchUtilities.Find("DarkNestDimension_Body/Sector/DarkNest_To_Core_Node/Essence_Signal_1").SetActive(false);
            SearchUtilities.Find("DarkNestDimension_Body/Sector/DarkNest_To_Core_Node/Essence_Signal_2").SetActive(false);
            SearchUtilities.Find("DarkNestDimension_Body/Sector/DarkNest_To_Core_Node/Essence_Signal_3").SetActive(false);



        }

        public void AddComponents()
        {
            SearchUtilities.Find("CaveDimension_Body/Sector/Cave/HornetYellow").AddComponent<FireInteractor>();
            SearchUtilities.Find("CaveDimension_Body/Sector/Cave/HornetRed").AddComponent<FireInteractor>();
            SearchUtilities.Find("CaveDimension_Body/Sector/Cave/HornetGreen").AddComponent<FireInteractor>();

            Locator.GetPlayerCamera().gameObject.AddComponent<CameraEffect>();
            SearchUtilities.Find("Player_Body").AddComponent<PsionicInteractor>();
            SearchUtilities.Find("CaveDimension_Body/Sector/Cave/FirstInteractionTrigger").AddComponent<EntryTrigger>();
            SearchUtilities.Find("CaveDimension_Body/Sector/Cave/SecondInteractionTrigger").AddComponent<EntryTrigger>();
            SearchUtilities.Find("HeartDimensionSmall_Body/Sector").AddComponent<EntryTrigger>().exitCheckAllowed = true;
            SearchUtilities.Find("CaveDimension_Body/Sector/Cave/SecondInteractionTrigger").SetActive(false);

            Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
            {

                SearchUtilities.Find("ThirdInteractionTrigger").AddComponent<EntryTrigger>();
                SearchUtilities.Find("CoreDimension_Body/Sector/Core/FourthInteractionTrigger").AddComponent<EntryTrigger>();
                SearchUtilities.Find("HeartDimension_Body/Sector/heart/FifthInteractionTrigger").AddComponent<EntryTrigger>();
                SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/SixthInteractionTrigger").AddComponent<EntryTrigger>();
                SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/SixthInteractionTrigger").GetComponent<EntryTrigger>().exitCheckAllowed = true;

                SearchUtilities.Find("CaveDimension_Body/Sector/CaveCampfire/Props_HEA_Campfire/Campfire_Flames").AddComponent<EatenMarshmallowTracker>();
                SearchUtilities.Find("HeartDimension_Body/Sector/heart/NeerPivot/Neer").AddComponent<HeadController>();
                SearchUtilities.Find("HeartDimension_Body/Sector/heart/NeerPivot/Neer").AddComponent<NeerAnimController>();
                SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/NeerPivot/Neer").AddComponent<HeadController>();

                SearchUtilities.Find("EnteranceDimension_Body/Sector/Recorder_EnteranceNode/PointLight_NOM_Recorder").GetComponent<Light>().intensity = 3f;
            });

        }

        public void AddPhysics()
        {
            SearchUtilities.Find("EnteranceDimension_Body/Sector/Recorder_EscapePod").AddComponent<NewHorizons.Components.AddPhysics>();
            SearchUtilities.Find("EnteranceDimension_Body/Sector/Recorder_EnteranceNode").AddComponent<NewHorizons.Components.AddPhysics>();
            SearchUtilities.Find("CoreDimension_Body/Sector/Recorder_Essence_2").AddComponent<NewHorizons.Components.AddPhysics>();
            SearchUtilities.Find("CoreDimension_Body/Sector/Recorder_Essence_1").AddComponent<NewHorizons.Components.AddPhysics>();
            SearchUtilities.Find("CoreDimension_Body/Sector/Dead_Nomai_1").AddComponent<NewHorizons.Components.AddPhysics>();
            SearchUtilities.Find("CoreDimension_Body/Sector/Dead_Nomai_2").AddComponent<NewHorizons.Components.AddPhysics>();           

        }

        public void InvokeEnding()
        {
            Invoke("TriggerEnding", 5);
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Effects").SetActive(false);
        }

        public void TriggerEnding()
        {
            var endingTrigger = SearchUtilities.Find("Ending_Trigger");
            endingTrigger.transform.localPosition = new Vector3(0, 0, 0);
            endingTrigger.SetActive(true);

        }

        /*
        private void AddColliders( )
        {
            var entryCollider = new GameObject("EntryDimensionCollider");
            var endingCollider = new GameObject("EndingCollider");

            entryCollider.AddComponent<SphereCollider>().isTrigger = true;
            entryCollider.GetComponent<SphereCollider>().radius = 2000;
            entryCollider.AddComponent<EntryTrigger>();
            entryCollider.transform.parent = SearchUtilities.Find("EnteranceDimension_Body/Sector").transform.parent;
            entryCollider.transform.localPosition = new Vector3(0, 0, 0);

            endingCollider.AddComponent<SphereCollider>().isTrigger = true;
            endingCollider.GetComponent<SphereCollider>().radius = 2000;
            endingCollider.AddComponent<EntryTrigger>();
            endingCollider.transform.parent = SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node").transform.parent;
            entryCollider.transform.localPosition = new Vector3(0, 0, 0);
            endingCollider.SetActive(false);
        }
        */
        public void DarkBrambleTransform()
        {
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node").GetComponent<InnerFogWarpVolume>()._warpRadius = 150;
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/Interactables_DB").SetActive(false);
            SearchUtilities.Find("DarkBramble_Body/Atmosphere_DB").SetActive(false);
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/Effects_DB").SetActive(false);
            SearchUtilities.Find("DarkBramble_Body/Sector_DB/To_Enterance_Node/Effects/InnerWarpFogSphere").GetComponent<OWRenderer>().sharedMaterial.color = new Color(83, 129, 225);

            var darkBramble = SearchUtilities.Find("DarkBramble_Body/Sector_DB/Interactables_DB/EntranceWarp_ToHub");
            darkBramble.GetComponent<InnerFogWarpVolume>()._warpRadius = 0;
            darkBramble.SetActive(false);
        }
    }   
}
