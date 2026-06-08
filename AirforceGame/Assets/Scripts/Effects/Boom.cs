using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update() //boom effect 가 가만히 그 transform postition에 나타나도록 하지 않고 배경과 비슷한 속도로 내려가게 하는거.
    {
        float moveY = (GameManager.gameManager.worldSpeed * PlayerManager.playerManager.boost) * Time.deltaTime;
        
        transform.position += new Vector3(0, -moveY);
    }
}
