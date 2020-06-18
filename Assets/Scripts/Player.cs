using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _rotationSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController != null)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * _rotationSpeed, 0);

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            _characterController.SimpleMove(forward * _speed * Input.GetAxis("Vertical"));

        }
    }
}
