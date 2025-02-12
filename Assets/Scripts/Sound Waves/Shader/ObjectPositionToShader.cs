using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionToShader : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] string propertyPath;
    //[SerializeField] Shader shader;
    [SerializeField] Transform obj;

    // Start is called before the first frame update
    void Start()
    {
        if(obj == null)
        {
            obj = FindObjectOfType<PlayerManager>().transform;
        }


    }

    // Update is called once per frame
    void Update()
    {
        material.SetVector(propertyPath, obj.position);
    }
}
