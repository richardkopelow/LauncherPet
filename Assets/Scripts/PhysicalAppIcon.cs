using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAppIcon : MonoBehaviour
{
    public AppEvent StartApp;
    public App App;
    public FloatValue PressThreshold;
    public FloatValue IconDragForce;
    public StringValue RoomLayer;

    private Rigidbody rigid;
    private bool pressed;
    private float pressTime;
    private Vector3 moveForce;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material iconMat = new Material(renderer.material);
        iconMat.SetTexture("_MainTex", App.Icon);
        renderer.material = iconMat;
    }

    private void OnMouseDown()
    {
        pressed = true;
        pressTime = Time.time;
    }

    private void OnMouseDrag()
    {
        if (pressed && Time.time - pressTime >= PressThreshold)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit,
                20,
                LayerMask.GetMask(RoomLayer)))
            {
                moveForce = (hit.point - rigid.position) * IconDragForce;
            }
        }
    }

    private void OnMouseUp()
    {
        pressed = false;
        if (Time.time - pressTime < PressThreshold)
        {
            StartApp.Fire(App);
        }
        moveForce = Vector3.zero;
    }

    private void FixedUpdate()
    {
        rigid.AddForce(moveForce);
    }
}
