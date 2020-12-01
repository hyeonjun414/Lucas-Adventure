using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MySceneManager : MonoBehaviour
{
    public CanvasGroup Fade_img;
    float fadeDuration = 0.5f; //암전되는 시간
    public GameObject Loading;
    public Text Loading_text; //퍼센트 표시할 텍스트
    public static MySceneManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static MySceneManager instance;

    void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded; // 이벤트에 추가
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트에서 제거*
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() => {
            Loading.SetActive(false);
        })
        .OnComplete(() => {
            Fade_img.blocksRaycasts = false;
        });
    }
    public void ChangeScene(string sceneName)
    {
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() => {
            Fade_img.blocksRaycasts = true; //아래 레이캐스트 막기
    })
        .OnComplete(() => {
            StartCoroutine("LoadScene", sceneName);
            //로딩화면 띄우며, 씬 로드 시작
        });
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        yield return async;
    }
}