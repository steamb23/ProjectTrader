using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetObject;
    public Vector2 offset = Vector2.zero;
    public float velocityImpact = 30;
    public float speed = 0.1f;

    //private Rigidbody2D targetRigidbody;
    private Vector3 targetPosition;
    private Vector3 prevTargetPosition;
    private Vector3 targetVelocity;
    private Vector3 interpolationTargetPosition;
    //private Vector3 prevtargetVelocity;

    //public void SetTargetObject(GameObject targetObject)
    //{
    //    if (targetObject != null)
    //    {
    //        //targetRigidbody = targetObject.GetComponent<Rigidbody2D>();
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = targetObject.transform.position - targetPosition + (Vector3)offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var interpolatedSpeed = speed * Time.deltaTime;

        prevTargetPosition = targetPosition;
        //prevtargetVelocity = targetVelocity;

        // targetPosition 보간
        targetPosition += (targetObject.transform.position - targetPosition + (Vector3)offset) * interpolatedSpeed;


        targetVelocity = targetPosition - prevTargetPosition;
        //Debug.Log($"targetVelocity {targetVelocity}");
        interpolationTargetPosition = targetPosition;
        interpolationTargetPosition += targetVelocity * velocityImpact;

        var direction = interpolationTargetPosition - this.transform.position;
        direction *= interpolatedSpeed;

        transform.Translate(direction.x, direction.y, 0);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            var origColor = UnityEditor.Handles.color;

            Gizmos.DrawIcon(interpolationTargetPosition, "Camera Gizmo", true, Color.gray);
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawLine(targetObject.transform.position + (Vector3)offset, interpolationTargetPosition);
            UnityEditor.Handles.color = Color.gray;
            UnityEditor.Handles.DrawLine(transform.position, interpolationTargetPosition);

            UnityEditor.Handles.color = origColor;
        }
    }
#endif
}
