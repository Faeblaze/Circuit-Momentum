using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speedBoostSpeed;

    public float acceleration = 8;
    public float turnSpeed = 5;

    private Quaternion targetRotation;
    private new Rigidbody rigidbody;

    // Use this for initialization
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        SetRotationPoint();

    }

    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
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
        float speed = rigidbody.velocity.magnitude / 1000;

        float accelerationInput = acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        rigidbody.AddRelativeForce(Vector3.forward * accelerationInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(speed, -1, 1) * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            rigidbody.AddForce(other.transform.right * speedBoostSpeed);
        }
    }
}
