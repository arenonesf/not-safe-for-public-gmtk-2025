using System.Collections.Generic;
using UnityEngine;

public class CensorItemSelector : MonoBehaviour
{
    private Camera _camera;
    private ICensorItem _censorItemSelected;
    [SerializeField] private List<Texture2D> _CursorTextureList = new();

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Start()
    {
        ClickController.OnMouseClicked += TryFindCensorItem;
        ClickController.OnMouseClicked += ApplySelectedToolAction;
    }

    private void OnDisable()
    {
        ClickController.OnMouseClicked -= TryFindCensorItem;
        ClickController.OnMouseClicked -= ApplySelectedToolAction;
    }

    private void TryFindCensorItem(Vector2 mousePosition)
    {
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        Vector2 origin = (Vector2)worldPosition;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log($"Hit Object: {hit.collider.name}");

            if (hit.collider.TryGetComponent<ICensorItem>(out var censorItem))
            {
                // TODO: Make the proper changes to the mouseSprite or gameobject Sprite
                Debug.Log($"Censor Item Selected: {censorItem.Type}");
                _censorItemSelected = censorItem;
                ChangeToolCursor(censorItem.Type);
            }
            else
            {
                Debug.Log("The hit object is not a censor item.");
                if (hit.collider.TryGetComponent<Frame>(out var frameItem))
                    return;
                ChangeToolCursor(CensorItemType.None);
            }
        }
        else
        {
            Debug.Log("No object hit.");
        }
    }

    private void ApplySelectedToolAction(Vector2 mousePosition) 
    {
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        Vector2 origin = (Vector2)worldPosition;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);

        if (hit.collider != null)
        {

            if (hit.collider.TryGetComponent<Frame>(out var frameItem))
            {
                if (_censorItemSelected == null)
                    return;

                _censorItemSelected.TryApplyCensorToolToFrame(frameItem);
            }
            else
            {
                Debug.Log("The hit object is not a frame item.");
            }
        }
    }

    private void ChangeToolCursor(CensorItemType censorItemType) 
    {
        
        switch (censorItemType)
        {
            case CensorItemType.Stamp:
                Cursor.SetCursor(_CursorTextureList[(int)CensorItemType.Stamp], new Vector2(_CursorTextureList[(int)CensorItemType.Stamp].width / 2, _CursorTextureList[(int)CensorItemType.Stamp].height / 2), CursorMode.Auto);
                break;
            case CensorItemType.Scissors:
                Cursor.SetCursor(_CursorTextureList[(int)CensorItemType.Scissors], new Vector2(_CursorTextureList[(int)CensorItemType.Stamp].width / 2, _CursorTextureList[(int)CensorItemType.Stamp].height / 2), CursorMode.Auto);
                break;
            case CensorItemType.Pen:
                Cursor.SetCursor(_CursorTextureList[(int)CensorItemType.Pen], new Vector2(0f, _CursorTextureList[(int)CensorItemType.Stamp].height), CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(null, Vector2.left, CursorMode.Auto);
                break;
        }
    }
}
