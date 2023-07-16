using UnityEngine;

namespace Origins.Components.Scripts
{
    public class CameraEffect : MonoBehaviour
    {
        private OWCamera viewerCamera;
        NomaiViewerImageEffect effect;
        private ICommonCameraAPI commonCameraAPI;

        private void Awake()
        {
            LoadCamera();
            base.enabled = true;
        }
        private void LoadCamera()
        {
            var remote = FindObjectOfType<NomaiRemoteCamera>();
            var isActive = remote.gameObject.activeInHierarchy;
            remote.gameObject.SetActive(false);
            var newCam = GameObject.Instantiate(remote.gameObject);
            remote.gameObject.SetActive(isActive);

            GameObject.Destroy(newCam.GetComponent<NomaiRemoteCamera>());
            viewerCamera = newCam.GetComponent<OWCamera>();
            var camera = newCam.GetComponent<Camera>();

            viewerCamera.gameObject.SetActive(true);

            commonCameraAPI = Origins.Instance.ModHelper.Interaction.TryGetModApi<ICommonCameraAPI>("xen.CommonCameraUtility");
            commonCameraAPI.RegisterCustomCamera(viewerCamera);
            effect = viewerCamera.GetComponent<NomaiViewerImageEffect>();

            Origins.Instance.ModHelper.Events.Unity.FireOnNextUpdate(() => { viewerCamera.transform.parent = Locator.GetPlayerCamera().transform; });
        }

        public void EnterView()
        {
            commonCameraAPI.EnterCamera(viewerCamera);
            effect.SetFade(1);
        }

        public void ExitView()
        {
            commonCameraAPI.ExitCamera(viewerCamera);
            effect.SetFade(0);
        }
    }
}
