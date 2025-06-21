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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _layerIndex)
        {
            BillInfo info = collision.gameObject.GetComponent<BillInfo>();
            if (info != null)
            {
                if (info.BillIndex == _info.BillIndex)
                {
                    int thisID = gameObject.GetInstanceID();
                    int otherID = collision.gameObject.GetInstanceID();

                    if (thisID > otherID)
                    {
                        GameManager.instance.IncreaseScore(_info.PointsWhenAnnihilated);

                        if (_info.BillIndex == BillSelector.instance.Bills.Length -1)
                        {
                            Destroy(collision.gameObject);
                            Destroy(gameObject);
                        }

                        else
                        {
                            Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
                            GameObject go = Instantiate(SpawnCombinedBill(_info.BillIndex), GameManager.instance.transform);
                            go.transform.position = middlePosition;

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
            }
        }
    }

    private GameObject SpawnCombinedBill(int index)
    {
        GameObject go = BillSelector.instance.Bills[index + 1];
        return go;
    }
}
