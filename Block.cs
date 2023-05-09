using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //���� ������Ʈ���� ĳ���͸� rigid�� ����
    public Rigidbody[] characters;
    Ledge ledge;
    public int type;

    void Start()
    {
        ledge = GetComponentInParent<Ledge>();
    }

    void LateUpdate()
    {
        if (transform.position.z >= 4)
        {
            //�ٽ� �ڷ� ��ġ��Ŵ
            transform.Translate(0, 0, ledge.blockSize * ledge.blockCount * -1);
            Init();
        }
    }
    public void Init()
    {
        type = Random.Range(0, characters.Length);
        for (int index = 0; index < characters.Length; index++)
        {
            characters[index].gameObject.SetActive(type == index);
        }
        StartCoroutine(InitPhysics());
    }
    //hit�ǰ� �������ֵ� ���ġ
    IEnumerator InitPhysics()
    {
        characters[type].isKinematic = true;
        yield return new WaitForFixedUpdate();

        characters[type].velocity = Vector3.zero;
        characters[type].angularVelocity = Vector3.zero;
        //�̰� ���ϸ� ��������
        yield return new WaitForFixedUpdate();

        //�̰� �۷ι��̸� ������
        characters[type].transform.localPosition = Vector3.zero;
        characters[type].transform.localRotation = Quaternion.identity;
    }
    //�������� �ƴ��� üũ
    //selectType �� ����
    //��ư���� ���ߴ°Ű����� ���ؼ� Ʈ��� �Լ���ȯ
    public bool Check(int selectType)
    {
        bool result = type == selectType;

        if(result)
            StartCoroutine(Hit());

        //boolŸ�� �Լ��� boolŸ�� ��ȯ �Լ��� ���̽���Ǵ°���
        return result;
    }
    IEnumerator Hit()
    {

        //��������Ű�� �������ӽ�
        //����� hit�� �����������̶� �̷������� ���ݵθ� ���� �Ⱥε���
        characters[type].isKinematic = false;

        //hit�� ���� ȣ�����
        int ran = Random.Range(0, 2);
        Vector3 forceVec = Vector3.zero;
        Vector3 torqueVec = Vector3.zero;
        //�������겨���� hit����ɶ� üũ����
        switch (ran)
        {
            case 0:
                forceVec = (Vector3.right + Vector3.up) * 7;
                torqueVec = (Vector3.left + Vector3.down) * 7;
                characters[type].AddForce(forceVec,ForceMode.Impulse);
                characters[type].AddTorque(torqueVec, ForceMode.Impulse);
                break;
            case 1:
                forceVec = (Vector3.left + Vector3.up) * 7;
                torqueVec = (Vector3.back + Vector3.up) * 7;
                characters[type].AddForce(forceVec, ForceMode.Impulse);
                characters[type].AddTorque(torqueVec, ForceMode.Impulse);
                break;
        }
        yield return new WaitForFixedUpdate();
    }
}
