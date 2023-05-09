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
    //�ڷ�ƾ�� static�Լ����� ������� static���� �ٲ㼭 ���
    //���Ӹ޴��� ������ �����޸𸮿� �Űܼ� �����Լ��ȿ� �������
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
        //�����Լ� ȣ��ɶ����� �ڷ�ƾ���ְ� 3���̻� ȣ������� off�޺� ����Ǵ°���

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
        //���ʱ�ȭ�ϸ� ���� �� �ʱ�ȭ������ static�� ���� start���� �ص��� �ٵ� �����ֱ� �Լ��ȿ��� ����ϰ� �ؾߵǼ�
        //�Լ��ϳ� ���� ���
        //playComboTime = 0;
        //playTime = 0;
        //isGameOver = false;
        //score = 0;
        //combo = 0;
        SceneManager.LoadScene(0);
    }
}
