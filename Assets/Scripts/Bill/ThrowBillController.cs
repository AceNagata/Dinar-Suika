using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBillController : MonoBehaviour
{
    public static ThrowBillController instance;

    public GameObject CurrentBill { get; set; }
    [SerializeField] private Transform _billTransform;
    [SerializeField] private Transform _parentAfterThrow;
    [SerializeField] private BillSelector _selector;

    private PlayerController _playerController;

    private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;

    public Bounds Bounds { get; private set; }

    private const float EXTRA_WIDTH = 0.03f;

    public bool CanThrow { get; set; } = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();

        SpawnABill(_selector.PickRandomBillForThrow());
    }

    private void Update()
    {
        if (UserInput.IsThrowPressed && CanThrow)
        {
            SpriteIndex index = CurrentBill.GetComponent<SpriteIndex>();
            Quaternion rot = CurrentBill.transform.rotation;

            GameObject go = Instantiate(BillSelector.instance.Bills[index.Index], CurrentBill.transform.position, rot);
            go.transform.SetParent(_parentAfterThrow);

            Destroy(CurrentBill);

            CanThrow = false;
        }
    }

    public void SpawnABill(GameObject bill)
    {
        GameObject go = Instantiate(bill, _billTransform);
        CurrentBill = go;
        _circleCollider = CurrentBill.GetComponent<CircleCollider2D>();
        Bounds = _circleCollider.bounds;

        _playerController.ChangeBoundary(EXTRA_WIDTH);
    }
}
