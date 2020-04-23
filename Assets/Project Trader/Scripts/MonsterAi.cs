using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
    public GameObject player;
    public float distance = 10;
    public float speed = 0.04f;
    void Start()
    {

    }

    
    void Update()
    {
        PlayerTrace();
    }

    
    void PlayerTrace()
    {
        Vector3 playerpos = player.transform.position;
        //정규벡터화
        Vector3 direction = (playerpos - transform.position).normalized;
        //몬스터-플레이어 사이 거리계산
        float between = Vector3.Distance(playerpos, transform.position);
        if (between <= distance)
        {
            this.transform.position = new Vector3(transform.position.x + (direction.x * speed), transform.position.y + (direction.y * speed), 0);
        }
    }
}
