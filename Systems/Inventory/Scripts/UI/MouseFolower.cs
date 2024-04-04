using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFolower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private UIInventoryItem item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();        
        item = GetComponentInChildren<UIInventoryItem>(); //O Item prefab � a child do MouseFolower
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity); //Dados do item que vai ser arrastad
    }
 
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, //RectTransform � o transform em screen space e nao em world space
        Input.mousePosition, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toogle(bool val)
    {        
        gameObject.SetActive(val);
    }
}
