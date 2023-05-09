using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Time")]
    public static float maxTime = 100f;
    public static float playTime;
    public static float maxComboTime = 3f;
    public static float playComboTime;
    public static bool isGameOver = true;

    [Header("Player")]
    public static float score;
    public static int hit;
    public static int combo;

    [Header("UI")]
    public GameObject uiInfo;
    public GameObject uiSelect;
    public GameObject uiGameOver;
    public GameObject uiGameStart;
    public GameObject uiNewRecord;

    [Header("Effect")]
    public ParticleSystem particle;
    public Animator anim;
    //코루틴이 static함수에서 쓸수없어서 static으로 바꿔서 사용
    //게임메니저 변수를 정적메모리에 옮겨서 정적함수안에 사용했음
    static GameManager instance;

    void Start()
    {
        Application.targetFrameRate = 60;
        Init();
    }

    void Init()
    {
        instance = this;
        playComboTime = 0;
        playTime = 0;
        isGameOver = true;
        score = 0;
        combo = 0;
        
        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetFloat("Score", 0);

    }

    public static void Success()
    {
        hit++;
        combo++;
        score += 1 + (combo * 0.1f);
        playComboTime = 0;
        //성공함수 호출될때마다 코루틴꺼주고 3초이상 호출없으면 off콤보 실행되는거임

        instance.particle.Play();
        instance.anim.SetTrigger("Hit");
        SoundManager.PlaySound("Hit");
    }
    public static void Fail()
    {
        playTime += 10f;
        combo = 0;
        SoundManager.PlaySound("Fail");
    }
    IEnumerator ComboTime()
    {
        while(!isGameOver)
        {
            yield return null;
            playComboTime += Time.deltaTime;

            if(playComboTime > maxComboTime)
            {
                combo = 0;
            }
        }
    }
    void Update()
    {
        if (isGameOver)
            return;

        GameTimer();
    }
    void GameTimer()
    {
        playTime += Time.deltaTime;

        if (playTime > maxTime)
            GameOver();
    }
    void GameOver()
    {
        isGameOver = true;
        uiInfo.SetActive(false);
        uiSelect.SetActive(false);
        uiGameOver.SetActive(true);

        if (score > PlayerPrefs.GetFloat("Score"))
            uiNewRecord.SetActive(true);

        PlayerPrefs.SetFloat("Score", Mathf.Max(score, PlayerPrefs.GetFloat("Score")));
        SoundManager.PlaySound("Over");
        SoundManager.BgmStop();
    }
    public void GameStart()
    {
        isGameOver = false;
        uiInfo.SetActive(true);
        uiSelect.SetActive(true);
        uiGameOver.SetActive(false);
        uiNewRecord.SetActive(false);
        uiGameStart.SetActive(false);
        instance.StartCoroutine(instance.ComboTime());
        SoundManager.PlaySound("Start");
        SoundManager.BgmStart();
    }
    public void Retry()
    {
        //씬초기화하면 원래 다 초기화되지만 static은 남음 start에서 해도됨 근데 생명주기 함수안에는 깔끔하게 해야되서
        //함수하나 만들어서 사용
        //playComboTime = 0;
        //playTime = 0;
        //isGameOver = false;
        //score = 0;
        //combo = 0;
        SceneManager.LoadScene(0);
    }
}
