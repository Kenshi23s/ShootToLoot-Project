using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    Dictionary<string, IState> _statesList = new Dictionary<string, IState>();
    IState _currentState;
    string _currentStateName;
   

    public void CreateState(string name, IState state)
    {
        // si en mi diccionario no tengo esa llave 
        if (!_statesList.ContainsKey(name))
            //la creo
            _statesList.Add(name, state);
    }

    public void Execute()
    {
       
        _currentState.OnUpdate();
      
    }

    public void ChangeState(string name)
    {
        Debug.LogWarning("mi estado actual es " + name);
        if (_statesList.ContainsKey(name))
        {
            if (_currentState != null)
            {
               
                _currentState.OnExit();
            }

            _currentState = _statesList[name];
            _currentState.OnEnter();
           
        }
       
    }
    public void ShowGizmos()
    {
        _currentState.ShowStateGizmos();
    }
}

