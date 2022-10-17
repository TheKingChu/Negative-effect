using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public TextureSaving ts;

    public GameManager gm;
    public GameObject film;
    public GameObject[] notes;

    //Inventory popout VR 
    public GameObject inventory;
    public GameObject anchor;
    bool UIactive;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("__GM").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        inventory.SetActive(false);
        UIactive = false;
    }

    public void UIopen(InputAction.CallbackContext context)
    {
        #region POP out inventory (not for use in game)
        if (context.performed) //this will not work but its just to test something
        {
            UIactive = !UIactive;
            inventory.SetActive(UIactive);
        }

        if (UIactive)
        {
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }
        #endregion
    }

    /*public void OnCollisionEnter(Collision collision, InputAction.CallbackContext context)
    {
        if (collision.gameObject.CompareTag("RightHand") || collision.gameObject.CompareTag("LeftHand") && context.performed)
        {
            InstantiateFilm();
            InstantiateNotes();
            UIactive = !UIactive;
            inventory.SetActive(UIactive);
            Debug.Log("collision works opening inv");
        }

        if (UIactive)
        {
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }

        else
        {
            Debug.Log("collision doesnt work inv");
        }
    }*/

    public void VrpopoutMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InstantiateFilm();
            InstantiateNotes();
            UIactive = !UIactive;
            inventory.SetActive(UIactive);
            Debug.Log("collision works opening inv");
        }

        if (UIactive)
        {
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }

        else
        {
            Debug.Log("collision doesnt work inv");
        }
    }

    public void InstantiateFilm()
    {
        Debug.Log("grabbed a film, do you have more than 0?");

        if (gm.film >= 1)
        {
            gm.film--;
            Instantiate(film,gameObject.transform.position,quaternion.identity);
            Debug.Log("Film has been instatiated");
        }
    }

    public void InstantiateNotes()
    {
        Debug.Log("Notes has been instatiated");
    }
}
