using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    private const int MaxItems = 5;
    private const float ItemGap = 0.1f;
    private const float ControlPointModifier = 3f;

    public delegate void OnCashRegisterClicked(List<Item> items);

    public OnCashRegisterClicked OnClick;

    [SerializeField]
    private Shelf _shelf;

    [SerializeField]
    private Transform _itemArea;


    private List<Item> items = new List<Item>();

    private int _itemCount = 0;

    private Vector3 PreviousPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (_shelf != null)
        {
            _shelf.OnClick += AddItem;
        }
    }

    private void OnDestroy()
    {
        if (_shelf != null)
        {
            _shelf.OnClick -= AddItem;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_itemArea.position, Vector3.up, Color.green);
    }

    public void AddItem(Item item)
    {
        if (_itemCount < MaxItems)
        {

            Vector3 offsetX = Vector3.zero;
            if (_itemCount > 0)
            {
                Bounds prevBounds = items[_itemCount - 1].Mesh.mesh.bounds;
                Bounds currentBounds = item.Mesh.mesh.bounds;
                offsetX = new Vector3(prevBounds.extents.x + currentBounds.extents.x + ItemGap, 0, 0);
                //zero = items[_itemCount - 1].transform.localPosition;
            }

            Item newItem = Instantiate(item, item.transform.position + offsetX, Quaternion.identity, _itemArea);
            CurveWalker walker = newItem.gameObject.AddComponent<CurveWalker>();
            Vector3 start = newItem.transform.localPosition;
            Vector3 end = PreviousPosition + offsetX;
            walker.ControlPoints = new List<Vector3>()
            {
                start,
                start + (ControlPointModifier * Vector3.right),
                end + (ControlPointModifier * Vector3.up),
                end
            };
            PreviousPosition += offsetX;
            walker.StartCurveWalk = true;
            items.Add(newItem);
            _itemCount++;
        }


    }

    private void OnMouseDown()
    {
        if (OnClick != null)
        {
            OnClick(this.items);
        }
    }
}
