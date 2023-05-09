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
        //0��° ���������� ��ġ �ε������ؼ� ��ġ �����Ŵ
        blockSize = blocks[0].GetComponentInChildren<BoxCollider>().transform.localScale.z;

        for(int index=0; index < blockCount; index++)
        {
            blocks[index].transform.Translate(0, 0,index * blockSize * -1);
            blocks[index].Init();
        }
    }
    //�ѹ� ȣ���̰� 2�Ÿ� ���� �̵��ϴ� ���� null�� ȣ���ֱ� �����
    IEnumerator Move()
    {
        float nextZ = transform.position.z + 2;

        while (transform.position.z < nextZ)
        {
            yield return null;
            //������� �����ϴ°����� Time.deltaTime * 20f��ŭ �ɰ���
            //translate �̵��ϴ� �ӵ��ΰ�
            //�ð��� ���ϴ¼��ڴ� �̵��ϴ¼ӵ� �����Ӹ��� ȣ�������� �ӵ��� �߸���´ٰ� �����ϸ��
            transform.Translate(0, 0, Time.deltaTime * 19f);
        }
        //�������������� �׳� ��ġ��Ű�°� ������� ��ġ 
        transform.position = Vector3.forward * nextZ;
        //���갡 ȣ��ɶ����� �Ǿ��� �ε����� ����
        //nowBlock = (nowBlock + 1) % blockCount;
    }
    //[ContextMenu("Do Move")]
    public void Select(int selectType)
    {
        //������ - �Ǿ��Ǻ���� üũ�� �ε����� ���ؾߵ�
        bool result = blocks[nowBlock].Check(selectType);

        if(result)//����
        {
            GameManager.Success();
            StartCoroutine(Move());

            //��ġ �ű� ��ġ�� ���� Ÿ�̹��� �ٸ�
            nowBlock = (nowBlock + 1) % blockCount;
        }
        else//����
        {
            GameManager.Fail();
        }
    }
}
