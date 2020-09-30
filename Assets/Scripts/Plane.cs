using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plane : MonoBehaviour
{
    public int maxDistance = 6;
    public float maxY = 2;
    static List<Circle> circles = new List<Circle>();

     public void AddCircle(Circle circle) {
        //Debug.Log(circles);
        circles.Add(circle);
        //Debug.Log(circles.Count);
    }

    public void RemoveCircle(Circle circle) {
        CreateRandomCircle();
        circles.Remove(circle);
        if (circles.Count == 0) {
            FindObjectOfType<CubeAgent>().Reset(2.0f);
//            SceneManager.LoadScene(0);
        }
    }

    void CreateRandomCircle() {
        Circle newCircle = new Circle();

        
        float x = Random.Range(0,maxDistance);
        float z = Random.Range(0,maxDistance);
        newCircle.SetPos(new Vector3(x, -0.2f, z));
        circles.Add(newCircle);
    }

    public void ChangeCirclePos(Circle circle) {

        //System.Random rnd = new System.Random();
        //Debug.Log(Random.Range(0,maxDistance));

        float x = transform.position.x + Random.Range(-maxDistance,maxDistance);
        float z = transform.position.z + Random.Range(-maxDistance,maxDistance);
        float y = transform.position.y + Random.Range(0,maxY);

        circle.SetPos(new Vector3(x, y, z));
    }

    public Circle GetNearestCircle(CubeAgent agent) {

        int index = 0;
        float minDistance = 9999999f;

        //Debug.Log(circles.Count);

        for (int i = 0; i < circles.Count; i++) {
            float distance = circles[i].GetDistance(agent.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                index = i;
            }
        }
        
        return circles[index];
    }

    private void OnTriggerEnter(Collider other) {
        other.GetComponent<CubeAgent>().Reset(-1.0f);
        //SceneManager.LoadScene(0);

    }
}
