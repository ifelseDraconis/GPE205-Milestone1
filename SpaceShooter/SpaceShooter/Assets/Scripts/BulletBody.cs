using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletBody : MonoBehaviour
{
    public GameObject goesKaboom;
    public AudioClip doesHit;

    private Transform thisBulletTran;
    private GameManager thisManager;
    private float thisBulletSpeed;
    private float thisBulletLifespan;
    private Collider warHead;

    private GameObject myFather;
    private Rigidbody thisRigid;


   
    // Start is called before the first frame update
    void Start()
    {
        thisBulletTran = GetComponent<Transform>();

        thisManager = GameManager.thisManager;
        thisBulletSpeed = thisManager.BulletSpeed;
        thisBulletLifespan = thisManager.BulletDuration;

        warHead = GetComponent<Collider>();
        thisRigid = GetComponent<Rigidbody>();

        StartCoroutine(byebyeWorld());

        
    }

    // Update is called once per frame
    void Update()
    {
        // this makes the bullet move
        thisBulletTran.Translate(-thisBulletTran.forward * thisBulletSpeed * Time.deltaTime);
    }

    // this just sends a message that the bullet is dead
    void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(doesHit, transform.position);
        Instantiate(goesKaboom, thisBulletTran.position, Quaternion.identity);
    }

    IEnumerator byebyeWorld()
    {
        yield return new WaitForSeconds(thisBulletLifespan);
        Destroy(gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        bool doesHit = other.tag == "BlastSphere" | other.tag == "Pickups";
        if (!doesHit)
        {
            Destroy(gameObject);
        }
        
    }

    public void thisParent(GameObject myPapa)
    {
        myFather = myPapa;
    }
}
