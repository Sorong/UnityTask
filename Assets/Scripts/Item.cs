using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Item : MonoBehaviour
{
    public delegate void OnItemClicked(Item item);

    public OnItemClicked OnClick;

    [SerializeField]
    private string _name;

    [SerializeField]
    private float _price;

    public string Name => _name;

    public float Price => (float) Math.Round(_price, 2);

    public MeshFilter Mesh { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Mesh = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (OnClick != null)
        {
            OnClick(this);
        }
    }
}
