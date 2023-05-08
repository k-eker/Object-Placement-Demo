using CreativeUrge.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.Camera
{
    public class TouchCameraController : MonoBehaviour, ICameraController
    {
        public bool InputEnabled { get; set; } = true;
        
#if UNITY_IOS || UNITY_ANDROID
        private Vector2 initialTouchPosition1;
        private Vector2 initialTouchPosition2;
        private float initialDistance;
        private UnityEngine.Camera mainCamera;
        private Plane groundPlane;
        private bool isDraggingOverUI = false;

        private const float MOVE_SENSITIVITY = 1f;
        private const float ZOOM_SENSITIVITY = 1f;

        private void Awake()
        {
            mainCamera = UnityEngine.Camera.main;
            groundPlane = new Plane(Vector3.up, Vector3.zero);
        }

        private void Update()
        {
            if (!InputEnabled)
            {
                return;
            }
            
            DetectDragOverUI();

            if (Input.touchCount >= 1)
            {
                MoveCamera();
            }

            if (Input.touchCount >= 2)
            {
                var touch1 = Input.GetTouch(0);
                var touch2 = Input.GetTouch(1);
                
                var hitPos1 = GetRaycastHitPoint(touch1.position);
                var hitPos2 = GetRaycastHitPoint(touch2.position);
                
                var prevHitPos1 = GetRaycastHitPoint(touch1.position - touch1.deltaPosition);
                var prevHitPos2 = GetRaycastHitPoint(touch2.position - touch2.deltaPosition);

                ZoomCamera(hitPos1, hitPos2, prevHitPos1, prevHitPos2);
                
                RotateCamera(hitPos1, hitPos2, prevHitPos1, prevHitPos2);
            }
        }

        private void DetectDragOverUI()
        {
            if (Input.touchCount == 0)
            {
                return;
            }
            
            var touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began && TouchUtility.IsPointerOverUIObject())
            {
                isDraggingOverUI = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDraggingOverUI = false;
            }
        }

        private void MoveCamera()
        {
            if (isDraggingOverUI)
            {
                return;
            }
            
            var touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                var delta = GetPlanePositionDelta(touch);
                mainCamera.transform.Translate(delta * MOVE_SENSITIVITY, Space.World);
            }
        }

        private void ZoomCamera(Vector3 hitPos1, Vector3 hitPos2, Vector3 prevHitPos1, Vector3 prevHitPos2)
        {
            var zoom = Vector3.Distance(hitPos1, hitPos2) / Vector3.Distance(prevHitPos1, prevHitPos2);
            var lerpedPos = Vector3.LerpUnclamped(hitPos1, mainCamera.transform.position, 1 / zoom);
            
            mainCamera.transform.position = lerpedPos * ZOOM_SENSITIVITY;
        }

        private void RotateCamera(Vector3 hitPos1, Vector3 hitPos2, Vector3 prevHitPos1, Vector3 prevHitPos2)
        {
            if (prevHitPos2 == hitPos2 && prevHitPos1 == hitPos1)
            {
                return;
            }
            
            var angle = Vector3.SignedAngle(hitPos2 - hitPos1, prevHitPos2 - prevHitPos1, groundPlane.normal);
                
            mainCamera.transform.RotateAround(hitPos1, groundPlane.normal, angle);
        }

        private Vector3 GetPlanePositionDelta(Touch touch)
        {
            if (touch.phase != TouchPhase.Moved)
            {
                return Vector3.zero;
            }

            var previousRay = mainCamera.ScreenPointToRay(touch.position - touch.deltaPosition);
            var ray = mainCamera.ScreenPointToRay(touch.position);

            var previousHit = groundPlane.Raycast(previousRay, out var initialHit);
            var hit = groundPlane.Raycast(ray, out var currentHit);
            
            if (previousHit && hit)
            {
                var planePosDelta = previousRay.GetPoint(initialHit) - ray.GetPoint(currentHit);
                return planePosDelta;
            }

            return Vector3.zero;
        }

        private Vector3 GetRaycastHitPoint(Vector2 screenPos)
        {
            var ray = mainCamera.ScreenPointToRay(screenPos);
            
            return groundPlane.Raycast(ray, out var hit) ? ray.GetPoint(hit) : Vector3.zero;
        }
#endif
    }
}
