using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetObject;
    public float velocityImpact = 30;
    public float speed = 0.1f;

    //private Rigidbody2D targetRigidbody;
    private Vector3 targetPosition;
    private Vector3 prevTargetPosition;
    private Vector3 targetVelocity;
    private Vector3 prevtargetVelocity;

    public void SetTargetObject(GameObject targetObject)
    {
        if (targetObject != null)
        {
            //targetRigidbody = targetObject.GetComponent<Rigidbody2D>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTargetObject(targetObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        prevTargetPosition = targetPosition;
        prevtargetVelocity = targetVelocity;

        // targetPosition 보간
        targetPosition += (targetObject.transform.position - targetPosition) * speed;


        targetVelocity = targetPosition - prevTargetPosition;
        //Debug.Log($"targetVelocity {targetVelocity}");
        var interpolationTargetPosition = targetPosition;
            interpolationTargetPosition += targetVelocity * velocityImpact;

        var direction = interpolationTargetPosition - this.transform.position;
        direction *= speed;

        transform.Translate(direction.x, direction.y, 0);
    }
}
