using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeManager : MonoBehaviour
{
    public PathNode counterNode;
    public Vector3 casherDirection;
    public PathNode exitNode;
    public List<PathNode> waitNodes;
    public List<PathNode> itemNodes;
    public List<PathNode> judgeNodes;

    // 아이템 점유 상태
    public VisitorAi[] ItemOccupancyList { get; private set; }

    // 계산 대기열
    public Queue<VisitorAi> WaitQueue { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        WaitQueue = new Queue<VisitorAi>(waitNodes.Count);
        ItemOccupancyList = new VisitorAi[itemNodes.Count];
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(counterNode.transform.position + casherDirection, "Transform Icon");
    }
#endif
}
