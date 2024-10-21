using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachines.States
{
	public interface IState
	{
		void OnStateEnter(float[] values);
		void OnStateExit();
		void OnStateUpdate();

		void OnStateFixedUpdate();
	}
}