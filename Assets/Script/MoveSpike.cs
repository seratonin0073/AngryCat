using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpike : MonoBehaviour
{
    [SerializeField] private Transform upPoint, bottomPoint;
    [SerializeField] private float moveUpSpeed, moveBottomSpeed;
    [SerializeField] private bool moveModeHorizontally = false;
    
    private bool isMoveUp = false;

    void Update()
    {

        if(moveModeHorizontally)
        {
            if (transform.position.x < upPoint.position.x) isMoveUp = false;
            else if (transform.position.x > bottomPoint.position.x) isMoveUp = true;
        }
        else
        {
            if (transform.position.y > upPoint.position.y) isMoveUp = false;
            else if (transform.position.y < bottomPoint.position.y) isMoveUp = true;
        }

    }

    void FixedUpdate()
    {
        if (isMoveUp) transform.Translate(Vector3.up * moveUpSpeed * Time.fixedDeltaTime);
        else transform.Translate(-Vector3.up * moveBottomSpeed * Time.fixedDeltaTime);
    }
}
