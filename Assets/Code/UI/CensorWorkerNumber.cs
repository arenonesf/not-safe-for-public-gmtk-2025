using UnityEngine;
using TMPro;

public class CensorWorkerNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _censorWorkerText;
    private void Awake()
    {
        _censorWorkerText.SetText((GameManager.Instance.CensorWorkerCount + 1).ToString());
    }
}
