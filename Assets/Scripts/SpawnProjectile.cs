using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{

    //// Update is called once per frame
    //[SerializeField]
    //float speed = 1f;

    //[SerializeField]
    //bool scaleToTime = false;

    //public GameObject firePoint;

    //public void Awake()
    //{
    //    GameManager.Instance.globalUpdate += internalUpdate;
    //}
    //public void internalUpdate()
    //{


    //}

    //public void Move(float moveX, float moveZ)
    //{
    //    Vector3 movingVector = new Vector3(moveX, 0.0f, moveZ);
    //    movingVector *= speed;
    //    //transform.Translate(movingVector);
    //    transform.parent.Translate(movingVector);
    //}

    GameObject prefab;

    [SerializeField]
    float offsetUnits = 10f;

    [SerializeField]
    float xDegreeOffset = 0f;
    [SerializeField]
    float yDegreeOffset = 0f;
    [SerializeField]
    float zDegreeOffset = 0f;

    public float[] burstPattern;

    void Start()
    {
        prefab = Resources.Load("EnemyProjectile") as GameObject;
    }
        

    Vector3 calcOffset()
    {
        float xVal = Mathf.Cos(transform.eulerAngles.x+xDegreeOffset) * offsetUnits;
        float zVal = Mathf.Cos(transform.eulerAngles.z+yDegreeOffset) * offsetUnits;

        return new Vector3(xVal, Mathf.Cos(transform.eulerAngles.y), zVal);
    }

    void createProjectiles()
    {
        for(int i = 0; i < burstPattern.Length; ++i )
        {

        }
    }
    
}
