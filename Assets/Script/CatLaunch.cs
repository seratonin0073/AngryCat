using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLaunch : MonoBehaviour
{
    [SerializeField] private float forceValue = 1f;
    [SerializeField] private float minSpeedToDrag = 1f;
    [SerializeField] private GameObject startPositionPrefab;

    private Rigidbody2D rb2d;
    private Camera mainCamera;

    private Vector2 startPosition;
    private bool canDrag;
    private GameObject startPositionObject;

    void Awake()
    {
        mainCamera = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d.isKinematic = true;
    }

    //Викликається коли користувач натиснув і тримає кнопку миші поки вона перекриває об'єкт
    private void OnMouseDown()
    {
        float playerSpeed = rb2d.velocity.magnitude;

        if(playerSpeed < minSpeedToDrag)
        {
            canDrag = true;
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            startPosition = rb2d.position;
            startPositionObject = Instantiate(startPositionPrefab, startPosition, Quaternion.identity);
        }
    }

    //Викликається коли користувач клікнув по колойдеру та тримає курсор всередині колайдера
    private void OnMouseDrag()
    {
        if(canDrag)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    //Викликається коли користувач відпустив кнопку миші
    private void OnMouseUp()
    {
        if(canDrag)
        {
            Vector2 currentPosition = rb2d.position;
            Vector2 direction = startPosition - currentPosition;
            rb2d.isKinematic = false;
            rb2d.AddForce(direction * forceValue, ForceMode2D.Impulse);
            canDrag = false;
            Destroy(startPositionObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
