using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;
    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

    [System.Serializable]
    public class Wave
    {
        public GameObject prefabObj;
        public float spawnTimer;
        public float spawninterval = 1f;
        public int objectsPerWave;
        public int spawnedObjectCount;

    }

    
    void Update()
    {
        waves[waveNumber].spawnTimer += Time.deltaTime * PlayerManager.playerManager.boost;
        if(waves[waveNumber].spawnTimer >= waves[waveNumber].spawninterval)
        {
            waves[waveNumber].spawnTimer = 0; //리셋 또 반복할수 있게
            SpawnObject(); //스폰 후 다시 타이머 카운트

        }
        if(waves[waveNumber].spawnedObjectCount >= waves[waveNumber].objectsPerWave)
        {
            waves[waveNumber].spawnedObjectCount = 0;
            waveNumber++;
            if (waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }
    private void SpawnObject()
    {
        Instantiate(waves[waveNumber].prefabObj, RandomSpawnPoint(), transform.rotation);
                    // 뭘 생성하는지 , (이 코드)컨포넌트가 연결된 오브젝트에 위치, (이 코드)컨포넌트가 연결된 오브젝트에 회전
        waves[waveNumber].spawnedObjectCount++;
    }
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;
        spawnPoint.y = minPos.position.y;
        spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
        return spawnPoint;
        
    }
}
