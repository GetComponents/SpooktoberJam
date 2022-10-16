using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float throwSpeed;
    [SerializeField]
    MeshCollider myCollider;
    public bool AmFlying;
    public BottleSpawner MySpawner;

    public void GetPickedUp()
    {
        rb.isKinematic = true;
        myCollider.isTrigger = true;
        myCollider.transform.SetParent(PlayerActions.Instance.hand);
        myCollider.transform.position = PlayerActions.Instance.hand.position;
    }

    public void ThrowStone(Vector3 _direction)
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.Normalize(_direction) * throwSpeed, ForceMode.Impulse);
        myCollider.transform.parent = null;
        myCollider.isTrigger = false;
        StartCoroutine(ChangeState());
    }

    public IEnumerator ChangeState()
    {
        AmFlying = true;
        yield return new WaitForSeconds(1.5f);
        AmFlying = false;
    }

    private void OnDestroy()
    {
        MySpawner.myBottle = null;
    }
}
