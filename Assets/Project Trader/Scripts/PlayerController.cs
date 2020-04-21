using Inputs;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public float speed = 10;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var joystickVector = JoystickMapper.GetAxis();
        rigidBody.velocity = joystickVector * speed;

        // 애니메이션 예제
        var currentSpeed = joystickVector.magnitude;
        if (currentSpeed > 0)
        {
            skeletonAnimation.transform.localScale = new Vector3(joystickVector.x < 0 ? -1 : 1, 1, 1);
            if (currentSpeed > 0.5f)
            {
                skeletonAnimation.AnimationName = "run";
                skeletonAnimation.timeScale = currentSpeed;
            }
            else
            {
                skeletonAnimation.AnimationName = "walk";
                skeletonAnimation.timeScale = currentSpeed / 0.5f;
            }
        }
        else
        {
            skeletonAnimation.AnimationName = "idle";
            skeletonAnimation.timeScale = 1;
        }
    }
}
