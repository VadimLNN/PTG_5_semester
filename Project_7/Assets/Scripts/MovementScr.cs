using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScr : MonoBehaviour
{
    [Range(200f, 500f)]
    public float mSpeed = 300f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        bool sprint = (Input.GetKey(KeyCode.LeftShift));

        // �������� ������� w a s d
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        // ��������� ����������� �������� � ��������� X/Z
        Vector3 dir = new Vector3(xMove, 0, zMove);
        // ������������ ������� 
        dir.Normalize();

        Vector3 v;

        // ������ �������� = ����������� * �������� * ����� � ���������� ������
        if (!sprint)
            v = transform.TransformDirection(dir) * mSpeed * Time.fixedDeltaTime;
        else
            v = transform.TransformDirection(dir) * mSpeed*2 * Time.fixedDeltaTime;

        // �������������� �������� �� Y
        v.y = rb.velocity.y;
        // ���������� ��������
        rb.velocity = v;
    }
}