using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static BeamWeapon beamWeapon;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ObjectPooler bulletPool;
    public float speed;
    public int damage;
    public bool isShooting = false;

    void Awake()
    {
        if (beamWeapon != null)
        {
           Destroy(gameObject);
        }
        else{beamWeapon = this;}
    } 
    public IEnumerator WaitThenShoot()
    {
        isShooting = true;
        //Instantiate(prefab, transform.position, transform.rotation);
        GameObject bullet = bulletPool.GetPooledObject();
        bullet.transform.position = transform.position; //bullet 위치가 플레이어 child인 beamweapon 과 같은 위치이도록 하는 코드
        bullet.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        isShooting = false;
    }
    
}
