using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private InputDevice targetDevice;

    public bool showController = false;

    private Animator handAnim;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    //checks if the controllers are in use or if they have been activated yet
    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        //Browse the list of devices, and you can then access them
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        //checks for the first usable controllers
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnim = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
//            handAnim.SetFloat("Trigger", triggerValue);
        }
        else
        {
            //handAnim.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            //handAnim.SetFloat("Trigger", gripValue);
        }
        else
        {
            //handAnim.SetFloat("Trigger", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* CONTROLLER STUFF TO REMOVE LATER
        //if a button is being pressed on the controller
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            Debug.Log("Pressing Primary Button");
        }

        //shows a slide if the trigger button is being pressed
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            Debug.Log("Trigger pressed " + triggerValue);
        }

        //This is the joystick buttons, for movement etc
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Primary touchpad " + primary2DAxisValue);
        } */

        //checks if there is a device
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        } 
    }
}
