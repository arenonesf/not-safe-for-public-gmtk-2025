using Code.Core.Timer;
using System.Collections;
using UnityEngine;

public class Loops : MonoBehaviour
{
    private int _mistakes = 0;
    private int _currentMistakes = 0;
    
    private Tape _tape;
    [SerializeField] private int _maxMistakes;

    private float _prevSpeed;

    public int MistakeForLoop => _mistakes;

    void Start()
    {
        _tape = GetComponent<Tape>();
    }

    public void SetFinishLoop()
    {
        ++_currentMistakes;
        StopLoop();
    }

    private void StopLoop()
    {
        _prevSpeed = _tape.TapeSpeed;
        _tape.SetSpeed(0);

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        _mistakes += _currentMistakes;

        if (_maxMistakes <= _mistakes)
            Debug.Log("Perdiste");
        else
        {
            if (_currentMistakes > 0)
                _tape.SetSpeed(_prevSpeed * 1.25f);
            else
                _tape.SetSpeed(_prevSpeed);

            _currentMistakes = 0;

        }
    }
}
