using NewHorizons.Utility;
using Origins.Utility;
using UnityEngine;

namespace Origins.Components.Scripts
{
    internal class LavaPuzzle: MonoBehaviour    
    {
        public void Start()
        {
            SearchUtilities.Find("EnteranceDimension_Body/Sector/MoltenCore").transform.localPosition = new Vector3(16.1794f, -599.5986f, 20.0453f);
        }

        private void Update()
        {
            var cockpit = Locator.GetShipBody().GetComponent<ShipDamageController>();
            var lava = SearchUtilities.Find("EnteranceDimension_Body/Sector/MoltenCore");

            if (cockpit != null)
            {
                if (cockpit._cockpitDetached && !SearchUtilities.Find("Module_Cockpit_Body").activeSelf && lava.activeSelf)
                {
                    lava.SetActive(false);
                    WriteUtil.WriteLine("Lava puzzle");
                    PlayerEffectController.PlayAudioOneShot(AudioType.Splash_Lava, 1);

                }
            }
        }
    }
}
