using Pathfinding;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        /// 카운터 찾기
        /// </summary>
        FindCounter,
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
    public SpriteRenderer itemSpriteRenderer;
    public VisitorAnimation visitorAnimation;

    public PathNode targetNode;
    public ItemNode targetItemNode;

    public int targetItemNodeIndex;

    public PathNodeManager pathNodeManager;
    public VisitorManager visitorManager;

    [UnityEngine.SerializeField]
    private List<ProjectTrader.Datas.Item> wishItems = new List<ProjectTrader.Datas.Item>();

    public List<ProjectTrader.Datas.Item> WishItems => this.wishItems;

    public AiState state;

    // A* 프로젝트
    IAstarAI astarAI;

    float waitTime = 0;

    // 카운터 대기 번호
    int counterWaitNumber = 0;

    static int debugIndex = 0;
    void Start()
    {
        // 초기 상태 지정
        state = AiState.Finding;
        astarAI = GetComponent<IAstarAI>();
        if (pathNodeManager == null)
            pathNodeManager = FindObjectOfType<PathNodeManager>();
        if (visitorManager == null)
            visitorManager = FindObjectOfType<VisitorManager>();
        if (itemSpriteRenderer == null)
            itemSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        if (visitorAnimation == null)
            visitorAnimation = GetComponentInChildren<VisitorAnimation>();

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Appear());

        IEnumerator Appear()
        {
            var color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
            yield return null;
            while (true)
            {
                color = spriteRenderer.color;
                color.a += 1 / 0.2f * Time.deltaTime;
                if (color.a >= 1)
                {
                    color.a = 1;
                    break; // 루프 탈출
                }
                else
                {
                    spriteRenderer.color = color;
                }
                yield return null;
            }
        }

#if UNITY_EDITOR
        name += debugIndex++.ToString();
