using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base Class For The State Machine That We Create
/// </summary>
public class State_Machine : MonoBehaviour
{
    [SerializeField] public State currentState;
    [HideInInspector]
    public UnityEvent OnStateEnter;
    [HideInInspector]
    public UnityEvent OnStateExit;

    protected virtual void OnEnable()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.OnEnter();
    }

    private void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    /// <summary>
    /// Get The Initial State of The State Machine
    /// </summary>
    /// <returns></returns>
    protected virtual State GetInitialState()
    {
        return null;
    }

    /// <summary>
    /// Set a New State For the State Machine
    /// </summary>
    /// <param name="newState"></param>
    public void SetState(State newState)
    {
        if (newState == null)
        {
            Debug.LogError($"{name} : New State Is Null", this);
            return;
        }

        if (newState == currentState)
            return;

        OnStateExit?.Invoke();
        currentState.OnExit();
        Debug.Log($"Transitioned State : {currentState.name} => {newState.name}" );
        currentState = newState;

        OnStateEnter?.Invoke();
        currentState.OnEnter();
    }
}