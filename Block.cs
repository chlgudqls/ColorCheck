using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //게임 오브젝트말고 캐릭터를 rigid로 구분
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
            //다시 뒤로 위치시킴
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
    //hit되고 떨어진애들 재배치
    IEnumerator InitPhysics()
    {
        characters[type].isKinematic = true;
        yield return new WaitForFixedUpdate();

        characters[type].velocity = Vector3.zero;
        characters[type].angularVelocity = Vector3.zero;
        //이거 안하면 문제생김
        yield return new WaitForFixedUpdate();

        //이거 글로벌이면 겹쳐짐
        characters[type].transform.localPosition = Vector3.zero;
        characters[type].transform.localRotation = Quaternion.identity;
    }
    //정답인지 아닌지 체크
    //selectType 이 뭔지
    //버튼으로 맞추는거같은데 비교해서 트루면 함수반환
    public bool Check(int selectType)
    {
        bool result = type == selectType;

        if(result)
            StartCoroutine(Hit());

        //bool타입 함수라서 bool타입 반환 함수도 같이실행되는건지
        return result;
    }
    IEnumerator Hit()
    {

        //물리연산키고 한프레임쉼
        //무브랑 hit가 한프레임차이라서 이런식으로 간격두면 서로 안부딪힘
        characters[type].isKinematic = false;

        //hit이 언제 호출될지
        int ran = Random.Range(0, 2);
        Vector3 forceVec = Vector3.zero;
        Vector3 torqueVec = Vector3.zero;
        //물리연산꺼놔서 hit실행될땐 체크해제
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
