using UnityEngine;

public class CameraControl_v3 : MonoBehaviour
{

    public GameObject CameraPillar;

    public bool
        CameraMouseScrollEnabled = false;

    public float
        CameraMoveSpeed = 20.0f,
        CameraZoomSpeed = 25.0f,
        CameraInitalHeight = 50.0f,
        CameraBorderThickness = 15.0f,
        CameraScrollSpeed = 200.0f,
        CameraVerticalLimitMax = 60.0f,
        CameraVerticalLimitMin = 40.0f;

    public Vector2
        CameraHorizontalLimit = new Vector2(-1.0f, -1.0f);

    private Vector3
        temp = new Vector3(0.0f, 0.0f, 0.0f);

    private float
        CameraStoreY = 0.0f;

    void Start()
    {
        CameraReset();
    }

    void Update()
    {
        Vector3 CameraPosition = transform.position;
        Vector3 PillarPosition = CameraPillar.transform.position;

        CameraStoreY = CameraPosition.y;

        if (Input.GetKeyDown(KeyCode.R))
            CameraReset();

        if (Input.GetKey(KeyCode.W) || CameraMouseScrollEnabled && (Input.mousePosition.y <= Screen.height - CameraBorderThickness))
        {
            temp = transform.forward * CameraMoveSpeed * Time.deltaTime;
            PillarPosition += temp;
            CameraPosition += temp;
        }

        if (Input.GetKey(KeyCode.S) || CameraMouseScrollEnabled && (Input.mousePosition.y >= CameraBorderThickness))
        {
            temp = -transform.forward * CameraMoveSpeed * Time.deltaTime;
            PillarPosition += temp;
            CameraPosition += temp;
        }

        if (Input.GetKey(KeyCode.A) || CameraMouseScrollEnabled && (Input.mousePosition.x >= Screen.width - CameraBorderThickness))
        {
            temp = -transform.right * CameraMoveSpeed * Time.deltaTime;
            PillarPosition += temp;
            CameraPosition += temp;
        }

        if (Input.GetKey(KeyCode.D) || CameraMouseScrollEnabled && (Input.mousePosition.x <= CameraBorderThickness))
        {
            temp = transform.right* CameraMoveSpeed * Time.deltaTime;
            PillarPosition += temp;
            CameraPosition += temp;
        }

        CameraPosition.y = CameraStoreY;
        PillarPosition.y = 0.5f;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        CameraPosition += 100 * scroll * transform.forward * CameraZoomSpeed * Time.deltaTime;
        CameraPosition.y = Mathf.Clamp(CameraPosition.y, CameraVerticalLimitMin, CameraVerticalLimitMax);

        if (CameraHorizontalLimit.x >= 0 && CameraHorizontalLimit.y >= 0)
        {
            CameraPosition.x = Mathf.Clamp(CameraPosition.x, -CameraHorizontalLimit.x, CameraHorizontalLimit.x);
            CameraPosition.z = Mathf.Clamp(CameraPosition.z, -CameraHorizontalLimit.y, CameraHorizontalLimit.y);
        }

        CameraPillar.transform.position = PillarPosition;
        transform.position = CameraPosition;
    }

    private void CameraReset()
    {
        float CameraXZ = Mathf.Sqrt(CameraInitalHeight * CameraInitalHeight / 2);
        transform.localPosition = new Vector3(CameraPillar.transform.position.x - CameraXZ, CameraInitalHeight, CameraPillar.transform.position.z - CameraXZ);
        transform.localRotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);
        transform.LookAt(CameraPillar.transform);
    }
}
