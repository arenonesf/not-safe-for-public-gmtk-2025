using Code.Core;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : PersistentSingleton<CameraShakeManager>
{
    [Header("General Shake Settings")]
    [SerializeField] private float globalShakeForce = 0.1f;
    [SerializeField] private float gameOverShakeForce = 1.0f;

    [Header("Dynamic Error Shake Settings")]
    [SerializeField] private float minShakeForce = 0.05f;
    [SerializeField] private float maxShakeForce = 0.15f;
    [SerializeField, Range(0f, 1f)] private float strongShakeChance = 0.1f;
    [SerializeField] private float strongShakeMultiplier = 2.0f;

    private CinemachineImpulseSource _impulseSource;

    protected override void Awake()
    {
        base.Awake();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void CameraShakeGameOver()
    {
        // Strongest shake
        Vector2 randomDir2D = Random.insideUnitCircle.normalized;
        Vector3 shakeDirection = new Vector3(randomDir2D.x, randomDir2D.y, 0f);

        Vector3 impulse = shakeDirection * gameOverShakeForce;

        _impulseSource.GenerateImpulse(impulse);

        Debug.Log($"[CameraShake] GAME OVER shake | Force: {gameOverShakeForce:F2} | Direction: {shakeDirection}");
    }


    public void CameraShakeOnError()
    {
        // Randomized base force within range
        float baseForce = Random.Range(minShakeForce, maxShakeForce);
        bool isStrongShake = Random.value < strongShakeChance;
        if (isStrongShake)
            baseForce *= strongShakeMultiplier;

        // Random 2D direction in the X/Y plane
        Vector2 randomDir2D = Random.insideUnitCircle.normalized;
        Vector3 shakeDirection = new Vector3(randomDir2D.x, randomDir2D.y, 0f);

        // Final shake vector
        Vector3 impulse = shakeDirection * baseForce;

        _impulseSource.GenerateImpulse(impulse);

        Debug.Log($"[CameraShake] Error shake | Force: {baseForce:F2} | Direction: {shakeDirection} | Strong: {isStrongShake}");
    }
}
