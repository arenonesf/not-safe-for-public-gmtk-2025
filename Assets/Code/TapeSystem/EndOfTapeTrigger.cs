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

        // Centro de la cámara
        Vector3 centro = _camera.transform.position;

        // Posición del borde izquierdo (a la mitad de la altura)
        Vector3 ladoIzquierdo = new Vector3(centro.x - (ancho / 2f), centro.y, centro.z);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Frame>(out Frame frameObject))
        {
            
            if (_tape.GetLastFrameInTape().Equals(frameObject))
            {
                LoopsManager.Instance.TapeRunnedCompletely();
            }
            else
            {
                var hasFrameError = frameObject.CheckFrame();
                Debug.Log($"SHOULD ADD ERROR {hasFrameError}");
                if (hasFrameError) OnFrameWithError?.Invoke(frameObject);
            }

            frameObject.ReturnToStartPosition();
        }
    }
}
