using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingKnobs : MonoBehaviour
{
    public Vector3 spawnPosition; // ���������� ������� ������ ��� �������
    public float speed = 2.0f; // �������� �������� �������

    private void Update()
    {
        // ��������� ����������� �������� � �����-�������
        Vector3 direction = (spawnPosition - transform.position).normalized;

        // ��������� ����� ������� �������
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // ���������� ������ � ����� �������
        transform.position = newPosition;
    }
}
