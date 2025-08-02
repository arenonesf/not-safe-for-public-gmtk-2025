using UnityEngine;

public class State_Stop : MonoBehaviour, IState
{
    private Tape _tape;

    public void OnEnter()
    {
        _tape = GameManager.Instance.GetTape();

        Loops.Instance._prevSpeed = _tape.TapeSpeed;
        _tape.SetSpeed(0);
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
