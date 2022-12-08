using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class IKImplementation : MonoBehaviour
{
    public int chainLength;
    public Transform target;

    public int iterations = 10;
    public float delta = 0.001f;
    [Range(0, 1)]
    public float snapBackStrength = 1f;

    protected float[] bonesLength;
    protected float completeLength;
    protected Transform[] bones;
    protected Vector3[] positions;
    // Start is called before the first frame update
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        bones = new Transform[chainLength + 1];
        positions = new Vector3[chainLength + 1];
        bonesLength = new float[chainLength];

        completeLength = 0;

        var current = transform;
        for(var i=bones.Length-1; i>=0; i--)
        {
            bones[i] = current;
            if (i == bones.Length - 1)
            {

            }
            else
            {
                bonesLength[i] = (bones[i + 1].position - current.position).magnitude;
                completeLength += bonesLength[i];
            }
            current = current.parent;
        }
    }
    private void LateUpdate()
    {
        ResolveIK();
    }
    private void ResolveIK()
    {
        if (target == null)
        {
            return;
        }
        if (bonesLength.Length != chainLength)
        {
            Init();
        }
        for(int i=0; i<bones.Length; i++)
        {
            positions[i] = bones[i].position;
        }


        if ((target.position - bones[0].position).sqrMagnitude >= completeLength * completeLength)
        {
            var direction = (target.position - positions[0].normalized);
            for(int i=1; i<positions.Length; i++)
            {
                positions[i] = positions[i - 1] + direction * bonesLength[i - 1];
            }
        }

        for (int i = 0; i < positions.Length; i++)
        {
            bones[i].position = positions[i];
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        var current = this.transform;
        for(int i=0; i<chainLength && current!=null && current.parent!=null; i++)
        {
            var scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), new Vector3(scale, Vector3.Distance(current.parent.position, current.position), scale));
            Handles.color = Color.green;
            Handles.DrawWireCube(Vector3.up * 0.5f, Vector3.one);
            current = current.parent;
        }
    }
}
