using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CannonShot : MonoBehaviour
{
    private Transform thisCannon;
    private bool makeShot;
    public AudioClip thisShot;

    public GameObject myBulletShot;
    // Start is called before the first frame update
    void Start()
    {
        thisCannon = gameObject.GetComponent<Transform>();
    }

    public void fireTheCannon(Transform thisShipTrans)
    {
        // this produces and then fires a bullet at a specific point in space with the same rotation as the ship
        makeShot = true;
        
    }

    void Update()
    {

        if (makeShot)
        {
            GameObject thisBullet;
            thisBullet = Instantiate(myBulletShot, thisCannon.position, thisCannon.rotation) as GameObject;
            thisBullet.transform.Rotate(Vector3.left * 90);
            AudioSource.PlayClipAtPoint(thisShot, thisCannon.position);

            Rigidbody thisTempRigid;
            thisTempRigid = thisBullet.GetComponent<Rigidbody>();

            BulletBody byTheWay = thisBullet.GetComponent<BulletBody>();
            byTheWay.thisParent(gameObject);

            makeShot = false;
        }
    }
}
