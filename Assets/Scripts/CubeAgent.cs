using System.Collections;
using System.Collections.Generic;
using MLAgents;
using MLAgents.Sensors;
using UnityEngine;

public class CubeAgent : Agent
{
    public float speed;
//    public float maxTime;
//    public float maxResetTime;
    public float jumpForce;

    public Plane plane;
    Rigidbody rb;
    float time, resetTime;

    bool isJump = false;
    Vector3 initPos;
    Circle nearest;

    float previosDistance;

    private void Awake() {
        initPos = transform.position;
    //    time = 0;
    //    resetTime = 0;
        rb = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        //MoveCube(x,z,speed);

        /*time += Time.deltaTime;

        if (time > maxTime && nearest != null) {

            float distance = Vector3.Distance(nearest.transform.position, transform.position);
            float delta = distance - previosDistance;
    
            if (delta < 0) AddReward(-0.1f * delta);

            previosDistance = distance;
            time = 0;
        }*/

        /*resetTime += Time.deltaTime;

        if (resetTime > maxResetTime) {
            Reset(-1.0f);
        }*/

        
    }

    private void FixedUpdate() {
        if (StepCount % 5 == 0)
        {
            RequestDecision();
        }
        else
        {
            RequestAction();
        }

        //if (isJump) rb.AddForce(0,jumpForce,0);

    }

    void MoveCube(float x, float z, float y, float speed) {

        //Debug.Log(y);
        if (y > 0.5 && !isJump) {
            rb.AddForce(0,jumpForce,0,ForceMode.Impulse);
            isJump = true;
            AddReward(-0.1f);
        }

        if (isJump) return;

        Vector3 translate = (Vector3.forward * x + Vector3.left * z) * speed * Time.fixedDeltaTime;
        transform.Translate(translate);
    }

    public void Reset(float amount) {
        Award(amount);
        EndEpisode();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        float x = vectorAction[0];
        float z = vectorAction[1]; 
        float y = vectorAction[2];

        MoveCube(x,z, y, speed);
        if (maxStep > 0) {
            AddReward(-1f / maxStep);
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[3];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        action[2] = Input.GetAxis("Jump");
        
        return action;
    }

    public override void CollectObservations(VectorSensor vectorSensor)
    {
        // nearest Circle
        if (nearest == null) {
            nearest = plane.GetNearestCircle(this);
            previosDistance = Vector3.Distance(nearest.transform.position, transform.position);
        }
        else nearest = plane.GetNearestCircle(this);
        
        Vector3 observeDirection = nearest.transform.position - transform.position;

        vectorSensor.AddObservation(observeDirection.magnitude);
        vectorSensor.AddObservation(observeDirection);
        
        float distace = Mathf.Abs(observeDirection.y);
        vectorSensor.AddObservation(distace > 0.75f);

        //vectorSensor.AddObservation(observeDirection.x);
        //vectorSensor.AddObservation(observeDirection.x);
        
    }

    public override void OnEpisodeBegin() {
        transform.position = initPos;
    //    time = 0;
    //    resetTime = 0;
    }

    public void Award(float amount) {
        AddReward(amount);
        /*if (amount > 0) {
            time = 0;
            resetTime = 0;
        }*/
    }

    private void OnCollisionEnter(Collision other) {
        isJump = false;
    }

}
