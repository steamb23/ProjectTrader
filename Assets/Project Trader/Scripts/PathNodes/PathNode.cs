using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 추후 노드 기반 길찾기에 구현 예정.
// 현재 상황에서는 아이템 노드, 도착지 추가를 위한 표식
//
[DisallowMultipleComponent]
public class PathNode : MonoBehaviour
{
    public List<PathNode> connectedNodeList;

    public float gizmoRadius = 0.05f;

    public Color gizmoColor = new Color(0.84f, 0.48f, 0.72f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        var origColor = UnityEditor.Handles.color;
        UnityEditor.Handles.color = gizmoColor;
        // 연결된 노드 그리기 (길찾기 기능 구현시 구현)
        // 노드 기즈모 그리기
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, gizmoRadius);
        UnityEditor.Handles.color = origColor;
    }
#endif
}
