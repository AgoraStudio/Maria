using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float MoveSpeed;
    public Edge Left;
    public Edge Right;
    public Edge Up;
    public Edge Down;
    internal bool CanMove = true;
    private bool IsAutoMove;
    private GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !Left.IsTrigger && CanMove)
        {
            transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) && !Right.IsTrigger && CanMove)
        {
            transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) && !Up.IsTrigger && CanMove)
        {
            transform.position += Vector3.up * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && !Down.IsTrigger && CanMove)
        {
            transform.position += Vector3.down * MoveSpeed * Time.deltaTime;
        }
        if (IsAutoMove && !CanMove)
        {
            AutoMove();
        }
        else if (IsAutoMove)
        {
            IsAutoMove = false;
        }
    }

    private void AutoMove()
    {
        Vector3 dir = Target.transform.position - transform.position;
        transform.Translate(dir.normalized * MoveSpeed * Time.deltaTime, Space.World);
        if (Vector3.Distance(Target.transform.position, transform.position) < MoveSpeed * Time.deltaTime)
        {
            IsAutoMove = false;
        }
    }

    public void AutoMoveStart(GameObject target)
    {
        Target = target;
        IsAutoMove = true;
    }
}
