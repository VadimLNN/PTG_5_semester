using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunScr : MonoBehaviour
{
    // ���� � ��������� 
    public float dmg = 10f;
    public float range = 1000f;

    //  �������� �������� (10 � �������) � ����� �� ����. �������� 
    public float fireRate = 10f;
    public float nextShot = 0f;

    // ������ �� ������, ������� ������ ��� �������� � ���������
    public Camera cam;
    public ParticleSystem flash;
    public ParticleSystem onHit;

    // ������ �� ������ 
    public GameObject gilza_orig;

    // ����������� ����� � ������ �� ��������� ����� 
    float score = 0;
    public Text scoreText;

    void Update()
    {
        // ����� �������� ��� ������� ������ �������� 
        if (Input.GetButton("Fire1") && Time.time >= nextShot)
        {
            // ������ ������� �� ���� �������� � �������
            nextShot = Time.time + 1 /fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // ��������������� ������� 
        flash.Play();
        
        // �������� ������� ������ 
        GameObject gilza = Instantiate(gilza_orig, transform.position, transform.rotation);
        // �������� ����������� ���� ������ ��� �������� ���������
        Rigidbody rb_g = gilza.GetComponent<Rigidbody>();
        // ����������� ������ ������
        Vector3 dir = new Vector3(0f, 300f, -100f); 
        // �������� ��������� ������ 
        rb_g.AddForce(dir);
        // ����������� ������ ����� 5 ������ 
        Destroy(gilza.gameObject, 5f);


        // ���� ��� �� ������ ����� �� ���-��
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            // ���� ��� ���-�� ����� ��� "����"
            if (hit.transform.CompareTag("target"))
            {
                // ��������� ������� � ������� ����
                TargetScr t = hit.transform.GetComponent<TargetScr>();
                // ����� ������ ��������� ����� 
                t.Hit(dmg);            
            }

            // �������� � ��������������� ������� �������� � ����� ��������� 
            ParticleSystem hitEffect = Instantiate(onHit, hit.point, Quaternion.LookRotation(hit.normal));
            // ��������������� �������� 
            hitEffect.Play();
            // ����������� ������� ����� ������� 
            Destroy(hitEffect.gameObject, 1f);
        }
    }

    public void GetScore(float gettedScore)
    {
        score += gettedScore;
        scoreText.text = score.ToString("F0");
    }
}
