using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachines.States
{
    public class Locomotion : State<PlayerBehaviour>, IState
    {
        private Vector2 movementInput;
        private Vector3 velocity;
        private bool grounded;

        public Locomotion(PlayerBehaviour owner) : base(owner)
        {

        }

        public override void OnStateEnter(float[] values)
        {
            Owner.PlayerInput.FirstPersonPlayer.Jump.started += Jump;
        }

        public override void OnStateExit()
        {
            Owner.PlayerInput.FirstPersonPlayer.Jump.started -= Jump;
        }

        public override void OnStateUpdate()
        {
            grounded = Owner.GroundedCheck();

            Movement();

            Owner.UpdateCamera();
        }

        void Movement()
        {
            if (grounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            movementInput = Owner.PlayerInput.FirstPersonPlayer.Movement.ReadValue<Vector2>();

            Vector3 movementDirection = Owner.transform.right * movementInput.x + Owner.transform.forward * movementInput.y;
            Owner.CharController.Move(movementDirection * Owner.moveSpeed * Time.deltaTime);

            // Gravity
            velocity.y += Owner.gravity * Time.deltaTime;
            Owner.CharController.Move(velocity * Time.deltaTime);
        }

        void Jump(InputAction.CallbackContext context)
        {
            if (grounded)
            {
                velocity.y = Mathf.Sqrt(Owner.jumpHeight * -2f * Owner.gravity);
            }
        }
    }
}