using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowCapsule : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            float step = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
        }       
    }



    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
