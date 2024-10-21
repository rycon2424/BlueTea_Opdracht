using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public float mouseSensitivity = 100f;

        private Transform playerBody;
        private float xRotation = 0f;

        void Start()
        {
            playerBody = transform.root;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UpdateCamera(Vector2 cameraMovement)
        {
            float mouseX = cameraMovement.x * mouseSensitivity * Time.deltaTime;
            float mouseY = cameraMovement.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}