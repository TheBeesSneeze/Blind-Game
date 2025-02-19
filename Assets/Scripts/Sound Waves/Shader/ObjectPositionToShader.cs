using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPositionToShader : MonoBehaviour
{
    [SerializeField] Material[] materials;
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
        foreach(Material material in materials) 
            if(material != null)
                material.SetVector(propertyPath, obj.position);
    }

    private void OnDisable()
    {
        foreach (Material material in materials)
            if (material != null)
                material.SetVector(propertyPath, Vector3.zero);
    }

}
