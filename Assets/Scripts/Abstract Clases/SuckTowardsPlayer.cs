using UnityEngine;

public abstract class SuckTowardsPlayer : MonoBehaviour
{
    [SerializeField]
    public bool LookToPlayer=false;
    public float PlayerDetect = 15f;
    public float moveSpeed = 3f;

    protected Vector3 dir=Vector3.zero;
    
    public void Start()=> dir = GameManager.instance.PlayerPos.position - transform.position;
     
    public virtual void LateUpdate()=> GoToPlayer();
 
  
    protected virtual void GoToPlayer()
    {
        dir = GameManager.instance.PlayerPos.position - transform.position;

        if (PlayerDetect > dir.magnitude && dir.magnitude>0)
        {
            transform.position += Vector3.ClampMagnitude(dir.normalized * Time.deltaTime * (moveSpeed/dir.magnitude),dir.magnitude);
            if (LookToPlayer)
            {
                transform.right = dir.normalized;
            }
        }
       
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //Gizmos.DrawLine(transform.position, GameManager.instance.PlayerPos.position);
     
    }
}
