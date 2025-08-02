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
        [SerializeField] private float _targetScale = 4f;
        [SerializeField] private float _targetYPosition = 37f;
        [SerializeField] private float _targetXPosition = 540f;
        [SerializeField] private Sprite _selectedSprite;
        [Header("Button Default Params")]
        [SerializeField] private float _defaultYPosition = -27f;
        [SerializeField] private float _defaultScale = 3f;
        [Header("Button Transition Params")]
        [SerializeField] private float _moveDuration = 0.075f;
        [SerializeField] private float _scaleDuration = 0.5f;
        
        private Sprite _defaultSprite;
        private Image _visibleSprite;
        private Button _button;
        public Action OnNotebookClicked;
        private Vector3 _targetMovePosition;

        private void Awake()
        {
            _visibleSprite = GetComponent<Image>();
            _defaultSprite = _visibleSprite.sprite;
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OpenNotebook);
            _targetMovePosition = new(_targetXPosition, _targetYPosition, 0);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //transform.DOMoveY(_targetYPosition, _moveDuration);
            //transform.DOScale(_targetScale, _scaleDuration);
            _visibleSprite.sprite = _selectedSprite;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //transform.DOMoveY(_defaultYPosition,_moveDuration);
            //transform.DOScale(_defaultScale, _scaleDuration);
            _visibleSprite.sprite = _defaultSprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            transform.DOMove(_targetMovePosition, _moveDuration);
        }

        private void OpenNotebook()
        {
            Debug.Log("Notebook Should Open");
            OnNotebookClicked?.Invoke();
        }
    }
}
