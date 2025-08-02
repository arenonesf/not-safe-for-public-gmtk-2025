using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace Code.UI
{
    public class OnButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Button Hover Params")]
        [SerializeField] private float _targetScale = 4f;
        [SerializeField] private float _targetYPosition = 37f;
        [SerializeField] private Sprite _selectedSprite;
        [Header("Button Default Params")]
        [SerializeField] private float _defaultYPosition = -27f;
        [SerializeField] private float _defaultScale = 3f;
        [Header("Button Transition Params")]
        [SerializeField] private float _moveYDuration = 0.075f;
        [SerializeField] private float _scaleDuration = 0.5f;
        
        private Sprite _defaultSprite;
        private Image _visibleSprite;
        private Button _button;
        public Action OnNotebookClicked;

        private void Awake()
        {
            _visibleSprite = GetComponent<Image>();
            _defaultSprite = _visibleSprite.sprite;
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OpenNotebook);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOMoveY(_targetYPosition, _moveYDuration);
            transform.DOScale(_targetScale, _scaleDuration);
            _visibleSprite.sprite = _selectedSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOMoveY(_defaultYPosition,_moveYDuration);
            transform.DOScale(_defaultScale, _scaleDuration);
            _visibleSprite.sprite = _defaultSprite;
        }

        private void OpenNotebook()
        {
            Debug.Log("Notebook Should Open");
            OnNotebookClicked?.Invoke();
        }
    }
}
