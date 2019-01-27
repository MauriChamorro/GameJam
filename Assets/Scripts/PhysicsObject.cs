using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float factor1;

    public float factor2;

    public float minGroundNormalY = .65f;

    //for gravity
    public float gravityModifier = 1f;

    //horizontal - input from outside the class
    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;

    //reference to attached rg2d
    protected Rigidbody2D rb2d;

    //vector velocity for gravity
    protected Vector2 velocity;

    #region Physcis Cast
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    //store active contacts
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    //padding of distance, to make sure that we never get stuck inside another collider
    public float shellRadius = 0.01f;

    public float minMoveDistance = 0.001f;
    #endregion


    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //not going to check againts triggers
        contactFilter.useTriggers = false;

        //get a layer mask from Player Settings: Physcis 2D
        //use the settings to determine what layers we're gonna check collision aganits
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));

        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        //move down it's beign affected bt gravity 
        // Physics2D.gravity: default value unity
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        //horizontal movmt
        //testing x-mov first because is better handle slopes
        velocity.x = targetVelocity.x;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        //calculate movement
        //direction to try to move along the ground
        //get the perperdicular normal of original
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 moveDirection = moveAlongGround * deltaPosition.x;

        // default value
        grounded = false;

        //First
        //false: idicate to calculate horizontal movmt
        Movement(moveDirection, false);

        moveDirection = Vector2.up * deltaPosition.y;

        //Second
        //aply movemnt - true: idicate to calculate vertical movmt
        Movement(moveDirection, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {

                // This line and the if statement will check if a platform has a PlatformEffector2D.
                // If it does, it will allow the player to jump up from underneath, but not fall through
                // the top surface
                PlatformEffector2D platform = hitBuffer[i].collider.GetComponent<PlatformEffector2D>();
                if (!platform || (hitBuffer[i].normal == Vector2.up && velocity.y < 0 && yMovement))
                {
                    hitBufferList.Add(hitBuffer[i]);
                }


            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {

                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {

                    velocity = velocity - projection * currentNormal;

                }
                float modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}