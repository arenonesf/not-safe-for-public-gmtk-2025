using System;
using UnityEngine;

public class CensorItemSelector : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    void Start()
    {
        ClickController.OnMouseClicked += TryFindCensorItem;
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
            }
            else
            {
                Debug.Log("The hit object is not a censor item.");
            }
        }
        else
        {
            Debug.Log("No object hit.");
        }
    }
}
