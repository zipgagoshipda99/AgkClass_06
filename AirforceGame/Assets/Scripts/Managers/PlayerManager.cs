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
    private float powerBoost = 3f;
    private bool boosting = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        energy = maxEnergy;
        //게임 시작때 에너지 표시용
        UI_Controller.uiController.UpdEnergySlider(energy, maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {// -1 , 0, 1 씩만 움직이게 함
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
    private void FixedUpdate()
    {
        //가로축은 +1f이므로 오르쪽이므로 이동, 0은 세로축이므로 이동 안함
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
        UI_Controller.uiController.UpdEnergySlider(energy, maxEnergy);
    }
    private void EnterBoost()
    {
        if (energy >= 10)
        {
            animator.SetBool("isBoosting", true);
            boost = powerBoost;
            boosting = true;
        }
    }
    private void ExitBoost()
    {
        animator.SetBool("isBoosting", false);
        boost = 1f;
        boosting = false;
    }
}
