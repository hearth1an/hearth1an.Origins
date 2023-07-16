using Origins.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Origins.Components
{
	internal class DebugCommands : MonoBehaviour
	{
		private static Key _debugKey = Key.I;
		private static Key _warpKey = Key.O;

		private readonly List<(Key key, Action action)> _debugCommands = new();
		private readonly List<(Key key, Action action)> _debugWarpCommands = new();

		private ScreenPrompt _debugPrompt;
		private readonly List<ScreenPrompt> _debugPromptList = new();

		private ScreenPrompt _warpPrompt;
		private readonly List<ScreenPrompt> _warpPromptList = new();

		public void Start()
		{
			_debugPrompt = PromptUtils.AddPrompt("Debug Commands", PromptPosition.UpperRight, _debugKey);
			_warpPrompt = PromptUtils.AddPrompt("Warp Commands", PromptPosition.UpperRight, _warpKey);

			/*
			RegisterDebugCommand(Key.Numpad1, ShipHandler.BlowUpShip, "Blow up ship");
			RegisterDebugCommand(Key.Numpad2, HeldItemHandler.GivePlayerWarpCore, "Get warp core");
			RegisterDebugCommand(Key.Numpad3, HeldItemHandler.GivePlayerLantern, "Get lantern");
			RegisterDebugCommand(Key.Numpad4, OpenSarcophagus, "Open sarcophagus");
			RegisterDebugCommand(Key.Numpad9, Conditions.ResetAllConditions, "Reset conditions");
			*/
			
			RegisterDebugWarpCommand(Key.Numpad1, WarpUtils.WarpToEnterance, "Warp to Enterance Dimension");
			RegisterDebugWarpCommand(Key.Numpad2, WarpUtils.WarpToDarkNest, "Warp to DarkNest Dimension");
			RegisterDebugWarpCommand(Key.Numpad3, WarpUtils.WarpToCave, "Warp to Cave Dimension");
            RegisterDebugWarpCommand(Key.Numpad4, WarpUtils.WarpToCore, "Warp to Core Dimension");
			RegisterDebugWarpCommand(Key.Numpad5, WarpUtils.WarpToHeart, "Warp to Heart Dimension");
			RegisterDebugWarpCommand(Key.Numpad6, WarpUtils.WarpToHeartSmall, "Warp to Heart (small) Dimension");
			//RegisterDebugWarpCommand(Key.Numpad6, WarpUtils.RunPsionicThing, "Run Psionic Thing");

			/*
			RegisterDebugWarpCommand(Key.Numpad6, WarpUtils.WarpToSixthLocation, "Warp to Solanum");
			RegisterDebugWarpCommand(Key.Numpad7, WarpUtils.WarpToLetranger, "Warp to Letranger");
			RegisterDebugWarpCommand(Key.Numpad8, WarpUtils.WarpToDreamTown, "Warp to Dream Town");
			*/
		}

        public void Update()
		{
			if (Keyboard.current[_debugKey].wasPressedThisFrame)
			{
				foreach (var prompt in _debugPromptList)
				{
					prompt.SetVisibility(true);
				}
			}
			else if (Keyboard.current[_debugKey].wasReleasedThisFrame)
			{
				foreach (var prompt in _debugPromptList)
				{
					prompt.SetVisibility(false);
				}
			}

			if (Keyboard.current[_debugKey].isPressed)
			{
				foreach (var (key, action) in _debugCommands)
				{
					if (Keyboard.current[key].wasReleasedThisFrame)
					{
						action.Invoke();
					}
				}
			}

			if (Keyboard.current[_warpKey].wasPressedThisFrame)
			{
				foreach (var prompt in _warpPromptList)
				{
					prompt.SetVisibility(true);
				}
			}
			else if (Keyboard.current[_warpKey].wasReleasedThisFrame)
			{
				foreach (var prompt in _warpPromptList)
				{
					prompt.SetVisibility(false);
				}
			}

			if (Keyboard.current[_warpKey].isPressed)
			{
				foreach (var (key, action) in _debugWarpCommands)
				{
					if (Keyboard.current[key].wasReleasedThisFrame)
					{
						action.Invoke();
					}
				}
			}

			var buttonsPressed = Keyboard.current[_debugKey].isPressed || Keyboard.current[_warpKey].isPressed;
			_warpPrompt.SetVisibility(!buttonsPressed);
			_debugPrompt.SetVisibility(!buttonsPressed);
		}

		private void RegisterDebugCommand(Key key, Action action, string description)
		{
			_debugCommands.Add((key, action));
			_debugPromptList.Add(PromptUtils.AddPrompt(description, PromptPosition.UpperRight, key));
		}

		private void RegisterDebugWarpCommand(Key key, Action action, string description)
		{
			_debugWarpCommands.Add((key, action));
			_warpPromptList.Add(PromptUtils.AddPrompt(description, PromptPosition.UpperRight, key));
		}

		public static void OpenSarcophagus()
		{
			var sarcophagusController = GameObject.FindObjectOfType<SarcophagusRingworldController>();
			sarcophagusController.OnFirstSealExtinguish();
			sarcophagusController.OnSecondSealExtinguish();
			sarcophagusController.OnThirdSealExtinguish();
		}
	}
}