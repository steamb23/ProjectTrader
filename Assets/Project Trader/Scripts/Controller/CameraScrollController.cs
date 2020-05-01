using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraScrollController : MonoBehaviour
{
    public new Camera camera;
    public PixelPerfectCamera pixelPerfectCamera;

    public Vector2 cameraRegion;
    public Vector2 cameraRegionOffset;
    public Vector2 referenceResolution = new Vector2(6.4f, 3.6f);

    [Min(0)]
    public float decelerationRate = 1.1f;

    // Start is called before the first frame update

    public Rect CameraRegion
    {
        get => new Rect((Vector2)transform.position + cameraRegionOffset - cameraRegion / 2, cameraRegion);
        set
        {
            cameraRegion = new Vector2(value.x, value.y);
            cameraRegionOffset = new Vector2(value.width, value.height);
        }
    }

    private int Ratio
    {
        get
        {
            var ratio = 1;
            if (pixelPerfectCamera != null)
            {
                ratio = pixelPerfectCamera.pixelRatio;
            }
            return ratio;
        }
    }

    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        if (pixelPerfectCamera == null)
        {
            pixelPerfectCamera = camera.GetComponent<PixelPerfectCamera>();
        }
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var deltaRatio = Time.deltaTime * 60;
        camera.transform.Translate(cameraVelocity * deltaRatio);
        var decelerationRate = this.decelerationRate * deltaRatio;
        if (decelerationRate > 1)
            decelerationRate = 1;
        cameraVelocity *= 1 - decelerationRate;


        // 카메라 영역 고정
        var cameraRegion = CameraRegion;

        var referenceResolutionX = referenceResolution.x / 2;
        var referenceResolutionY = referenceResolution.y / 2;

        var temp = camera.transform.position;
        if (referenceResolution.x < cameraRegion.width)
        {
            if (camera.transform.position.x + referenceResolutionX > cameraRegion.xMax)
            {
                temp.x = cameraRegion.xMax - referenceResolutionX;
            }
            if (camera.transform.position.x - referenceResolutionX < cameraRegion.xMin)
            {
                temp.x = cameraRegion.xMin + referenceResolutionX;
            }
        }
        else
        {
            temp.x = transform.position.x + cameraRegionOffset.x;
        }
        if (referenceResolution.y < cameraRegion.height)
        {
            if (camera.transform.position.y + referenceResolutionY > cameraRegion.yMax)
            {
                temp.y = cameraRegion.yMax - referenceResolutionY;
            }
            if (camera.transform.position.y - referenceResolutionY < cameraRegion.yMin)
            {
                temp.y = cameraRegion.yMin + referenceResolutionY;
            }
        }
        else
        {
            temp.y = transform.position.y + cameraRegionOffset.y;
        }
        camera.transform.position = temp;
    }

    Vector2 cameraVelocity;
    Vector2 previousDragPos;
    public void DragBegin(BaseEventData eventData)
    {
        var data = eventData as PointerEventData;

        cameraVelocity = Vector2.zero;
        previousDragPos = data.position;
    }
    public void Drag(BaseEventData eventData)
    {
        var data = eventData as PointerEventData;

        var ratio = Ratio;

        camera.transform.Translate((previousDragPos - data.position) / 100 / ratio);
        previousDragPos = data.position;
    }
    public void DragEnd(BaseEventData eventData)
    {
        var data = eventData as PointerEventData;
        var ratio = Ratio;
        cameraVelocity = (previousDragPos - data.position) / 100 / ratio;
    }

    Rect CameraRect
    {
        get
        {
            var cameraRect = camera.rect;
            cameraRect.width *= camera.orthographicSize;
            cameraRect.height *= camera.orthographicSize;

            cameraRect.center = camera.transform.position;

            return cameraRect;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var origColor = UnityEditor.Handles.color;
        UnityEditor.Handles.color = Color.green;
        // 직사각형
        Rect rect = CameraRegion;

        Handles.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMax, rect.yMin));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMin), new Vector3(rect.xMax, rect.yMax));
        Handles.DrawLine(new Vector3(rect.xMax, rect.yMax), new Vector3(rect.xMin, rect.yMax));
        Handles.DrawLine(new Vector3(rect.xMin, rect.yMax), new Vector3(rect.xMin, rect.yMin));

        UnityEditor.Handles.color = origColor;
    }
#endif
}
