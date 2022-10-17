using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Slot : MonoBehaviour
{
    public GameObject ItemInSlot;
    public Image slotImage;
    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
    }

    /*public void OnTriggerStay(Collider other, InputAction.CallbackContext context)
    {
        if (ItemInSlot != null)
        {
            return;
        }

        GameObject obj = other.gameObject;

        if (!IsItem(obj))
        {
            return;
        }

        if (context.performed) //needs the inputaction for this to happen
        {
            InsertItem(obj);
        }
    }*/

    public void ItemsInSlot(InputAction.CallbackContext context, Collider other)
    {
        if (ItemInSlot != null)
        {
            return;
        }

        GameObject obj = other.gameObject;

        if (!IsItem(obj))
        {
            return;
        }

        if (context.performed) //needs the inputaction for this to happen
        {
            InsertItem(obj);
        }
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
    }

    private void InsertItem(GameObject obj)
    {
        obj.GetComponent<Rigidbody>();
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot = true;
        obj.GetComponent<Item>().currentSlot = this;
        ItemInSlot = obj;
        slotImage.color = Color.grey;
    }

    public void ResetColor()
    {
        slotImage.color = originalColor;
    }
}
