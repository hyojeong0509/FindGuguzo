using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene_Cloud : MonoBehaviour
{
    float minSpeed = 1f;
    float maxSpeed = 2.5f;
    float nowSpeed = 0;

    void Start()
    {
        // 랜덤한 변수로 속도 설정하기
        nowSpeed = Random.Range(minSpeed, maxSpeed);
    }


    void Update()
    {
        // 속도 변수로 구름 왼쪽으로 이동시키기
        transform.Translate(Vector3.left * nowSpeed * Time.deltaTime);

        // 일정 구간에 다다르면 디스트로이 하기 
        if(transform.position.x < -5f)
        {
            Destroy(gameObject);
        }
    }
}