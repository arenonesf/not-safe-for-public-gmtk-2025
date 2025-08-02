using UnityEngine;

public class SelfRotator : MonoBehaviour
{
    [SerializeField] private float _rotionSpeed = 90f; // Degrees per second

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, _rotionSpeed * Time.deltaTime);
    }
}
