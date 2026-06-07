using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveY = (GameManager.gameManager.worldSpeed * PlayerManager.playerManager.boost) * Time.deltaTime;
        
        transform.position += new Vector3(0, -moveY);//원하는 설정대로 밑으로 내려가게함. x 좌표들은 그대로.
        if (transform.position.x < -7f || transform.position.x > 7f ||
            transform.position.y < -7f || transform.position.y > 7f)
        {
            Destroy(gameObject);
        }
        
    }
}
