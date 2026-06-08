using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 5; //will automatically add more if we run out of bullets to use
    private List<GameObject> pool; //선언
    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        pool = new List<GameObject>(poolSize); //poolsize length인 빈 리스트 생성 (기본값 초기화)
         //list에 추가되면 자동으로 늘어남 poolsize에 고정된건 아님 ㅇㅇ 애초에 왜냐하면 start에 한번 실행되니

        //0~4까지 돌아서 매번 증감식 하기 전 조건문이 참일때 CreateNewObject메소드를 호출
        for (int i = 0; i<poolSize; i++)
        {
            CreateNewObject();
        }
    }
    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform); //하이어라키에 잘 보이도록 transform.position, transform.rotation을 부모 transform으로 하나로 통일
        //부모의 위치, 회전으로 istantiate(object copy) 하게 하여서 만든 obj copy는 부모 밑에 child로 추가하게됨
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in pool) // 를 검사하기
        {
            if (obj.activeSelf == false) // 제일 먼저 비활성화 된 오브젝트를 찾는다
            {
                return obj;// 그걸 가지고감
            }
        }
        return CreateNewObject(); //비활성화 된게 없으면 비활성화 된것을 createnewobject 메소드를 리턴해서 하나 만듬.
    }

}
