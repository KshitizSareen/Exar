using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 previousPosition;
    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.1f;
    float ZoomMinBound = 10.0f;
    float ZoomMaxBound = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch1 = Input.GetTouch(0);
            int fingerId = touch1.fingerId;
            if (!EventSystem.current.IsPointerOverGameObject(fingerId))
            {

                if (Input.touchCount == 2)
                {

                    // get current touch positions
                    Touch tZero = Input.GetTouch(0);
                    Touch tOne = Input.GetTouch(1);
                    // get touch position from the previous frame
                    Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                    Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                    float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                    float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                    // get offset value
                    float deltaDistance = oldTouchDistance - currentTouchDistance;
                    Zoom(deltaDistance, TouchZoomSpeed);
                    return;
                }
                else if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector2 firstPostion = touch.position;

                        previousPosition = cam.ScreenToViewportPoint(new Vector3(firstPostion.x, firstPostion.y, 0));
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 currPosition = touch.position;
                        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(new Vector3(currPosition.x, currPosition.y, 0));
                        cam.transform.position = new Vector3();
                        cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                        cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                        cam.transform.Translate(new Vector3(0, 0, -3.5f));
                        previousPosition = cam.ScreenToViewportPoint(currPosition);


                    }
                }
            }
        }
    }
    void Zoom(float deltaMagnitudeDiff, float speed)
    {

        cam.fieldOfView += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, ZoomMinBound, ZoomMaxBound);
    }
}
