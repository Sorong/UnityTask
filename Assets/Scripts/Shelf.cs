using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField]
    private GameObject _ShelfItems;

    public delegate void OnClickCallback(Item item);

    public OnClickCallback OnClick;
    // Start is called before the first frame update
    void Start()
    {

        if (_ShelfItems != null)
        {
            var items = _ShelfItems.GetComponentsInChildren<Item>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    item.OnClick += OnItemClick;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemClick(Item item)
    {
        
        if (OnClick != null)
        {
            OnClick(item);
        }
    }
}
