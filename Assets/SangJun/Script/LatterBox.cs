using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LatterBox : MonoBehaviour
{
    public static LatterBox instance;

    [SerializeField] Camera subCam;
    private ScreenOrientation lastOrientation;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private float targetWidth = 760f; // 기준 해상도의 가로값
    [SerializeField] private float targetHeight = 1280f; // 기준 해상도의 세로값

    private void Start()
    {    
        // 시작 시 화면 방향을 저장합니다.
        lastOrientation = Screen.orientation;
    }
    void Update()
    {
        // 화면 방향이 변경되었을 때만 레터박스를 다시 설정합니다.
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            SetLatterBox(); // 레터박스 다시 그리기
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // AdjustCameraSize();
        SetLatterBox();
    }
    private void AdjustCameraSize()
    {
        // 기준 해상도의 비율
        float targetAspect = targetWidth / targetHeight;

        // 현재 기기의 화면 비율
        float screenAspect = (float)Screen.width / Screen.height;

        // 카메라의 orthographicSize 조정
        Camera camera = Camera.main;

        if (screenAspect < targetAspect) // 화면 비율이 더 좁음
        {
            float scaleFactor = targetAspect / screenAspect;
            camera.orthographicSize = (targetHeight / 200f) * scaleFactor;
        }
        else // 화면 비율이 같거나 더 넓음
        {
            camera.orthographicSize = targetHeight / 200f;
        }
    }

    void SetLatterBox()
    {
        Camera camera = Camera.main;
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / (760f / 1280f); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }
    void OnPostRender()
    {
        GL.PushMatrix();
        GL.LoadOrtho(); // 정규화된 좌표계(0~1)로 변환
        GL.Begin(GL.QUADS);
        GL.Color(Color.black); // 검은색으로 덮기

        // 상단 레터박스
        GL.Vertex3(0, 1 - Camera.main.rect.yMax, 0);
        GL.Vertex3(1, 1 - Camera.main.rect.yMax, 0);
        GL.Vertex3(1, 1, 0);
        GL.Vertex3(0, 1, 0);

        // 하단 레터박스
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(1, 0, 0);
        GL.Vertex3(1, Camera.main.rect.yMin, 0);
        GL.Vertex3(0, Camera.main.rect.yMin, 0);

        // 좌측 레터박스 (필요한 경우)
        if (Camera.main.rect.xMin > 0)
        {
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(Camera.main.rect.xMin, 0, 0);
            GL.Vertex3(Camera.main.rect.xMin, 1, 0);
            GL.Vertex3(0, 1, 0);
        }

        // 우측 레터박스 (필요한 경우)
        if (Camera.main.rect.xMax < 1)
        {
            GL.Vertex3(Camera.main.rect.xMax, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(Camera.main.rect.xMax, 1, 0);
        }

        GL.End();
        GL.PopMatrix();
    }
}
