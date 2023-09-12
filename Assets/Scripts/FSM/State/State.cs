using System.Collections.Generic;

public enum CharacterState
{
    IDLE,
    WALKING,
    RUNNING,
    JUMPING,
    ATTACKING,
    TAKINGDAMAGE,
    DEAD
}

/// <summary>
/// Base Class For The States That We Create
/// </summary>
public class State
{
    public string name;
    protected State_Machine stateMachine;
    protected Dictionary<string, State> transitions = new();

    public State(string name, State_Machine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    /// <summary>
    /// This Is Called When We Enter A State
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// Fuction To Update The Logic of A State (Update)
    /// </summary>
    public virtual void UpdateLogic() { }

    /// <summary>
    /// Fuction To Update The Pgysics of A State (Fixed Update)
    /// </summary>
    public virtual void UpdatePhysics() { }

    /// <summary>
    /// This Is Called When We Exit A State
    /// </summary>
    public virtual void OnExit() { }

    /// <summary>
    ///  Function To Add The Posible Transitions For A State
    /// </summary>
    /// <param name="transitionName"></param>
    /// <param name="transitionState"></param>
    public virtual void AddStateTransitions(string transitionName, State transitionState)
    {
        transitions.Add(transitionName, transitionState);
    }
}