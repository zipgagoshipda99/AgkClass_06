using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    [SerializeField] private Sprite[] sprites;
    [SerializeField]private Material whiteMaterial;
    [SerializeField]private GameObject destroyEffect;
    [SerializeField]private int obstacleLives;

    private Material defaultMaterial;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        float driftX = Random.Range(-0.5f, 0.5f);            // left/right drift
        float fallSpeedY = Random.Range(1f, 1f); //
        rb2D.velocity = new Vector2(driftX, -fallSpeedY); // -Y를 강제로 -로 변경해서 밑으로 내려가도록 함       
        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);
    }

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
    // void Start()
    // {
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     rb2D = GetComponent<Rigidbody2D>();
    //     spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    //     float pushX = Random.Range(-1f, 2f); //float 쓰면 -1.0~ 1.0 까지 모든 수 포함 ㅇㅇ
    //     float pushY = Random.Range(-1f,1f); //이건 -1.0~1.0 까지 모든 수 포함
    //     rb2D.velocity = new Vector2(pushX, pushY);

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     float moveX = 2f * Time.deltaTime;
    //     float[] randomMoveX = {-moveX, moveX};
    //     transform.position += new Vector3(randomMoveX[Random.Range(0, randomMoveX.Length)], 0);
    //     if (transform.position.x < -5 || transform.position.x > 5)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    private IEnumerator MaterialChangeWithDelay()
    {
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }
    private void OnCollisionEnter2D(Collision2D enteredCollision)
    {
        if (enteredCollision.gameObject.CompareTag("Player") || enteredCollision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log(gameObject.name + "collided with" + enteredCollision.gameObject.name);
            StartCoroutine("MaterialChangeWithDelay");
            AudioManager.audioManager.PlayModifiedSound(AudioManager.audioManager.hitObstacle);
            obstacleLives --;
            if (obstacleLives <= 0)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
                AudioManager.audioManager.PlayModifiedSound(AudioManager.audioManager.boom2);
                Destroy(gameObject);
            }
        }
    }
}