#endif
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

        // 아이템 스프라이트
        if (wishItems.Count > 0)
        {
            itemSpriteRenderer.sprite = ItemSpriteData.GetItemSprite(wishItems[0].Code);
        }
        else
        {
            itemSpriteRenderer.sprite = null;
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
            case AiState.FindCounter:
                FindCounter();
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
        //Debug.Log("Finding");
        // 랜덤 아이템 선택
        targetItemNodeIndex = UnityEngine.Random.Range(0, pathNodeManager.itemNodes.Count);

        // TODO:해당 위치에 캐릭터가 존재하면 다시 선택하도록 수정
        if (pathNodeManager.ItemOccupancyList[targetItemNodeIndex] != null)
        {
            state = AiState.Finding;
            return;
        }

        SetTarget(pathNodeManager.itemNodes[targetItemNodeIndex]);
        targetItemNode = targetNode as ItemNode;
        // 아이템 점유
        pathNodeManager.ItemOccupancyList[targetItemNodeIndex] = this;

        state = AiState.Finded;
    }

    void Finded()
    {
        //Debug.Log("Finded");
        waitTime = UnityEngine.Random.Range(0f, 1f);

        state = AiState.Thinking;
    }

    void Thinking()
    {
        //Debug.Log("Thinking");
        visitorAnimation.direction = ProjectTrader.FourDirection.Up;
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            // 구매 확률
            float purchasingProbability = 0.3f;

            var random = UnityEngine.Random.Range(0f, 1f);

            if (random < purchasingProbability)
            {
                var displayedItem = targetItemNode.displayedItem;

                WishItems.Add(new Item()
                {
                    Code = displayedItem.Item.Code,
                    Count = 1
                });
                state = AiState.FindCounter;
            }
            else
            {
                state = AiState.Discard;
            }

            // 아이템 점유 해제
            pathNodeManager.ItemOccupancyList[targetItemNodeIndex] = null;
        }
    }

    Coroutine findCounterCoroutine;
    void FindCounter()
    {
        findCounterCoroutine = StartCoroutine(FindCounterCoroutine());
        state = AiState.ToCounter;
    }

    IEnumerator FindCounterCoroutine()
    {
        while (true)
        {
            // 대기열 찾기
            counterWaitNumber = pathNodeManager.WaitQueue.Count - 1;
            if (counterWaitNumber >= 0)
            {
                SetTarget(pathNodeManager.waitNodes[counterWaitNumber]);
            }
            else
            {
                SetTarget(targetNode = pathNodeManager.counterNode);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void ToCounter()
    {
        // 계산원 방향 바라보기
        visitorAnimation.direction = Utils.VelocityToDirection(this.transform.position - pathNodeManager.counterNode.transform.position + pathNodeManager.casherDirection);
        // 이동 완료후 처리
        StopCoroutine(findCounterCoroutine);
        var currentWaitNumber = pathNodeManager.WaitQueue.Count - 1;
        //Debug.Log($"currentWaitNumber = {currentWaitNumber}, counterWaitNumber = {counterWaitNumber}");
        // 기억했던 대기번호와 현재 대기번호가 상이하면 새로운 대기 노드 검색
        if (currentWaitNumber != counterWaitNumber)
        {
            Debug.Log("ToCounter 대기노드 검색");
            counterWaitNumber = currentWaitNumber;
            if (counterWaitNumber >= 0)
            {
                SetTarget(pathNodeManager.waitNodes[counterWaitNumber]);
            }
            else
            {
                SetTarget(targetNode = pathNodeManager.counterNode);
            }
        }
        pathNodeManager.WaitQueue.Enqueue(this);
        state = AiState.CounterWait;
    }

    void CounterWait()
    {
        visitorAnimation.direction = FourDirection.Up;
        if (pathNodeManager.WaitQueue.Peek() == this)
        {

            Debug.Log($"{name} 계산");
            state = AiState.Buy;
        }
    }

    void Buy()
    {
        //Debug.Log("Buy");
        // 캐릭터 상호작용

        //TODO:'FindObjectOfType<CashierAi>()'대신에 매니저 클래스를 이용하여 최적화 필요함.
        var cashierAI = FindObjectOfType<CashierAi>();

        // 계산원이 없으면 무한 대기
        if (cashierAI == null)
            return;

        state = AiState.Buying;

        // 알바가 있으면
        if (cashierAI.IsEmployee)
        {
            cashierAI.ItemDeal(this, ItemDealCallback);
        }
        // 없으면
        else
        {
            // 유저 상호작용 요청
            FloatingButton.Create(cashierAI.transform, new Vector3(0.2f, 0), null, (button) =>
             {
                 Destroy(button.gameObject);

                 var timer = FloatingTimer.Create(cashierAI.transform, new Vector3(0.2f, 0), 10, (floatingTimer) =>
                 {
                     floatingTimer.FadeoutWithDestory();

                     ItemDealCallback();
                 });
                 //timer.boostRatio = 2;
             });
        }

        void ItemDealCallback()
        {
            // 상호작용이 끝나면 
            // 가격 처리
            int price = 0;
            FindObjectOfType<SoundControl>().PaySound();
            PlayData.CurrentData.Awareness += 1;
            FindObjectOfType<Uiup>().Upawareness(1);
            foreach (var item in WishItems)
            {
                price += item.GetData().SellPrice * item.Count;
                FindObjectOfType<Uiup>().Upmoney(price);
            }

            // 구매할 아이템 초기화
            WishItems.Clear();

            state = AiState.Buyed;
        }
    }

    void Buying()
    {
        // Buy에서 ItemDeal 콜백에서 Buyed로 변환함.
    }

    void Buyed()
    {
        //Debug.Log("Buyed");
        pathNodeManager.WaitQueue.Dequeue();
        //다른 visitor들 지정해주기
        int i = 0;
        foreach (var visitor in pathNodeManager.WaitQueue)
        {
            if (i == 0)
            {
                visitor.SetTarget(pathNodeManager.counterNode);
            }
            else
            {
                visitor.SetTarget(pathNodeManager.waitNodes[i - 1]);
            }
            i++;
        }
        SetTarget(pathNodeManager.exitNode);
        state = AiState.Exit;
    }

    void Discard()
    {
        //Debug.Log("Discard");
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
        //Debug.Log("Exit");
        // 파괴절차
        //Destroy(this.gameObject, 1);

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Disappear());

        state = AiState.None;

        IEnumerator Disappear()
        {
            while (true)
            {
                var color = spriteRenderer.color;
                color.a -= 1 / 0.2f * Time.deltaTime;
                if (color.a <= 0)
                {
                    Destroy(gameObject);
                    break; // 루프 탈출
                }
                else
                {
                    spriteRenderer.color = color;
                }
                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        visitorManager.visitors.Remove(this);
    }
}
