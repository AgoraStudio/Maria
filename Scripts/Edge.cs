using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private Collider2D Collider;
    public bool IsTrigger { get; set; } 
    // Start is called before the first frame update
    void Start()
    {
        Collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTrigger = false;
    }
}
