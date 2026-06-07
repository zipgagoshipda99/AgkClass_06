using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 5;
    private List<GameObject> pool;
    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        pool = new List<GameObject>(poolSize); //list에 추가되면 자동으로 늘어남 poolsize에 고정된건 아님 ㅇㅇ 애초에 왜냐하면 start에 한번 실행되니
        for (int i = 0; i<poolSize; i++)
        {
            CreateNewObject();
        }
    }
    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in pool) // pool를 검사하기
        {
            if (obj.activeSelf == false) // 제일 먼저 비활성화 된 오브젝트를 찾는다
            {
                return obj;// 그걸 가지고감
            }
        }
        return CreateNewObject(); //비활성화 된게 없으면 비활성화 된것을 createnewobject 메소드를 리턴함.
    }

}
