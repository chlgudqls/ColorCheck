using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;

    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayers;
    public int nextPlayer;

    public AudioClip[] hitClip;
    public AudioClip startClip;
    public AudioClip overClip;
    public AudioClip failClip;
    void Start()
    {
        //bgmPlayer = GetComponent<AudioSource>();
        ////칠드런만썻는데 부모도가져옴 그래서 퍼블릭으로 바꾸고 인스펙터창에서 직접 초기화
        //sfxPlayers = GetComponentsInChildren<AudioSource>();
        print(sfxPlayers.Length);
        instance = this;
    }


    public static void BgmStart()
    {
        instance.bgmPlayer.Play();
    }
    public static void BgmStop()
    {
        instance.bgmPlayer.Stop();
    }
    public static void PlaySound(string name)
    {
        //case안에는 사운드내용 밖에는 플레이어 플레이후 다음플레이어
        switch(name)
        {
            case "Start":
                instance.sfxPlayers[instance.nextPlayer].clip = instance.startClip;
                break;
            case "Over":
                instance.sfxPlayers[instance.nextPlayer].clip = instance.overClip;
                break;
            case "Hit":
                int ran = Random.Range(0, instance.hitClip.Length);
                instance.sfxPlayers[instance.nextPlayer].clip = instance.hitClip[ran];
                break;
            case "Fail":
                instance.sfxPlayers[instance.nextPlayer].clip = instance.failClip;
                break;
        }
        //뭔말?
        instance.sfxPlayers[instance.nextPlayer].Play();
        instance.nextPlayer = (instance.nextPlayer + 1) % instance.sfxPlayers.Length;
    }
}
