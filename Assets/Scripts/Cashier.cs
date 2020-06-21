using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    [SerializeField]
    private CashRegister _cashRegister;

    [SerializeField]
    private SpeechBubble _speechBubble;

    [SerializeField]
    private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        if (_cashRegister != null)
        {
            _cashRegister.OnClick += CashRegisterClicked;

        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        if (_speechBubble == null)
        {
            _speechBubble = GetComponentInChildren<SpeechBubble>();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Wave");
        }
    }

    public void CashRegisterClicked(List<Item> items)
    {
        if (_speechBubble != null)
        {
            _speechBubble.Text = BuildStringFromItems(items);
            _speechBubble.Show();
        }
    }

    private string BuildStringFromItems(List<Item> items)
    {
        if (items == null || items.Count == 0)  { return "Hello, how can i help you"; }
        string outString = "";
        Dictionary<string, (float price, int count)> itemDictionary = new Dictionary<string, (float price, int count)>();
        List<string> itemNames = new List<string>();
        float totalPrice = 0f;
        foreach(var item in items)
        {
            if (itemDictionary.ContainsKey(item.Name))
            {
                var currentItemCount = itemDictionary[item.Name];
                var itemCount = (price: item.Price + currentItemCount.price, count: 1 + currentItemCount.count);
                itemDictionary[item.Name] = itemCount;
            }
            else
            {
                var itemCount = (price: item.Price, count: 1);
                itemDictionary.Add(item.Name, itemCount);
                itemNames.Add(item.Name);
            }
        }

        foreach (var itemName in itemNames)
        {
            var itemCount = itemDictionary[itemName];
            totalPrice += itemCount.price;
            outString += string.Format("{0}x {1}: {2:0.00}€\n", itemCount.count, itemName, itemCount.price);
        }

        outString += string.Format("Total price: {0:0.00}€\n", totalPrice);

        return "Hello, do you want to buy: \n" + outString;
    }
}
