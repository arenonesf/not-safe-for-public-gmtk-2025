using UnityEngine;

public class State_Running : MonoBehaviour, IState
{
    private GameObject _pen, _scissor, _stamp;
    public void OnEnter()
    {
        _pen = FindFirstObjectByType<Pen>().gameObject;
        _scissor = FindFirstObjectByType<Scissors>().gameObject;
        _stamp = FindFirstObjectByType<Stamp>().gameObject;

        _pen.SetActive(false);
        _scissor.SetActive(false);
        _stamp.SetActive(false);
    }

    public void OnExit()
    {
        _pen.SetActive(true);
        _scissor.SetActive(true);
        _stamp.SetActive(true);
    }

    public void OnUpdate()
    {
    }
}
