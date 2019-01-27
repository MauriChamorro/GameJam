using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public float MovementX;
    public float MovementY;
    public float HorizontalSpeed;
    public float VerticalSpeed;

    bool horizontal = false;
    bool vertical = false;

    private Vector3 originalPos;

    private void Start()
    {
        originalPos = this.transform.position;
    }

    private void Update()
    {
        if (HorizontalSpeed != 0)
        {
            if (horizontal)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, 
                    new Vector3(originalPos.x + MovementX,
                        this.transform.position.y, 
                        this.transform.position.z), 
                    HorizontalSpeed * Time.deltaTime);
                if (this.transform.position.x >= originalPos.x + MovementX)
                    horizontal = false;
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position,
                    new Vector3(originalPos.x - MovementX,
                        this.transform.position.y,
                        this.transform.position.z), 
                    HorizontalSpeed * Time.deltaTime);
                if (this.transform.position.x <= originalPos.x - MovementX)
                    horizontal = true;
            }

        }

        if (VerticalSpeed != 0)
        {
            if (vertical)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position,
                    new Vector3(this.transform.position.x,
                        originalPos.y + MovementY,
                        this.transform.position.z),
                    VerticalSpeed * Time.deltaTime);
                if (this.transform.position.y >= originalPos.y + MovementY)
                    vertical = false;
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position,
                    new Vector3(this.transform.position.x,
                        originalPos.y - MovementY,
                        this.transform.position.z),
                    VerticalSpeed * Time.deltaTime);
                if (this.transform.position.y <= originalPos.y - MovementY)
                    vertical = true;
            }

        }
        //if (VerticalSpeed != 0)
        //{
        //    if (this.transform.position.x <= originalPos.x)
        //        this.transform.position = new Vector3(this.transform.position.x,
        //            Mathf.Lerp(transform.position.y,
        //                originalPos.y + MovementY,
        //                -HorizontalSpeed * Time.deltaTime));
        //}
    }
}
