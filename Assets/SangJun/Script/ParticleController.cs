using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem particleClick;

    [SerializeField] Transform MainCanvas;
    [SerializeField] GameObject objParticle;
    [SerializeField] GameObject prefabParticle;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (MainCanvas == null)
        {
            MainCanvas = GameObject.Find("Canvas").transform;
        }
        // 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector3 mousePosition = Input.mousePosition;
            Vector3 spawnPosition = new Vector3(mousePosition.x, mousePosition.y, 0);

            // 캔버스에 새로운 파티클 생성
            CreateParticleAtPosition(spawnPosition);
        }
    }
    void CreateParticleAtPosition(Vector3 position)
    {
        // 파티클 인스턴스 생성
        GameObject objParticle = Instantiate(prefabParticle, MainCanvas);
        ParticleSystem newParticle = objParticle.transform.GetChild(0).GetComponent<ParticleSystem>();

        // 파티클의 위치를 설정
        objParticle.transform.position = position;

        // 파티클 재생
        newParticle.Play();

        // 파티클이 종료되면 자동으로 삭제
        Destroy(newParticle.gameObject, newParticle.main.duration);
    }
}