using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float speed = 3f;
    private Vector3 startPoint;
    private Camera myCamera;


    void Awake()
    {
        myCamera = Camera.main;
        startPoint = transform.position;
    }

    private void LateUpdate()
    {
        if(currentTarget != null)
        {
            SetMovement();
        }
        else
        {
            currentTarget = GameObject.FindGameObjectWithTag("Finish").transform;
            
        }

        
    }

    private void SetMovement()
    {
        if(currentTarget.position.x > transform.position.x && !CatLaunch.canDrag)
        {
            Vector3 newPosition = 
                new Vector3(currentTarget.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, speed*Time.deltaTime);
        }
    }

    public void MoveToStartPosition()
    {
        transform.position = startPoint;
    }

}
