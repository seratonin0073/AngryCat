using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchingObject : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private Slider HPSlider;
    [SerializeField] private Transform spawnPoint;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if(HPSlider == null)
        {
            Debug.LogError("HPSlider is empty!");
        }
        HPSlider.maxValue = health;
        HPSlider.value = health;
    }

    void Damage(int damage)
    {
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
        rb2d.isKinematic = true;
        transform.position = spawnPoint.position;
        Camera.main.GetComponent<CameraMove>().MoveToStartPosition();
        health -= damage;
        HPSlider.value = health;
        if(health <= 0)
        {
            Debug.Log("I`m dead!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(spawnPoint != null)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Damage(collision.GetComponent<ObstacleSettings>().damage);
            }
        }
        else
        {
            Debug.LogError("Spawn Point is empty!");
        }
    }

    /*private void OnBecameInvisible()
    {
        Damage(0);
    }*/
}
