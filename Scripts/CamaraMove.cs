using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    public float MoveSpeed;
    public GameObject MainCharacter;
    public float LengthFactor = 1.1f;
    public float WidthFactor = 2;
    public float GenericFactor = 0.4f;
    private float ZDistance;
    // Start is called before the first frame update
    void Start()
    {
        ZDistance = Mathf.Abs(MainCharacter.transform.position.z - transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCharacter.transform.position.y - transform.position.y > ZDistance * GenericFactor * LengthFactor)
        {
            transform.Translate( Vector3.up * MoveSpeed * Time.deltaTime, Space.World);
        }
        if (transform.position.y - MainCharacter.transform.position.y > ZDistance * GenericFactor * LengthFactor)
        {
            transform.Translate( Vector3.down * MoveSpeed * Time.deltaTime, Space.World);
        }
        if (MainCharacter.transform.position.x - transform.position.x > ZDistance * GenericFactor * WidthFactor)
        {
            transform.Translate( Vector3.right * MoveSpeed * Time.deltaTime, Space.World);
        }
        if (transform.position.x - MainCharacter.transform.position.x > ZDistance * GenericFactor * WidthFactor)
        {
            transform.Translate( Vector3.left * MoveSpeed * Time.deltaTime, Space.World);
        }
    }
}
