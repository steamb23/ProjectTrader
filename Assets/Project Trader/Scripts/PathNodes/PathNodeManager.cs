using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeManager : MonoBehaviour
{
    public PathNode counterNode;
    public PathNode exitNode;
    public List<PathNode> waitNodes;
    public List<PathNode> itemNodes;

    public Queue<VisitorAi> waitQueue;
    // Start is called before the first frame update
    void Start()
    {
        waitQueue = new Queue<VisitorAi>();
    }
}
