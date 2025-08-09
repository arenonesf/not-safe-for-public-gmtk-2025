using System;
using UnityEngine;

public class EndOfTapeTrigger : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Tape _tape;
    public Action<Frame> OnFrameWithError;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        CalculateRightCamera();
    }

    private void CalculateRightCamera()
    {
        float altura = _camera.orthographicSize * 2f;
        float ancho = altura * _camera.aspect;
        
        Vector3 centro = _camera.transform.position;
        Vector3 ladoIzquierdo = new Vector3(centro.x - (ancho / 2f), centro.y, centro.z);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision) return;
        if (!collision.TryGetComponent<Frame>(out var frameObject) || !frameObject) return;

        var last = _tape ? _tape.GetLastFrameInTape() : null;

        if (last && last == frameObject)
        {
            LoopsManager.Instance?.TapeRunnedCompletely();
        }
        else
        {
            bool hasFrameError = frameObject && frameObject.CheckFrame();
            if (hasFrameError) OnFrameWithError?.Invoke(frameObject);
        }

        if (frameObject) frameObject.ReturnToStartPosition();
    }
}
