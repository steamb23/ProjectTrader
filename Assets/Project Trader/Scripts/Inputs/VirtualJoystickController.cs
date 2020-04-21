using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs
{
    public class VirtualJoystickController : MonoBehaviour
    {
        public Vector2 Velocity
        {
            get;
            private set;
        }

        public GameObject joystick;
        public float radius = 10;

        private GameObject stick;
        private Vector2 firstPos;

        private float uiScale;

        private void Awake()
        {
            // 조이스틱 매퍼의 소멸, null 여부를 체크
            if (JoystickMapper.VirtualJoystickController == null)
            {
                JoystickMapper.SetVirtualJoystickController(this);
            }
        }

        private void Start()
        {
            stick = joystick.transform.Find("Stick").gameObject;
        }

        //// Update is called once per frame
        //private void Update()
        //{
        //}

        public void DragBegin(BaseEventData eventData)
        {
            var data = eventData as PointerEventData;
            joystick.SetActive(true);
            // 좌표 초기화
            joystick.transform.position = firstPos = data.position;

            // UI 스케일 설정
            uiScale = transform.parent.localScale.x;

            //Debug.Log("Joystick On");
        }

        public void Drag(BaseEventData eventData)
        {
            var data = eventData as PointerEventData;

            // 상대 벡터
            Vector2 stickPos = data.position - firstPos;

            var stickPosNorm = stickPos.normalized;
            var distance = stickPos.magnitude / uiScale;

            // 거리 제한
            if (distance < radius)
            {
                stickPos = stickPosNorm * distance;
                Velocity = stickPos / radius;
            }
            else
            {
                stickPos = stickPosNorm * radius;
                Velocity = stickPosNorm;
            }

            stick.transform.localPosition = stickPos;
            //stick.transform.position = stickPos + firstPos;
            //Debug.Log(Velocity.magnitude);
            //Debug.Log(Velocity);
        }

        public void DragEnd()
        {
            // 비활성화
            stick.transform.localPosition = Vector3.zero;
            Velocity = Vector2.zero;
            joystick.SetActive(false);
            //Debug.Log("Joystick Off");
        }
    }
}
