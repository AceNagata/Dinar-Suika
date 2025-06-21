using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInformer : MonoBehaviour
{
    public bool WasCombinedIn { get; set; }

    private bool _hasCollided;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_hasCollided && !WasCombinedIn)
        {
            _hasCollided = true;
            ThrowBillController.instance.CanThrow = true;
            ThrowBillController.instance.SpawnABill(BillSelector.instance.NextBill);
            BillSelector.instance.PickNextBill();
            Destroy(this);
        }
    }
}
