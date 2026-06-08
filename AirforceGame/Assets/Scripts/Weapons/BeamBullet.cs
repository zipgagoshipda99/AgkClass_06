using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;



public class BeamBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, BeamWeapon.beamWeapon.speed * Time.deltaTime);
        if(transform.position.y > 7f)
        {
            gameObject.SetActive(false); //gameobject를 삭제하는 대신 그냥 비활성화
        }
    }
    private void OnCollisionEnter2D(Collision2D enteredCollision)
    {
        if (enteredCollision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false); //gameobejct를 삭제하는 대신 그냥 비활성화
            
        }
    }

}
