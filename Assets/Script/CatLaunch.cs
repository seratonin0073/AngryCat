using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLaunch : MonoBehaviour
{
    [SerializeField] private float forceValue = 1f;
    [SerializeField] private float minSpeedToDrag = 1f;
    [SerializeField] private GameObject startPointToDrag;
    [SerializeField] private ResultController resoult;

    private Rigidbody2D rb2d;
    private Camera mainCamera;
     

    public static bool isNonVisible;
    private Vector2 startPosition;
    public static bool canDrag;
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
            startPositionObject = Instantiate(startPointToDrag, startPosition, Quaternion.identity);

            /*resoult.StopTimer();
            isNonVisible = false;*/
        }
    }

    //Викликається коли користувач клікнув по колойдеру та тримає курсор всередині колайдера
    private void OnMouseDrag()
    {
        if(canDrag && !isNonVisible)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
        else if(canDrag && isNonVisible)
        {
            transform.position = startPointToDrag.transform.position;
        }
    }

    //Викликається коли користувач відпустив кнопку миші
    private void OnMouseUp()
    {
        if(canDrag && !isNonVisible)
        {
            Vector2 currentPosition = rb2d.position;
            Vector2 direction = startPosition - currentPosition;
            rb2d.isKinematic = false;
            rb2d.AddForce(direction * forceValue, ForceMode2D.Impulse);
            canDrag = false;
            Destroy(startPositionObject);
            resoult.StartTimer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            AudioManager.instance.PlayLevelComplete();
            resoult.StopTimer();
            resoult.SaveResoult();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        isNonVisible = true;
    }

    private void OnBecameVisible()
    {
        isNonVisible = false;
    }

}
