using UnityEngine;

public class State_Stop : MonoBehaviour, IState
{
    private Tape _tape;
    private CensorItemSelector _censorItem;
    private ICensorItem _prevCensorItem;

    public State_Stop(Tape tape)
    {
        _tape = tape;
    }

    public void OnEnter()
    {
        Debug.Log(_tape);
        Debug.Log(_tape.TapeSpeed);
        Loops.Instance._prevSpeed = _tape.TapeSpeed;
        _tape.SetSpeed(0);

        _censorItem = FindFirstObjectByType<CensorItemSelector>();
        _prevCensorItem = _censorItem.GetCensorItemSelect();

        _censorItem.SetCensorItemSelect(null);
        _censorItem.ChangeToolCursor(CensorItemType.None);
    }

    public void OnExit()
    {
        _censorItem.SetCensorItemSelect(_prevCensorItem);
        _censorItem.ChangeToolCursor(_prevCensorItem.Type);
    }

    public void OnUpdate()
    {
    }
}
