using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillCombiner : MonoBehaviour
{
    private int _layerIndex;
    private BillInfo _info;

    private void Awake()
    {
        _info = GetComponent<BillInfo>();
        _layerIndex = gameObject.layer;

        if (_info == null)
        {
            Debug.LogError($"?? BillCombiner started on '{gameObject.name}', but missing BillInfo! (Parent: {transform.parent?.name}, Scene: {gameObject.scene.name})");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_info == null)
        {
            Debug.LogError($"?? Collision triggered on '{gameObject.name}' without BillInfo!");
            return;
        }

        if (collision.gameObject.layer != _layerIndex)
            return;

        BillInfo info = collision.gameObject.GetComponent<BillInfo>();
        if (info == null)
        {
            Debug.LogError($"?? The object '{collision.gameObject.name}' collided but has no BillInfo.");
            return;
        }

        if (info.BillIndex == _info.BillIndex)
        {
            int thisID = gameObject.GetInstanceID();
            int otherID = collision.gameObject.GetInstanceID();
            if (thisID > otherID)
            {
                GameManager.instance.IncreaseScore(_info.PointsWhenAnnihilated);

                if (_info.BillIndex == BillSelector.instance.Bills.Length - 1)
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    return;
                }

                Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
                GameObject go = Instantiate(SpawnCombinedBill(_info.BillIndex), middlePosition, Quaternion.identity, GameManager.instance.transform);

                ColliderInformer informer = go.GetComponent<ColliderInformer>();
                if (informer != null)
                {
                    informer.WasCombinedIn = true;
                }

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private GameObject SpawnCombinedBill(int index)
    {
        return BillSelector.instance.Bills[index + 1];
    }
}
