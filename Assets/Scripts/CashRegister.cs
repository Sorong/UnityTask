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

    [SerializeField]
    private ParticleSystem _particleObject;

    [SerializeField]
    private AudioClip _audioClip;


    private List<Item> items = new List<Item>();

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
        if (items.Count < MaxItems)
        {

            Vector3 offsetX = CalcItemOffsetX(items.Count > 0 ? items[items.Count - 1] : null, item);
            Item newItem = Instantiate(item, item.transform.position + offsetX, Quaternion.identity, _itemArea);
            newItem.transform.rotation = item.transform.rotation;
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

            walker.StartWalk(Instantiate(_particleObject), walker.gameObject.AddComponent<AudioSource>(), _audioClip);
            items.Add(newItem);
        }
    }

    private Vector3 CalcItemOffsetX(Item previousItem, Item currentItem)
    {
        Vector3 offset = Vector3.zero;
        if (previousItem != null && currentItem != null)
        {
            var xExtentPrev = previousItem.Mesh.mesh.bounds.extents.x;
            var xScalePrev = previousItem.transform.localScale.x;
            var xExtentCurrent = currentItem.Mesh.mesh.bounds.extents.x;
            var xScaleCurrent = currentItem.transform.localScale.x;
            offset = new Vector3(xExtentPrev * xScalePrev + xExtentCurrent * xScaleCurrent + ItemGap, 0, 0);
        }

        return offset;
    }
    private void OnMouseDown()
    {
        if (OnClick != null)
        {
            OnClick(this.items);
        }
    }
}
