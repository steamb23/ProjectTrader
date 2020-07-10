using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

class JudgeAi : MonoBehaviour
{
    public GameObject targetObject;
    public Transform targetTransform;

    public EmployeeAnimation animation;

    // A* 프로젝트
    [SerializeField]IAstarAI astarAI;

    private void Awake()
    {
        astarAI = GetComponent<IAstarAI>();

        animation = GetComponent<EmployeeAnimation>();
    }

    public void SetTarget(Transform targetTransform)
    {
        nextTime = time = 0;
        this.targetTransform = targetTransform;
        this.targetObject = targetTransform.gameObject;

        astarAI.destination = targetTransform.position;
        astarAI.SearchPath();
    }

    // 전체 행동 FSM은 별도로 구현하지 않고 ReviewManager에서 관리

    float time;
    float nextTime = 0;

    private void Update()
    {
        if ((astarAI.destination - transform.position).sqrMagnitude < 0.0001f)
        {
            targetTransform = null;
        }

        if (targetTransform == null)
        {
            time += Time.deltaTime;

            if (time > nextTime)
            {
                RandomDirection();
                time = 0;
                nextTime = UnityEngine.Random.Range(0.1f, 2f);
            }
        }
    }

    public void RandomDirection()
    {
        animation.direction = (ProjectTrader.FourDirection)UnityEngine.Random.Range(0, 4);
    }
}