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
        Movement(moveDirection, true);

        moveDirection = Vector2.up * deltaPosition.y;

        //Second
        //aply movemnt - true: idicate to calculate vertical movmt
        Movement(moveDirection, true);
    }

    //set position
    void Movement(Vector2 moveDirection, bool yMovement)
    {
        float distance = moveDirection.magnitude;

        //if we want check colisions menausred by minMoveDistance
        if (distance > minMoveDistance)
        {
            //check if any of the attached colliders of rb2d are going to overlap (solapar) with anything in the next frame
            //ignoring the colliders attached to the same Rigidbody2D
            //move a cast from payer in front to us
            //in the next fram is this player going to overlap with another collider
            //distance: betwen playert cast
            int count = rb2d.Cast(moveDirection, contactFilter, hitBuffer, distance + shellRadius);

            //copy the array hit result 
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            //determine the angle of the thing that we're colliding with 
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                //compare the normal to a minimum ground nomal values
                Vector2 currentNormal = hitBufferList[i].normal;

                //trying to determinate if player is grounded
                //the main manifestation of that in our game is that the player is 
                //animations
                if (currentNormal.y > minGroundNormalY)
                {
                    //if the angle of the object that we´re gorunding o not: example - walls
                    // Y-movement insolate to hadl easly
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                //difference between the velocity and the corrent normal and determining 
                //whether we need to substract from our velocity to prevent the player from
                //entering  into another collider 
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    //cancell out the aprt of our velocity that would be stopped by collision
                    velocity = velocity - projection * currentNormal;
                }

                //check if the colision in our lists distance is less than our shellsize constant
                // then use the shell size for our distance insted
                float modifiedDistance = hitBufferList[i].distance - shellRadius;

                //distance is modified 
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }

        //apply de move 
        rb2d.position = rb2d.position + moveDirection.normalized * distance;
    }

}