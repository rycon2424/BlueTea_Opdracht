using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using Game.StateMachines;
using Game.StateMachines.States;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        [Header("Player Settings")]
        public float moveSpeed = 5f;
        public float gravity = -9.81f;
        public float jumpHeight = 2f;

        [Header("Grounded Settings")]
        [SerializeField] Transform groundCheck;
        [SerializeField] LayerMask groundMask;

        private bool isGrounded;

        private PlayerCamera playerCamera;
        private PlayerInteraction playerInteraction;
        private CharacterController controller;
        private GameControls input;
        private StateMachine statemachine;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<PlayerCamera>();
            playerInteraction = GetComponent<PlayerInteraction>();

            SetupInput();
            SetupStateMachine();
        }

        void SetupStateMachine()
        {
            Locomotion locomotionState = new Locomotion(this);


            statemachine = new StateMachine(locomotionState);
            statemachine.SwitchState(locomotionState);
        }

        void SetupInput()
        {
            input = new GameControls();
            input.Enable();
        }

        void Update()
        {
            statemachine.StateUpdate();
        }

        // Called from states

        public void UpdateCamera()
        {
            Vector2 cameraInput = input.FirstPersonPlayer.Camera.ReadValue<Vector2>();
            playerCamera.UpdateCamera(cameraInput);
        }

        public void CheckForInteractions()
        {
            playerInteraction.CheckForInteractions();
        }

        public bool GroundedCheck()
        {
            return Physics.CheckSphere(groundCheck.position, 0.5f, groundMask);
        }

        // Getters/Setters

        public StateMachine Statemachine
        {
            private set
            {
                Statemachine = value;
            }
            get
            {
                return Statemachine;
            }
        }

        public GameControls PlayerInput
        {
            private set
            {
                input = value;
            }
            get
            {
                return input;
            }
        }

        public CharacterController CharController
        {
            private set
            {
                controller = value;
            }
            get
            {
                return controller;
            }
        }
    }
}