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

    [SerializeField]
    private MeshFilter _mesh;

    public string Name => _name;

    public float Price => (float) Math.Round(_price, 2);

    public MeshFilter Mesh
    {
        get => _mesh;
        private set => _mesh = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Mesh == null)
        {
            Mesh = GetComponent<MeshFilter>();
        }
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
