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
        if (spawnPoint != null)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Damage(collision.GetComponent<ObstacleSettings>().damage);
            }
            if (collision.CompareTag("GravityZone")) rb2d.gravityScale = -rb2d.gravityScale; 
            if(collision.CompareTag("TimeRemover"))
            {
                ResultController.Instance.RemoveTime(10);
                Destroy(collision.gameObject);
            }
            if (collision.CompareTag("AddTime"))
            {
                ResultController.Instance.AddTime(10);
                Destroy(collision.gameObject);
            }
            if(collision.CompareTag("AID"))
            {
                if(health < HPSlider.maxValue)
                {
                    health += 20;
                    if (health > HPSlider.maxValue)
                    {
                        health = (int)HPSlider.maxValue;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Spawn Point is empty!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GravityZone"))
        {
            Vector2 dir = rb2d.velocity;
            dir.y = 0;
            rb2d.velocity = dir;
            rb2d.gravityScale = -rb2d.gravityScale;
            Debug.Log("Change gravity! Exit!");
        }
    }
}
