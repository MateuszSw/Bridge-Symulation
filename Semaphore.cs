using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaphore : MonoBehaviour
{
    public bool isOpen = true;
    public Collider stopCollider;

    public MeshRenderer lightMeshRenderer;
    private Material lightMaterial;

    void Awake()
    {
        lightMaterial = lightMeshRenderer.material;
    }

    public void Open()
    {
        isOpen = true;
        stopCollider.enabled = false;
        lightMaterial.color = Color.green;
    }

    public void Close()
    {
        isOpen = false;
        stopCollider.enabled = true;

        
        lightMaterial.color = Color.red;
    }
    
}
