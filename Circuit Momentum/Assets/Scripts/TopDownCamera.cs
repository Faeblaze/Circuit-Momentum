using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] Transform observeable;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followDamping;
    [SerializeField] float cameraHeight;

    Rigidbody _observableRigidBody;

    // Use this for initialization
    void Start()
    {
        _observableRigidBody = observeable.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (observeable == null)
            return;

        Vector3 targetPosition = observeable.position + Vector3.up * cameraHeight + _observableRigidBody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);
    }

    //  https://www.youtube.com/watch?v=pAsCXXsuB1M
}
