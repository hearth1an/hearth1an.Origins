using UnityEngine;
using NewHorizons.Handlers;
using NewHorizons.Utility;

namespace Origins.Components.Scripts
{
    public class FireVaseItem : OWItem
    {
		public BoxCollider _collider;

		private InteractReceiver _flameGreen;
		private InteractReceiver _flameRed;
		private InteractReceiver _flameYellow;
		
		public override string GetDisplayName( )
		{
			return TranslationHandler.GetTranslation("FIRE_VASE_ITEM", TranslationHandler.TextType.UI);
		}
		 
		public override void Awake()
		{
			_type = (ItemType)256;

			_flameGreen = SearchUtilities.Find("CaveDimension_Body/Sector/Cave/HornetGreen").GetComponent<InteractReceiver>();
			_flameRed = SearchUtilities.Find("CaveDimension_Body/Sector/Cave/HornetRed").GetComponent<InteractReceiver>();
			_flameYellow = SearchUtilities.Find("CaveDimension_Body/Sector/Cave/HornetYellow").GetComponent<InteractReceiver>();

			base.Awake();			
		}
		public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
		{			
			base.DropItem(position, normal, parent, sector, customDropTarget);
			EnableInteraction(true);
			SetColliderActivation(true);
			PlayerEffectController.PlayAudioOneShot(AudioType.ToolItemWarpCoreDrop, 0.5f);

			_flameGreen.DisableInteraction();
			_flameRed.DisableInteraction();
			_flameYellow.DisableInteraction();

			_flameGreen.gameObject.GetComponent<FireInteractor>().enabled = false;
			_flameRed.gameObject.GetComponent<FireInteractor>().enabled = false;
			_flameYellow.gameObject.GetComponent<FireInteractor>().enabled = false;
		}

		public override void PickUpItem(Transform holdTranform)
		{
			PlayerEffectController.PlayAudioOneShot(AudioType.ToolItemWarpCorePickUp, 0.5f);
			base.PickUpItem(holdTranform);
			transform.localPosition = new Vector3(0f, -0.4f, 0f);

			_flameGreen.EnableInteraction();
			_flameRed.EnableInteraction();
			_flameYellow.EnableInteraction();
		}

		public override void SocketItem(Transform socketTransform, Sector sector)
		{
			base.SocketItem(socketTransform, sector);
			EnableInteraction(false);
			SetColliderActivation(true);
		}


	}
}

