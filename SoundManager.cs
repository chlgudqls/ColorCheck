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
        ////ĥ�己�����µ� �θ𵵰����� �׷��� �ۺ����� �ٲٰ� �ν�����â���� ���� �ʱ�ȭ
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
        //case�ȿ��� ���峻�� �ۿ��� �÷��̾� �÷����� �����÷��̾�
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
        //����?
        instance.sfxPlayers[instance.nextPlayer].Play();
        instance.nextPlayer = (instance.nextPlayer + 1) % instance.sfxPlayers.Length;
    }
}
