using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuParalox : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;
    private float backgroundImageHeight;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundImageHeight = sprite.texture.height / sprite.pixelsPerUnit;
    }
    void Update()
    {
        float moveY = Time.deltaTime * moveSpeed;
        //Time.deltatime == 이전 프레임부터 현재 프레임까지 걸린 시간(초)
        //basically the time it takes for your computer to render a single frame.
        
        transform.position += new Vector3(0f, moveY, 0f);
        if (Math.Abs(transform.position.y) - backgroundImageHeight >0f)
        //절댓값 =  absolute value. 절댓값이 backgroundImage의 세의 길이보다 클때 리셋
        {
            transform.position = new Vector3(transform.position.x,0f,0f);
        }
    }
}
