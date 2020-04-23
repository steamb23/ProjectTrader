using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
    public GameObject player;
    public float distance = 10;
    public float speed = 10;
    void Start()
    {

    }


    void FixedUpdate()
    {
        PlayerTrace();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        var origColor = UnityEditor.Handles.color;
        UnityEditor.Handles.color = new Color(1, 0, 0, 0.1f);
        UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.forward, distance);
        UnityEditor.Handles.color = new Color(1, 0, 0, 1f);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, distance);
        UnityEditor.Handles.color = origColor;
    }
#endif


    void PlayerTrace()
    {
        Vector3 playerpos = player.transform.position;
        //정규벡터화
        Vector3 direction = (playerpos - transform.position).normalized;
        //몬스터-플레이어 사이 거리계산
        float between = Vector3.Distance(playerpos, transform.position);
        Debug.Log(between);
        if (between <= distance)
        {
            this.transform.position = new Vector3(
                transform.position.x + (direction.x * speed * Time.deltaTime),
                transform.position.y + (direction.y * speed * Time.deltaTime), 0);
        }
    }
}
