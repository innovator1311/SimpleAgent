using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class CircleFactory : MonoBehaviour
{
    public static CircleFactory instance;
    int maxDistance = 6;
    static List<Circle> circles = new List<Circle>();

    private void Start() {
     //   circles = new List<Circle>();
    }

    static public CircleFactory getInstance() {
        if (instance == null) instance = new CircleFactory();
        return instance;
    }

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

        float x = Random.Range(0,maxDistance);
        float z = Random.Range(0,maxDistance);
        
        circle.SetPos(new Vector3(x, -0.2f, z));
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
}
