using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorManager : MonoBehaviour
{
    [System.Serializable]
    public struct VisitorInfo
    {
        public VisitorAi visitor;
        public float priority;
    }

    [SerializeField] GameDateTimeManager gameDateTimeManager;

    public List<VisitorInfo> visitorInfos;
    public PathNodeManager pathNodeManager;
    [Tooltip("초당 들어올 확률")]
    // 매프레임 들어오게 하려면 60, 즉 600%이상이 필요함.
    public float enterProbability = 1f;

    [Space]
    public float maxVisitor;
    public List<VisitorAi> visitors;

    // Start is called before the first frame update
    void Start()
    {
        if (pathNodeManager == null)
        {
            pathNodeManager = FindObjectOfType<PathNodeManager>();
        }

        if (gameDateTimeManager == null)
        {
            gameDateTimeManager = FindObjectOfType<GameDateTimeManager>();
        }

        maxVisitor = pathNodeManager.itemNodes.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (visitors.Count < maxVisitor && !gameDateTimeManager.IsStopped)
        {
            // 랜덤
            var random = Random.Range(0, 1f);
            if ((enterProbability * Time.deltaTime) > random)
            {
                var visitorInfo = PeekVisitorInfo();
                var obj = Instantiate(visitorInfo.visitor);
                obj.transform.position = pathNodeManager.exitNode.transform.position;
                visitors.Add(obj);

                // 손님 방문 퀘스트 트리거
                ProjectTrader.QuestManager.Trigger(ProjectTrader.Datas.QuestData.GoalType.VisitorCount, 1);
            }
        }
    }

    // 목록 중에서 랜덤으로 가져오기
    VisitorInfo PeekVisitorInfo()
    {
        (var priorityTotal, var prioritySections) = CreateProbabilityTable();

        var random = Random.Range(0, priorityTotal);

        for (int i = 0; i < prioritySections.Length; i++)
        {
            if (random < prioritySections[i])
            {
                return visitorInfos[i];
            }
        }

        // 오류
        throw new System.ApplicationException("PeekVisitorInfo 랜덤 판정 실패");
    }

    (float priorityTotal, float[] prioritySections) CreateProbabilityTable()
    {
        float priorityTotal = 0;
        float[] prioritySections = new float[visitorInfos.Count];

        // 확률표 생성
        for (int i = 0; i < visitorInfos.Count; i++)
        {
            var visitorInfo = visitorInfos[i];
            prioritySections[i] = priorityTotal += visitorInfo.priority;
        }

        return (priorityTotal, prioritySections);
    }
}
