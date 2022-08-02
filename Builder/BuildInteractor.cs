using CardinalSystems.Builder;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cardinal.Builder
{
    public class BuildInteractor : MonoBehaviour
    {
        public InputAction SelectAction;
        [SerializeField]
        GameObject defaultTarget;

        GameObject ViewTargetObject;
        CinemachineVirtualCamera MainCam;

        private void Awake()
        {
            SelectAction.performed += OnSelect;
        }

        private void OnEnable()
        {
            SelectAction.Enable();
        }

        private void OnDisable()
        {
            SelectAction.Disable();
        }

        void Start()
        {
            MainCam = Camera.main.GetComponent<CinemachineBrain>()
                .ActiveVirtualCamera.VirtualCameraGameObject.
                    GetComponent<CinemachineVirtualCamera>();
        }

        void Update()
        {
        }

        void OnSelect(InputAction.CallbackContext context)
        {
            RaycastHit hit;
            var ray = fireRay();
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 30f);
                var objectHit = hit.transform.gameObject;
                if (objectHit.GetComponent<TileData>() && objectHit != ViewTargetObject)
                {
                    ViewTargetObject = objectHit;
                    CenterOnTarget(objectHit);
                    CardinalBuilder.Instance.SelectedTile = objectHit;
                    BuilderInterfaceManager.Instance.ToggleBuildFoundationsBar(true);
                }
                else
                {
                    ViewTargetObject = null;
                    ReleaseTarget();
                    CardinalBuilder.Instance.SelectedTile = null;
                    BuilderInterfaceManager.Instance.ToggleBuildFoundationsBar(false);
                }
            }
        }

        void CenterOnTarget(GameObject target)
        {
            MainCam.LookAt = target.transform;
        }

        void ReleaseTarget()
        {
            MainCam.LookAt = defaultTarget.transform;
        }

        public Ray fireRay()
        {
#if !UNITY_ANDROID
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
#endif

#if UNITY_ANDROID
                Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.position.ReadValue());
#endif
            if (Camera.main.transform.position.y < 0)
            {
                print("ATTENTION: Cam pos was " + Camera.main.transform.position + "and local pos was " + Camera.main.transform.localPosition);
                print("ATTENTION: Ray pos was " + ray.origin);
                Debug.Log("The camera appears to have been below the ground!");

            }
            return ray;
        }

        public TileData GetTargetData()
        {
            return ViewTargetObject.GetComponent<TileData>();
        }
    }
}

