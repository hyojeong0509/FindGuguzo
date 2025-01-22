using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] GameObject[] SpawnPointsCloud;
    [SerializeField] GameObject[] PrefabsCloud;

    [SerializeField] GameObject objDifficultyLevel;

    [SerializeField] Text[] TextDifficultyLevel; // 0 : easy, 1 : hard
                                                  // 원하는 x와 y 범위
    float minX = -4f;
    float maxX = 4f;
    float minY = -5f;
    float maxY = 5f;

    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.PlaySFX("BtnClick");
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Init()
    {
        // objDifficultyLevel 이지 난이도 클리어 여부에 따라 ON / OFF 시키기
        if (GameManager.Instance.isHardPossible)
        {
            objDifficultyLevel.SetActive(true);
        }
        else
        {
            objDifficultyLevel.SetActive(false);
        }

        FisherYatesShuffleUnity(SpawnPointsCloud);
        FisherYatesShuffleUnity(PrefabsCloud);

        // 최초 화면 내에서 랜덤한 Vector2 값을 생성
        for (int i = 0; i < PrefabsCloud.Length *2; i++)
        {
            Vector2 randomVector = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            SpawnCloud(randomVector);
        }

        if (!GameManager.Instance.isHard)
        {
            TextDifficultyLevel[0].color = Color.white;
            TextDifficultyLevel[1].color = new Color(128f / 255f, 128f / 255f, 128f / 255f, 0.4f);
        }
        else
        {
            TextDifficultyLevel[0].color = new Color(128f / 255f, 128f / 255f, 128f / 255f, 0.4f);
            TextDifficultyLevel[1].color = Color.white;
        }

        InvokeRepeating("SpawnCloud", 0f, .5f);
    }

    void SpawnCloud()
    {
        // 시간 랜덤하게 해서 구름을 랜덤한 스폰포인트에 생성하기
        int random_Cloud = Random.Range(0, PrefabsCloud.Length);
        int random_SpawnPoint = Random.Range(0, SpawnPointsCloud.Length);

        Instantiate(PrefabsCloud[random_Cloud],
            SpawnPointsCloud[random_SpawnPoint].transform.position,
            Quaternion.identity);
    }
    void SpawnCloud(Vector2 pos)
    {
        // 시간 랜덤하게 해서 구름을 랜덤한 스폰포인트에 생성하기
        int random_Cloud = Random.Range(0, PrefabsCloud.Length);
        int random_SpawnPoint = Random.Range(0, SpawnPointsCloud.Length);

        Instantiate(PrefabsCloud[random_Cloud],
            pos,
            Quaternion.identity);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Button func
    /// </summary>
    public void Btn_SelectLevel()
    {
        GameManager.Instance.Change_Level();
        if (!GameManager.Instance.isHard)
        {
            TextDifficultyLevel[0].color = Color.white;
            TextDifficultyLevel[1].color = new Color(128f / 255f, 128f / 255f, 128f / 255f, 0.4f);
        }
        else
        {
            TextDifficultyLevel[0].color = new Color(128f / 255f, 128f / 255f, 128f / 255f, 0.4f);
            TextDifficultyLevel[1].color = Color.white;
        }
    }

    /// <summary>
    /// Shuffle array
    /// </summary>
    T[] FisherYatesShuffleUnity<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            // i까지의 범위에서 무작위 인덱스를 선택
            int j = Random.Range(0, i + 1);

            // 현재 요소와 무작위 요소를 교환
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return array;
    }
}