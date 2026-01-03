using System.Collections;
using System.Collections.Generic;

namespace MKUtil.SM
{

    // T is some IStateMachine which takes some IState

    public class StateController<T, V> where T : IStateMachine<V>
    {

        // Something which holds a state machine and its states (by id).
        // NOTE: Must be executed manually by calling Execute().

        private Dictionary<string, V> _states = new Dictionary<string, V>();
        private T _stateMachine;

        public void Initialize(T stateMachine)
        {
            this._stateMachine = stateMachine;
        }
        public void Execute()
        {
            _stateMachine?.Execute();
        }
        public void AddState(string id, V state)
        {
            if (_states.ContainsKey(id))
            {
                DebugLogger.Log(GetType(), $"State '{id}' already exists. Overwriting.");
                _states[id] = state;
            }
            else
            {
                _states.Add(id, state);
            }
        }
        public void ChangeState(string id)
        {
            if (TryGet(id, out V state))
            {
                _stateMachine.ChangeState(state);
            }
            else
            {
                DebugLogger.Log(GetType(), $"No id {id} exists in state machine.");
            }
        }
        public void RemoveState(string id)
        {
            if (_states.ContainsKey(id))
            {
                _states.Remove(id);
            }
        }
        public bool TryGet(string id, out V result)
        {
            return (_states.TryGetValue(id, out result));
        }

    }

    // EXAMPLE

    //public class WeatherManager : MonoBehaviour
    //{
    //    // You define the specific types here
    //    private StateController<StateMachine<WeatherState>, WeatherState> _weatherController;

    //    void Awake()
    //    {
    //        // Controller
    //        _weatherController = new StateController<StateMachine<WeatherState>, WeatherState>();

    //        // Initialize
    //        var machine = new StateMachine<WeatherState>();
    //        _weatherController.Initialize(machine);

    //        // Add States.
    //        _weatherController.AddState("Sunny", new SunnyState());
    //        _weatherController.AddState("Rainy", new RainyState());
    //    }

    //    void Update()
    //    {
    //        _weatherController.Execute();
    //    }
    //}

}