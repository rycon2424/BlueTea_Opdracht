using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachines.States
{
	public interface IState
	{
		void OnStateEnter(float[] values);
		void OnStateUpdate();
		void OnStateExit();

		void OnStateFixedUpdate();
	}
}