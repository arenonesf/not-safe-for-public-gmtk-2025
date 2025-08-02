using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace Code.UI
{
    public class OnButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Button Hover Params")]
        [SerializeField] private float _hoveredScale = 4f;
        [SerializeField] private float _targetHoverYPosition = 37f;
        [SerializeField] private Sprite _selectedSprite;
        [Header("Button Default Params")]
        [SerializeField] private float _defaultYPosition = -27f;
        [SerializeField] private float _defaultScale = 3f;
        [Header("Button Transition Params")]
        [SerializeField] private float _moveDuration = 0.075f;
        [SerializeField] private float _scaleDuration = 0.5f;
        [Header("Button Clicked Params")]
        [SerializeField] private float _clickedScale = 6f;
        [SerializeField] private float _targetClickedYPosition = 960f;
        [SerializeField] private float _targetClickedXPosition = 540f;
        
        private Sprite _defaultSprite;
        private Image _visibleSprite;
        private Button _button;
        private Vector3 _defaultPosition;
        private RectTransform _rectTransform;
        public Action OnNotebookClicked;
        private bool _clicked;

        private void Awake()
        {
            _visibleSprite = GetComponent<Image>();
            _defaultSprite = _visibleSprite.sprite;
            _button = GetComponent<Button>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OpenNotebook);
            _defaultPosition = new(_rectTransform.position.x, _rectTransform.position.y, 0);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 hoverPosition = new(_defaultPosition.x, _targetHoverYPosition, 0);
            transform.DOScale(_hoveredScale, _scaleDuration);
            transform.DOMove(hoverPosition, _moveDuration);
            _visibleSprite.sprite = _selectedSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_clicked) return;
            transform.DOScale(_defaultScale, _scaleDuration);
            transform.DOMove(_defaultPosition, _moveDuration);
            _visibleSprite.sprite = _defaultSprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _clicked = true;
            Vector3 clickPosition = new(_targetClickedXPosition, _targetClickedYPosition, 0);
            Debug.Log(clickPosition);
            transform.DOMove(clickPosition, _moveDuration);
            transform.DOScale(_clickedScale, _scaleDuration).OnComplete(() => _clicked = false);
        }

        private void OpenNotebook()
        {
            Debug.Log("Notebook Should Open");
            OnNotebookClicked?.Invoke();
        }
    }
}
