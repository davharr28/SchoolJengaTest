using UnityEngine;

/// <summary>
/// Controls the camera and the user input
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform currentStack;
    [SerializeField] private LayerMask blockLayer;
    private Vector3 startPos;
    Vector3 lastMousePosition;
    bool displayBlockInfoWindow = false;
    private void Start()
    {
        startPos = transform.position;
        ChangeStack(currentStack);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TurnOffBlockInfoWindow();

            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            transform.RotateAround(currentStack.position, Vector3.up, 20 * delta.x * Time.deltaTime);

            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, blockLayer))
            {
                displayBlockInfoWindow = true;
                hit.collider.GetComponent<BlockInfo>().DisplayInfo();
            }
        }

    }

    public void ChangeStack(Transform newStack)
    {
        currentStack = newStack;
        transform.position = new Vector3(currentStack.position.x, startPos.y, startPos.z);
        transform.rotation = Quaternion.Euler(15f, 0, 0);
    }
    private void TurnOffBlockInfoWindow()
    {
        if (displayBlockInfoWindow)
        {
            displayBlockInfoWindow = false;
            UIManager.Instance.CloseDisplayBlockWindow();
        }
    }


}


