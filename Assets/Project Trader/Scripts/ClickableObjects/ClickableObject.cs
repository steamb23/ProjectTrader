using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ClickableObject : MonoBehaviour
{
    // 드래그 오차
    // 드래그되는 상황에서도 클릭 판정 가능하려면 무한대로 설정
    [SerializeField] float dragSafeDistance = 10;

    // 드래그한 거리의 제곱
    float sqrDragDistance = 0f;
    Vector3 previousMousePosition;



    protected virtual void OnMouseDown()
    {
        sqrDragDistance = 0;
        previousMousePosition = Input.mousePosition;
    }
    protected virtual void OnMouseOver()
    {
        var sqrDragDistance = (Input.mousePosition - previousMousePosition).sqrMagnitude;
        if (this.sqrDragDistance < sqrDragDistance)
            this.sqrDragDistance = sqrDragDistance;
    }

    protected virtual void OnMouseUp()
    {
        // UI 겹침 체크
        bool isOver = false;

        var graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();
        foreach (var graphicRaycaster in graphicRaycasters)
        {
            var eventData = new PointerEventData(null)
            {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, results);

            // 결과갯수가 0이상이고 첫번째 항목이 카메라 컨트롤러가 아니면
            if (results.Count > 0 && results[0].gameObject.layer != 9) // 9 = CameraController
            {
                isOver = true;
            }
        }

        // 오브젝트 겹침 체크
        var result = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f);

        // 체크된 오브젝트가 현재 오브젝트이고
        if (result.collider!=null && result.collider.gameObject == this.gameObject)
            // UI에 겹치지 않았으며
            if (!isOver)
                // 마우스 드래그가 허용범위 내에서 이루어졌을 경우
                if (sqrDragDistance < dragSafeDistance * dragSafeDistance)
                    Click();
    }

    public abstract void Click();
}
