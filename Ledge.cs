using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    public int blockCount;
    public float blockSize;
    Block[] blocks;
    public int nowBlock;
    void Start()
    {
        
        blocks = GetComponentsInChildren<Block>();
    }

    public void Align()
    {
        blockCount = blocks.Length;
        if (blockCount == 0)
        {
            Debug.Log("Not found Blocks");
            return;
        }
        //0번째 블럭기준으로 위치 인덱스곱해서 위치 변경시킴
        blockSize = blocks[0].GetComponentInChildren<BoxCollider>().transform.localScale.z;

        for(int index=0; index < blockCount; index++)
        {
            blocks[index].transform.Translate(0, 0,index * blockSize * -1);
            blocks[index].Init();
        }
    }
    //한번 호출이고 2거리 동안 이동하는 거임 null로 호출주기 만들고
    IEnumerator Move()
    {
        float nextZ = transform.position.z + 2;

        while (transform.position.z < nextZ)
        {
            yield return null;
            //결과값에 도달하는과정을 Time.deltaTime * 20f만큼 쪼갠거
            //translate 이동하는 속도인가
            //시간에 곱하는숫자는 이동하는속도 프레임마다 호출하지만 속도가 발목잡는다고 생각하면됨
            transform.Translate(0, 0, Time.deltaTime * 19f);
        }
        //루프빠져나오면 그냥 위치시키는거 결과값의 위치 
        transform.position = Vector3.forward * nextZ;
        //무브가 호출될때마다 맨앞의 인덱스를 구함
        //nowBlock = (nowBlock + 1) % blockCount;
    }
    //[ContextMenu("Do Move")]
    public void Select(int selectType)
    {
        //현재블록 - 맨앞의블록을 체크할 인덱스를 구해야됨
        bool result = blocks[nowBlock].Check(selectType);

        if(result)//정답
        {
            GameManager.Success();
            StartCoroutine(Move());

            //위치 옮김 위치에 따라 타이밍이 다름
            nowBlock = (nowBlock + 1) % blockCount;
        }
        else//오답
        {
            GameManager.Fail();
        }
    }
}
