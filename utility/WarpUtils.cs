using NewHorizons.Utility;
using Origins.Components.Scripts;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Origins.Utility
{
	internal static class WarpUtils
	{	
		public static void WarpToPlanet(AstroObject.Name planetName, float offset)
		{
			var planet = Locator.GetAstroObject(planetName);

			var body = PlayerState.AtFlightConsole() ? Locator.GetShipBody() : Locator.GetPlayerBody();

			var newWorldPos = planet.transform.position + Vector3.up * offset;

			body.WarpToPositionRotation(newWorldPos, Quaternion.identity);
			body.SetVelocity(planet.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));
		}	

		public static void WarpToEnterance()
		{
			var dimension = SearchUtilities.Find("EnteranceDimension_Body");

			var body = PlayerState.AtFlightConsole() ? Locator.GetShipBody() : Locator.GetPlayerBody();

			var newWorldPos = dimension.transform.TransformPoint(new Vector3(0, 0, 0));

			body.WarpToPositionRotation(newWorldPos, Quaternion.identity);
			body.SetVelocity(dimension.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));

			GlobalMessenger.FireEvent("PlayerFogWarp");
		}
		public static void WarpToDarkNest()
		{
			var dimension = SearchUtilities.Find("DarkNestDimension_Body");

			var body = PlayerState.AtFlightConsole() ? Locator.GetShipBody() : Locator.GetPlayerBody();

			var newWorldPos = dimension.transform.TransformPoint(new Vector3(10, 10, 25));

			body.WarpToPositionRotation(newWorldPos, Quaternion.identity);
			body.SetVelocity(dimension.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));

			GlobalMessenger.FireEvent("PlayerFogWarp");
		}

		public static void WarpToCave()
		{
			var dimension = SearchUtilities.Find("CaveDimension_Body");

			var body = PlayerState.AtFlightConsole() ? Locator.GetShipBody() : Locator.GetPlayerBody();

			var newWorldPos = dimension.transform.TransformPoint(new Vector3(25, 25, 25));

			body.WarpToPositionRotation(newWorldPos, Quaternion.identity);
			body.SetVelocity(dimension.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));

			GlobalMessenger.FireEvent("PlayerFogWarp");
		}
		public static void WarpToCore()
		{
			var dimension = SearchUtilities.Find("CoreDimension_Body");

			var body = PlayerState.AtFlightConsole() ? Locator.GetShipBody() : Locator.GetPlayerBody();

			var newWorldPos = dimension.transform.TransformPoint(new Vector3(10, 10, 25));

			body.WarpToPositionRotation(newWorldPos, Quaternion.identity);
			body.SetVelocity(dimension.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));

			GlobalMessenger.FireEvent("PlayerFogWarp");
		}
		public static void WarpToHeart()
		{
			var dimension = SearchUtilities.Find("HeartDimension_Body");

			var body = PlayerState.AtFlightConsole() ? Locator.GetShipBody() : Locator.GetPlayerBody();

			var newWorldPos = dimension.transform.TransformPoint(new Vector3(10, 10, 25));

			body.WarpToPositionRotation(newWorldPos, Quaternion.identity);
			body.SetVelocity(dimension.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));

			GlobalMessenger.FireEvent("PlayerFogWarp");
		}
		public static void WarpToHeartSmall()
		{
			var dimension = SearchUtilities.Find("HeartDimensionSmall_Body");

			var body = PlayerState.AtFlightConsole() ? Locator.GetShipBody() : Locator.GetPlayerBody();

			var newWorldPos = dimension.transform.TransformPoint(new Vector3(10, 10, 25));

			body.WarpToPositionRotation(newWorldPos, Quaternion.identity);
			body.SetVelocity(dimension.GetAttachedOWRigidbody().GetPointVelocity(newWorldPos));

			GlobalMessenger.FireEvent("PlayerFogWarp");
		}

		public static void RunPsionicThing()
		{
			Locator.GetPlayerBody().GetComponent<PsionicInteractor>().StartPsionicEvent();
			
		}
	}
}