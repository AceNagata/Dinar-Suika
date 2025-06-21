using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillSelector : MonoBehaviour
{
    public static BillSelector instance;

    public GameObject[] Bills;
    public GameObject[] NoPhysicsBills;
    public int HighestStartingIndex = 3;

    [SerializeField] private Image _nextBillImage;
    [SerializeField] private Sprite[] _billSprites;

    public GameObject NextBill { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PickNextBill();
    }

    public GameObject PickRandomBillForThrow()
    {
        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        if (randomIndex < NoPhysicsBills.Length)
        {
            GameObject randomBill = NoPhysicsBills[randomIndex];
            return randomBill;
        }

        return null;
    }

    public void PickNextBill()
    {
        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        if (randomIndex < Bills.Length)
        {
            GameObject nextBill = NoPhysicsBills[randomIndex];
            NextBill = nextBill;

            _nextBillImage.sprite = _billSprites[randomIndex];
        }
    }
}
