using UnityEngine;

public class State_Running : MonoBehaviour, IState
{
    private Tape _tape;
    private Pen _pen;
    private Scissors _scissor;
    private Stamp _stamp;

    public State_Running(Tape tape)
    {
        _tape = tape;
    }

    public void OnEnter()
    {
        _pen = FindFirstObjectByType<Pen>();
        _scissor = FindFirstObjectByType<Scissors>();
        _stamp = FindFirstObjectByType<Stamp>();
        _pen.enabled = true;
        _scissor.enabled = true;
        _stamp.enabled = true;
    }

    public void OnExit()
    {
        _pen.enabled = false;
        _scissor.enabled = false;
        _stamp.enabled = false;     
    }

    public void OnUpdate()
    {
    }
}
