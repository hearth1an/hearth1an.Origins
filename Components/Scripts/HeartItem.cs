using UnityEngine;
using NewHorizons.Handlers;
using NewHorizons.Utility;

namespace Origins.Components.Scripts
{
	public class HeartItem : OWItem
	{
		public BoxCollider _collider;
		private bool wasTaken = false;
		private GameObject sixthTrigger;
		private GameObject hazard;

		public override string GetDisplayName()
		{
			return TranslationHandler.GetTranslation("HEART_ITEM", TranslationHandler.TextType.UI);
		}

		public override void Awake()
		{
			_type = (ItemType)244;
			sixthTrigger = SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/SixthInteractionTrigger");
			hazard = SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/HazardVolume");

			base.Awake();
		}
		public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget)
		{
			base.DropItem(position, normal, parent, sector, customDropTarget);
			EnableInteraction(true);
			SetColliderActivation(true);
			PlayerEffectController.PlayAudioOneShot(AudioType.ToolItemWarpCoreDrop, 0.5f);

        }

        public override void PickUpItem(Transform holdTranform)
        {
            PlayerEffectController.PlayAudioOneShot(AudioType.ToolItemWarpCorePickUp, 0.5f);
            base.PickUpItem(holdTranform);
            transform.localPosition = new Vector3(0f, 0.2f, 0f);
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);			

			if (holdTranform != null)
            {
				if (!wasTaken)
                {
					SearchUtilities.Find("HeartDimensionSmall_Body/Sector/BrambleCrash_SFX").SetActive(true);
					sixthTrigger.SetActive(true);
					
					//SearchUtilities.Find("HeartDimensionSmall_Body/Sector/heart/backup/rings_low/GameObject/ring1_low").AddComponent<NewHorizons.Components.AddPhysics>();
					Invoke("StartMinimise", 10f);
					wasTaken = true;


                }
            }

        }

		private void StartMinimise()
        {
			SearchUtilities.Find("HeartDimensionSmall_Body/Sector").GetComponent<SmoothMinimiser>().StartEvent();
			//hazard.SetActive(true);
			Invoke("TriggerEnding", 20);

		}

        private void TriggerEnding() => FindObjectOfType<TransformController>().InvokeEnding();

		public override void SocketItem(Transform socketTransform, Sector sector)
		{
			base.SocketItem(socketTransform, sector);
			EnableInteraction(false);
			SetColliderActivation(true);
		}


	}
}

