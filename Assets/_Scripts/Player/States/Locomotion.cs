using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachines.States
{
    public class Locomotion : State<PlayerBehaviour>, IState
    {
        public Locomotion(PlayerBehaviour owner) : base(owner)
        {

        }
    }
}