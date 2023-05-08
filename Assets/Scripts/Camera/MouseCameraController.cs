using UnityEngine;
using UnityEngine.EventSystems;

namespace CreativeUrge.Camera
{
    public class MouseCameraController : MonoBehaviour, ICameraController
    {
        public bool InputEnabled { get; set; } = true;

#if UNITY_EDITOR || UNITY_STANDALONE
        private Transform cameraTransform;

        private bool isDragging = false;

        private const float MOVE_SENSITIVITY = 5f;
        private const float ZOOM_SENSITIVITY = 5f;
        private const float MIN_ZOOM = 0.5f;
        private const float MAX_ZOOM = 10f;

        private void Awake()
        {
            cameraTransform = UnityEngine.Camera.main.transform;
        }

        private void Update()
        {
            if (!InputEnabled)
            {
                return;
            }

            DetectDrag();

            if (isDragging)
            {
                MoveCamera();
            }

            ZoomCamera();
        }

        private void DetectDrag()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                isDragging = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }

        private void MoveCamera()
        {
            var axisX = Input.GetAxis("Mouse X");
            var axisY = Input.GetAxis("Mouse Y");

            var moveX = -axisX * MOVE_SENSITIVITY * Time.deltaTime;
            var moveZ = -axisY * MOVE_SENSITIVITY * Time.deltaTime;

            cameraTransform.position += new Vector3(moveX, 0, moveZ);
        }

        private void ZoomCamera()
        {
            var axis = Input.GetAxis("Mouse ScrollWheel");
            var zoomDelta = axis * ZOOM_SENSITIVITY * Time.deltaTime;

            var zoomVector = cameraTransform.forward * zoomDelta;
            var newPosition = cameraTransform.position + new Vector3(0, zoomVector.y, 0);

            newPosition.y = Mathf.Clamp(newPosition.y, MIN_ZOOM, MAX_ZOOM);

            cameraTransform.position = newPosition;
        }
#endif
    }
}