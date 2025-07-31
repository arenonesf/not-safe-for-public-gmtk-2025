using UnityEngine;

public class EndOfTapeTrigger : MonoBehaviour
{
    private Camera _camera;

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

        Debug.Log("Posición del borde izquierdo: " + ladoIzquierdo);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Frame>(out Frame frameObject))
        {
            Debug.Log("End of tape reached");
            frameObject.ReturnToStartPosition();
        }
    }
}
