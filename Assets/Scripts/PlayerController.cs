using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public List<string> anime_list = new List<string>
    { "Down", "Up", "Left", "Right", "Dead" };
    string current = "";
    string previous = "";
    float h, v; //가로축과 세로축에 대한 값
    public float z = -90.0f;//회전 각
    Rigidbody2D rbody; //컴포넌트
    bool isMove = false; //움직이는 상태인지 확인
    Animator animator;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        previous = anime_list[0]; //처음 시작은 아래를 보고 있도록
    }

    // Update is called once per frame
    void Update()
    {

        if (isMove == false)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        Vector2 from = transform.position;

        Vector2 to = new Vector2(from.x + h, from.y + v);

        z = GetAngle(from, to); //키 입력을 통한 값을 통해 이동 각도를 계산할 함수 GetAngle


        //각도에 따라 방향과 애니메이션 설정
        if (z >= -45 && z < 45)
        {
            //오른쪽
            current = anime_list[3];
        }
        else if (z >= 45 && z <= 135)
        {
            //위쪽
            current = anime_list[1];
        }
        else if (z >= -135 && z <= -45)
        {
            //아래쪽
            current = anime_list[0];
        }
        else
        {
            //왼쪽
            current = anime_list[2];
        }

        if (current != previous)
        {
            previous = current;
            animator.Play(current, 0);
        }
    }

    private void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(h, v) * speed;
    }

    /// <summary>
    /// from에서 to 까지의 각도를 계산하는 함수
    /// </summary>
    /// <param name="from">시작 위치(A 지점)</param>
    /// <param name="to">마무리 위치(B 지점)</param>
    private float GetAngle(Vector2 from, Vector2 to)
    {
        float angle;

        if (h != 0 || v != 0)
        {
            //from과 to의 차이를 계산합니다.
            float dx = to.x - from.x;
            float dy = to.y - from.y;

            float radian = Mathf.Atan2(dy, dx);
            //Atan 같은 경우는 x좌표가 0일 경우 계산이 안됩니다.
            angle = radian * Mathf.Rad2Deg;
        }
        else
        {
            angle = z;
        }
        return angle;
    }
}