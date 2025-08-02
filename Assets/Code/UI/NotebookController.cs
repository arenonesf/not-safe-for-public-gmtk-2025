using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Code.UI
{
    public class NotebookController : MonoBehaviour
    {
        [SerializeField] private OnButtonSelected _onButtonSelected;
        [SerializeField] private GameObject _notebook;
        private Image _notebookBackground;

        private void Awake()
        {
            _notebookBackground = _notebook.GetComponent<Image>();
        }

        private void OnEnable()
        {
            _onButtonSelected.OnNotebookClicked += OpenNotebook;
        }

        private void OnDisable()
        {
            _onButtonSelected.OnNotebookClicked -= OpenNotebook;
        }

        private void OpenNotebook()
        {
            _notebook.SetActive(true);
            _notebookBackground.DOFade(1f, .5f).SetEase(Ease.InOutSine);
        }

        private void CloseNotebook()
        {
            _notebookBackground.DOFade(0f, 1f).SetEase(Ease.InOutSine).OnComplete(() => _notebook.SetActive(false));
        }
    }
}
