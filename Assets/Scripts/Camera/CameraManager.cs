using System.Collections.Generic;
using UnityEngine;

namespace CreativeUrge.Camera
{
    public class CameraManager : MonoBehaviour
    {
        private readonly List<ICameraController> cameraControllers = new List<ICameraController>();
        private Vector3 initialCameraPosition;
        private Quaternion initialCameraRotation;
        
        public static CameraManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            
            Application.targetFrameRate = 60;

            GetCameraControllers();
            
            initialCameraPosition = UnityEngine.Camera.main.transform.position;
            initialCameraRotation = UnityEngine.Camera.main.transform.rotation;
        }

        private void GetCameraControllers()
        {
            foreach (var controller in this.GetComponents<ICameraController>())
            {
                cameraControllers.Add(controller);
            }
        }

        public void EnableInput()
        {
            foreach (var controller in cameraControllers)
            {
                controller.EnableInput();
            }
        }

        public void DisableInput()
        {
            foreach (var controller in cameraControllers)
            {
                controller.DisableInput();
            }
        }

        public void ResetCamera()
        {
            var camTransform = UnityEngine.Camera.main.transform;
            camTransform.position = initialCameraPosition;
            camTransform.rotation = initialCameraRotation;
        }
    }
}
