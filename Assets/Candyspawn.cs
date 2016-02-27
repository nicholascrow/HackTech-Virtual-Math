using UnityEngine;
using System.Collections;

public class Candyspawn : MonoBehaviour
{
    public GameObject candy;
    public GameObject camera;
    public int numToSpawn;
    // GameObject cube;
    // Use this for initialization
    void Start()
    {
        //for(int i = 0; i < 10; i++)
        // cube  = GameObject.Instantiate(spawnCandy);
        //  cube.transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (numToSpawn > 0)
        {
            GameObject spawnCandy = (GameObject)Instantiate(candy, camera.transform.position, Quaternion.Euler(camera.transform.forward));
            spawnCandy.transform.localScale = new Vector3(.2f, .2f, .2f);
            spawnCandy.AddComponent<Rigidbody>();
            //   spawnCandy.rigidbody.a
            BoxCollider b = (BoxCollider)spawnCandy.gameObject.AddComponent(typeof(BoxCollider));
            b.size = new Vector3(.5f, 2, .5f);
            b.center = new Vector3(0, 1, 0);
            spawnCandy.GetComponent<Rigidbody>().useGravity = false;
            spawnCandy.GetComponent<Rigidbody>().mass = 100;
            numToSpawn--;
        }
    }
}
