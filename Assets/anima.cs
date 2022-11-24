using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anima : MonoBehaviour
{
    Transform originRotation;
    // Start is called before the first frame update
    void Start()
    {
        originRotation = transform;
        /*        var diff = transform.rotation* Quaternion.Inverse(transform.rotation);
                print(transform.rotation);
                print(Quaternion.Inverse(transform.rotation));*/
        transform.rotation = Quaternion.Inverse(transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
