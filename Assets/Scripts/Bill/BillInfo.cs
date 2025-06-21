using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillInfo : MonoBehaviour
{
    public int BillIndex = 0;
    public int PointsWhenAnnihilated = 1;
    public float BillMass = 1f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.mass = BillMass;
    }
}
