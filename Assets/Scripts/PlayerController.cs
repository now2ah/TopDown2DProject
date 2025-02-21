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
    float h, v; //������� �����࿡ ���� ��
    public float z = -90.0f;//ȸ�� ��
    Rigidbody2D rbody; //������Ʈ
    bool isMove = false; //�����̴� �������� Ȯ��
    Animator animator;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        previous = anime_list[0]; //ó�� ������ �Ʒ��� ���� �ֵ���
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

        z = GetAngle(from, to); //Ű �Է��� ���� ���� ���� �̵� ������ ����� �Լ� GetAngle


        //������ ���� ����� �ִϸ��̼� ����
        if (z >= -45 && z < 45)
        {
            //������
            current = anime_list[3];
        }
        else if (z >= 45 && z <= 135)
        {
            //����
            current = anime_list[1];
        }
        else if (z >= -135 && z <= -45)
        {
            //�Ʒ���
            current = anime_list[0];
        }
        else
        {
            //����
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
    /// from���� to ������ ������ ����ϴ� �Լ�
    /// </summary>
    /// <param name="from">���� ��ġ(A ����)</param>
    /// <param name="to">������ ��ġ(B ����)</param>
    private float GetAngle(Vector2 from, Vector2 to)
    {
        float angle;

        if (h != 0 || v != 0)
        {
            //from�� to�� ���̸� ����մϴ�.
            float dx = to.x - from.x;
            float dy = to.y - from.y;

            float radian = Mathf.Atan2(dy, dx);
            //Atan ���� ���� x��ǥ�� 0�� ��� ����� �ȵ˴ϴ�.
            angle = radian * Mathf.Rad2Deg;
        }
        else
        {
            angle = z;
        }
        return angle;
    }
}