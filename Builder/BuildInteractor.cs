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
        public GameObject Indicator;
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
                var objectHit = hit.transform.gameObject;
                print(objectHit);
                if (objectHit.GetComponent<TileData>())
                {
                    if (objectHit == ViewTargetObject)
                    {
                        ReleaseTarget();
                        BuilderInterfaceManager.Instance.ToggleBuildFoundationsBar(false);
                        CardinalBuilder.Instance.SelectedObject = null;
                        return;
                    }
                    else if (objectHit != ViewTargetObject &&
                        ViewTargetObject != null)
                    {
                        //should be doing nothing here
                    }
                    if (objectHit != ViewTargetObject && ViewTargetObject == null)
                    {
                        CenterOnTarget(objectHit);
                        BuilderInterfaceManager.Instance.ToggleBuildFoundationsBar(true);
                        CardinalBuilder.Instance.SelectedObject = objectHit;
                        return;
                    }
                }
                if (objectHit.GetComponent<BuildingData>())
                {
                    if (objectHit == ViewTargetObject)
                    {
                        ReleaseTarget();
                        CardinalBuilder.Instance.SelectedObject = null;
                        return;
                    }
                    else if (objectHit != ViewTargetObject &&
                        ViewTargetObject != null)
                    {
                        //should be doing nothing here
                    }
                    if (objectHit != ViewTargetObject && ViewTargetObject == null)
                    {
                        CenterOnTarget(objectHit);
                        CardinalBuilder.Instance.SelectedObject = objectHit;
                        return;
                    }
                }

            }
        }

        void CenterOnTarget(GameObject target)
        {
            ViewTargetObject = target;
            Indicator.transform.position = target.transform.position;
            MainCam.LookAt = target.transform;
        }

        void ReleaseTarget()
        {
            ViewTargetObject = null;
            Indicator.transform.position = Vector3.down;
            MainCam.LookAt = defaultTarget.transform;
        }

        public void RemoteReleaseTarget()
        {
            ReleaseTarget();
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

