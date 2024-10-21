using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.StateMachines.States;

namespace Game.StateMachines
{
	public class StateMachine
	{
		private IState currentState;

		private Dictionary<System.Type, IState> stateCollection = new Dictionary<System.Type, IState>();

		public StateMachine(params IState[] states)
		{
			for (int i = 0; i < states.Length; i++)
			{
				stateCollection.Add(states[i].GetType(), states[i]);
			}
		}

		public IState CurrentState
		{
			get
			{
				return currentState;
			}
		}

		public void StateUpdate()
		{
			currentState?.OnStateUpdate();
		}

		public void StateFixedUpdate()
		{
			currentState?.OnStateFixedUpdate();
		}

		public void SwitchState(System.Type newStateType, float[] values = null)
		{
			if (stateCollection.ContainsKey(newStateType))
			{
				SwitchState(stateCollection[newStateType], values);
			}
			else
			{
				Debug.LogError($"State {newStateType.ToString()} not found in stateCollection");
			}
		}

		public void SwitchState(IState newState, float[] values = null)
		{
			currentState?.OnStateExit();
			currentState = newState;
			currentState?.OnStateEnter(values);
		}

		public void AddState(IState state)
		{
			stateCollection.Add(state.GetType(), state);
		}

		public bool IsInState(System.Type state)
		{
			if (currentState.ToString() == state.ToString())
			{
				return true;
			}
			return false;
		}
	}
}