using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class VisitorAi : MonoBehaviour
{
    public enum AiState
    {
        None,
        /// <summary>
        /// 아이템 찾기
        /// </summary>
        Finding,
        /// <summary>
        /// 아이템 찾아서 이동중
        /// </summary>
        Finded,
        /// <summary>
        /// 구매 의사 결정
        /// </summary>
        Thinking,
        /// <summary>
        /// 카운터로 가기
        /// </summary>
        ToCounter,
        /// <summary>
        /// 카운터에서 대기
        /// </summary>
        CounterWait,
        /// <summary>
        /// 아이템 구매
        /// </summary>
        Buy,
        /// <summary>
        /// 아이템 구매
        /// </summary>
        Buying,
        /// <summary>
        /// 아이템 구매함
        /// </summary>
        Buyed,
        /// <summary>
        /// 아이템 포기
        /// </summary>
        Discard,
        /// <summary>
        /// 나가기
        /// </summary>
        Exit
    }

    public PathNode targetNode;

    public int targetItemIndex;

    public PathNodeManager pathNodeManager;

    // 추후 아이템 클래스 형식으로 변경
    object item = null;

    public AiState state;

    // A* 프로젝트
    IAstarAI astarAI;

    float waitTime = 0;

    // 카운터 대기 번호
    int counterWaitNumber = 0;

    void Start()
    {
        // 초기 상태 지정
        state = AiState.Finding;
        astarAI = GetComponent<IAstarAI>();
        if (pathNodeManager == null)
            pathNodeManager = FindObjectOfType<PathNodeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetNode == null)
        {
            Action();
        }
        if ((astarAI.destination - transform.position).sqrMagnitude < 0.0001f)
        {
            targetNode = null;
        }
    }

    void SetTarget(PathNode targetNode)
    {
        this.targetNode = targetNode;

        astarAI.destination = targetNode.transform.position;
        astarAI.SearchPath();
    }

    void Action()
    {
        // 유한 상태 머신
        switch (state)
        {
            case AiState.Finding:
                Finding();
                break;
            case AiState.Finded:
                Finded();
                break;
            case AiState.Thinking:
                Thinking();
                break;
            case AiState.ToCounter:
                ToCounter();
                break;
            case AiState.CounterWait:
                CounterWait();
                break;
            case AiState.Buy:
                Buy();
                break;
            case AiState.Buying:
                Buying();
                break;
            case AiState.Buyed:
                Buyed();
                break;
            case AiState.Discard:
                Discard();
                break;
            case AiState.Exit:
                Exit();
                break;
        }
    }

    void Finding()
    {
        Debug.Log("Finding");
        // 랜덤 아이템 선택
        targetItemIndex = UnityEngine.Random.Range(0, pathNodeManager.itemNodes.Count);

        // TODO:해당 위치에 캐릭터가 존재하면 다시 선택하도록 수정
        if (pathNodeManager.ItemOccupancyList[targetItemIndex] != null)
        {
            state = AiState.Finding;
            return;
        }

        SetTarget(pathNodeManager.itemNodes[targetItemIndex]);
        // 아이템 점유
        pathNodeManager.ItemOccupancyList[targetItemIndex] = this;

        state = AiState.Finded;
    }

    void Finded()
    {
        Debug.Log("Finded");
        waitTime = UnityEngine.Random.Range(0f, 1f);

        state = AiState.Thinking;
    }

    void Thinking()
    {
        Debug.Log("Thinking");
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            // 구매 확률
            float purchasingProbability = 0.3f;

            var random = UnityEngine.Random.Range(0f, 1f);

            if (random < purchasingProbability)
            {
                state = AiState.ToCounter;
            }
            else
            {
                state = AiState.Discard;
            }

            // 아이템 점유 해제
            pathNodeManager.ItemOccupancyList[targetItemIndex] = null;
        }
    }

    void ToCounter()
    {
        Debug.Log("ToCounter");
        // 대기열 찾기
        counterWaitNumber = pathNodeManager.WaitQueue.Count - 1;
        if (counterWaitNumber >= 0)
        {
            SetTarget(pathNodeManager.waitNodes[counterWaitNumber]);
        }
        else
        {
            SetTarget(targetNode = pathNodeManager.counterNode);
            //state = AiState.Buy;
        }
        pathNodeManager.WaitQueue.Enqueue(this);
        state = AiState.CounterWait;
    }

    void CounterWait()
    {
        if (pathNodeManager.WaitQueue.Peek() == this)
        {
            state = AiState.Buy;
        }
    }

    void Buy()
    {
        Debug.Log("Buy");
        // 캐릭터 상호작용
        // 프로토타입에서는 대기로 대체
        waitTime = 1;
        state = AiState.Buying;
    }

    void Buying()
    {
        Debug.Log("Buying");
        // 캐릭터 상호작용
        // 프로토타입에서는 대기로 대체
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            state = AiState.Buyed;
        }
    }

    void Buyed()
    {
        Debug.Log("Buyed");
        pathNodeManager.WaitQueue.Dequeue();
        //다른 visitor들 지정해주기
        int i = 0;
        foreach(var visitor in pathNodeManager.WaitQueue)
        {
            if (i == 0)
            {
                visitor.SetTarget(pathNodeManager.counterNode);
            }
            else
            {
                visitor.SetTarget(pathNodeManager.waitNodes[i-1]);
            }
            i++;
        }
        SetTarget(pathNodeManager.exitNode);
        state = AiState.Exit;
    }

    void Discard()
    {
        Debug.Log("Discard");
        // 판매 포기 확률
        float discardProbability = 0.01f;

        var random = UnityEngine.Random.Range(0f, 1f);

        if (random < discardProbability)
        {
            SetTarget(pathNodeManager.exitNode);
            state = AiState.Exit;
        }
        else
        {
            state = AiState.Finding;
        }
    }

    void Exit()
    {
        Debug.Log("Exit");
        // 파괴절차
        Destroy(this.gameObject, 1);
        state = AiState.None;
    }
}
