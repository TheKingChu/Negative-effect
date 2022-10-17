using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererSettings : MonoBehaviour
{
    [SerializeField] LineRenderer rend;

    Vector3[] points;

    public LayerMask layerMask;

    public GameObject panel;
    public Image image;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<LineRenderer>();
        image = panel.GetComponent<Image>();

        points = new Vector3[2];
        points[0] = Vector3.zero;
        points[1] = transform.position + new Vector3(0, 0, 20);

        rend.SetPositions(points);
        rend.enabled = true;
    }

    private void Update()
    {
        AlignLineRenderer(rend);

        if(AlignLineRenderer(rend) && Input.GetAxis("Submit") > 0)
        {
            button.onClick.Invoke();
        }
    }

    public bool AlignLineRenderer(LineRenderer rend)
    {
        bool isHit = false;
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, layerMask))
        {
            button = hit.collider.gameObject.GetComponent<Button>();

            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;

            isHit = true;
        }
        else
        {
            points[1] = transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.green;
            rend.endColor = Color.green;

            isHit = false;
        }

        rend.SetPositions(points);
        rend.material.color = rend.startColor;
        return isHit;
    }

    public void OnClick()
    {
        //example stuff
        if(button != null)
        {
            if(button.name == "red_button")
            {
                image.color = Color.red;
            }
        }
    }
}
