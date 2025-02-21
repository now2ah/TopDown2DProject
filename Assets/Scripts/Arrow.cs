using System;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float speed = 12.0f;
    public float delay = 0.25f;
    public GameObject bowPrefab; //Ȱ
    public GameObject arrowPrefab;
    public int arrowCount = 5;

    bool inAttack = false; //���� ������� Ȯ��
    GameObject bowObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 pos = transform.position;
        bowObject = Instantiate(bowPrefab, pos, Quaternion.identity);
        bowObject.transform.SetParent(transform);
        //Ȱ ������Ʈ�� �θ�� �÷��̾��Դϴ�.
    }

    // Update is called once per frame
    void Update()
    {
        //left shift
        if (Input.GetButtonDown("Fire3"))
        {
            Attack();
        }


        //rotate bow , order
        float bowZ = -1; //ĳ���ͺ��� �տ� ������ �˴ϴ�.
        var player_controller = GetComponent<PlayerController>();

        if (player_controller.z > 30 && player_controller.z < 150)
        {
            bowZ = 1;
        }
        bowObject.transform.rotation = Quaternion.Euler(0, 0, player_controller.z);

        bowObject.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ);
    }

    private void Attack()
    {
        //ȭ���� ������ �ְ�, ���� ���°� �ƴ� ���
        if (arrowCount > 0 && inAttack == false)
        {
            arrowCount--; //ȭ�� 1�� �Ҹ�
            inAttack = true; //���� ���� ��ȯ

            var player_controller = GetComponent<PlayerController>();

            float z = player_controller.z; //ȸ�� ��

            var rotation = Quaternion.Euler(0, 0, z);

            //����� ȸ�� ������ ������Ʈ�� �����մϴ�.
            var arrow_object = Instantiate(arrowPrefab, transform.position, rotation);

            float x = Mathf.Cos(z * Mathf.Deg2Rad);
            float y = Mathf.Sin(z * Mathf.Deg2Rad);

            Vector3 vector = new Vector3(x, y) * speed;

            var rbody = arrow_object.GetComponent<Rigidbody2D>();

            rbody.AddForce(vector, ForceMode2D.Impulse);

            //�߻� �����̸�ŭ ���� ���Ѽ� ���� ��带 �����մϴ�.
            Invoke("AttackChange", delay);
        }
    }

    public void AttackChange() => inAttack = false;
}