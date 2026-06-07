using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerManager;
    void Awake()
    {
        if (playerManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            playerManager = this;
        }
    }

    private Rigidbody2D rb2D;
    private Animator animator;
    public float boost = 1f;
    [SerializeField]private Vector2 playerDirection;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float energy;
    [SerializeField]private float maxEnergy;
    [SerializeField]private float energyRegen;
    [SerializeField]private float health;
    [SerializeField]private float maxHealth;
    [SerializeField]private GameObject destroyEffect;
    [SerializeField]private Material whiteMaterial;
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    private float powerBoost = 3f;
    private bool boosting = false;
    private bool energyLowShown = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        energy = maxEnergy;
        //게임 시작때 에너지 표시용
        UI_Controller.uiController.UpdEnergySlider(energy, maxEnergy);
        health = maxHealth;
        UI_Controller.uiController.UpdHealthSlider(health, maxHealth);
        defaultMaterial = spriteRenderer.material;
        
    }

    // Update is called once per frame
    void Update()
    {// -1 , 0, 1 씩만 움직이게 함
        if(Time.timeScale > 0)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");
            animator.SetFloat("MoveX", directionX);
            animator.SetFloat("MoveY", directionY);
            playerDirection = new Vector2(directionX, directionY);//.normalized 도 가능 .Normalize() 대신 근데 .normalized는 메모리에 복사본 만들어서 아까우니 이렇게 하는것.
            playerDirection.Normalize(); //원본 벡터값을 직접 변경하는것
            //벡터의 방향은 그대로 유지한 채, 벡터의 길이(크기)를 '1'로 만들어 반환(정규화)하는 속성 = normalized
            //대각선으로 오른쪽 키와 위쪽 키를 눌르면 1.42~~ 어쩌고 나오는건 피타고라스 성질 때문. (대각선이니.)
            //.normalized is a property that returns a new vector with the exact same direction as the original vector, but with a magnitude (length) scaled to exactly 1
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                EnterBoost();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                ExitBoost();
            }
        }
    }
    private void FixedUpdate()
    {
        //새로운 vector 2를 저장한 변수에서 x값과 y값을 각각 움직이는 속도와 곱하고 속력을 구하는거 (거리 분의 시간 = 속력)
        rb2D.velocity = new Vector2(playerDirection.x *moveSpeed, playerDirection.y*moveSpeed);
        if (boosting == true)
        {
            if (energy >= 0.2f)
            {
                energy -= 0.2f;
            }
            else
            {
                ExitBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }
        bool isLow = energy < 10f;
        if (isLow != energyLowShown) //the opposite of.. 
                                    // isLow 가 true 고 energyLowShown이 false 일때와.
                                    // isLow 가 false이고 energyLowShown 이 true 일때 2가지 경우 ㅇㅇ
        {
            energyLowShown = isLow;
            UI_Controller.uiController.energyLowText.text = isLow ? "energy is low please wait until regen." : "";
                                                                    //isLow가 True 일때는 energy is low please wait until regen로 텍스트 변환
                                                                    //isLow가 false 일때는 ""로 텍스트 변환
        }

   
        UI_Controller.uiController.UpdEnergySlider(energy, maxEnergy);
    }
    private void EnterBoost()
    {
        if (energy >= 10)
        {
            animator.SetBool("isBoosting", true);
            boost = powerBoost;
            boosting = true;
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.fire);
        }
    }
    public void ExitBoost()
    {
        animator.SetBool("isBoosting", false);
        boost = 1f;
        boosting = false;
    }
    private void OnCollisionEnter2D(Collision2D enteredCollision)
    {
        if (enteredCollision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }
    private void TakeDamage(int damage)
    {
        health -= damage;
        UI_Controller.uiController.UpdHealthSlider(health, maxHealth);
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.hit);
        StartCoroutine("MaterialChangeWithDelay");
        if (health <= 0)
        {
            boost = 0f;
            gameObject.SetActive(false);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.DelayShowGameOverScreen());
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.ice);
        }
    }
    private IEnumerator MaterialChangeWithDelay()
    {
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }
}
