using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingKnobs : MonoBehaviour
{
    public Vector3 spawnPosition; // Уникальная позиция спавна для объекта
    public float speed = 2.0f; // Скорость движения объекта

    private void Update()
    {
        // Вычисляем направление движения к спавн-позиции
        Vector3 direction = (spawnPosition - transform.position).normalized;

        // Вычисляем новую позицию объекта
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // Перемещаем объект к новой позиции
        transform.position = newPosition;
    }
}
