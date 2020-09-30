using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public Plane plane;

    public void Start() {
       //CircleFactory.getInstance().AddCircle(this);
       plane.AddCircle(this);
    }

    private void OnTriggerEnter(Collider other) {
        if (other != null) {
            other.GetComponent<CubeAgent>().Award(10.0f);
            //Destroy(gameObject);
            //CircleFactory.getInstance().ChangeCirclePos(this);
            plane.ChangeCirclePos(this);
        }
    }

    public float GetDistance(Vector3 position) {
        return Vector3.Distance(transform.position, position);
    }

    public void SetPos(Vector3 position) {
        transform.position = position;
    }

    /*private void OnDestroy() {
        CircleFactory.getInstance().RemoveCircle(this);
    }*/
}
