using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float acceleration = 8;
    [SerializeField] float turnSpeed = 5;

    Quaternion targetRotation;
    Rigidbody _rigidbody;


	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetRotationPoint();
        
	}

    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Vector3 target = hitInfo.point;

            target.y = transform.position.y;

            Vector3 heading = target - transform.position;
            Vector3 direction = heading / heading.magnitude;

            targetRotation = Quaternion.LookRotation(direction);
            //targetRotation = Quaternion.LookRotation
            //Vector3 direction = target - transform.position;
            //float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
}
    private void FixedUpdate()
    {
        float speed = _rigidbody.velocity.magnitude / 1000;

        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        _rigidbody.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(speed, -1, 1) * Time.fixedDeltaTime);
    }
}
