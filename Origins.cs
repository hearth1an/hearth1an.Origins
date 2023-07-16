using System;
using OWML.Common;
using OWML.ModHelper;
using Origins.Utility;
using Origins.Components;
using Origins.Components.Scripts;
using NewHorizons.Utility;
using UnityEngine;

namespace Origins
{
    public class Origins : ModBehaviour
    {
        public static INewHorizons newHorizonsAPI;
        public static Origins Instance;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            var newHorizonsAPI = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
            newHorizonsAPI.LoadConfigs(this);
            newHorizonsAPI.GetStarSystemLoadedEvent().AddListener(OnStarSystemLoaded);

            ModHelper.Console.WriteLine($"My mod {nameof(Origins)} is loaded!", MessageType.Success); 
          
            LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
            {
                if (loadScene != OWScene.SolarSystem) return;
                ModHelper.Console.WriteLine("Loaded into solar system!", MessageType.Success);                
               
            };
            
        }
        private void OnStarSystemLoaded(string systemName)
        {
            WriteUtil.WriteLine("LOADED SYSTEM " + systemName);

            if (systemName == "SolarSystem")
            {
                try
                {
                    SpawnOnStart();
                }
                catch (Exception ex)
                {
                    WriteUtil.WriteError($"{ex}");
                }
            }
        }

        private void SpawnOnStart()
        {
            var player = SearchUtilities.Find("Player_Body");
            if (player != null)
            {
              //  player.AddComponent<DebugCommands>();
                WriteUtil.WriteLine("Warp commands added");
                player.AddComponent<PlayerEffectController>();
            }

            var controllers = new GameObject("TransformControllers");

            Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() =>
            {
                controllers.AddComponent<TransformController>().TransformThings();
                controllers.AddComponent<LavaPuzzle>().Start();

            });
            

        }
    }
}