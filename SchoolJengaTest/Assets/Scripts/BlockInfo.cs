using UnityEngine;

/// <summary>
/// In charge of changing  the block based of the user action and it's topic
/// </summary>
public class BlockInfo : MonoBehaviour
{
    [HideInInspector] public int mastery = 0;

    [SerializeField] private Material glassMat;
    [SerializeField] private Material woodMat;
    [SerializeField] private Material stoneMat;

    private SchoolTopic topic;

    private MeshRenderer meshRenderer;
    private Color32 currentColor;
    private Color hoverColor = Color.red;

    private Rigidbody rb;
    private Vector3 postion;
    private Quaternion rotation;

    public void SetupBlock(SchoolTopic topic)
    {
        this.topic = topic;
        rb = GetComponent<Rigidbody>();
        gameObject.name = this.topic.id.ToString();
        ChangeBlockMaterial(topic.mastery);
        postion = this.transform.position;
        rotation = this.transform.rotation;
    }
    private void ChangeBlockMaterial(int mastery)
    {
        this.mastery = mastery;
        Material m = null;

        switch (mastery)
        {
            case 0:
                m = glassMat;
                break;
            case 1:
                m = woodMat;
                break;
            case 2:
                m = stoneMat;
                break;
            default:
                break;
        }
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = m;
        currentColor = meshRenderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (meshRenderer)
        {
            meshRenderer.material.color = hoverColor;
        }

    }
    private void OnMouseExit()
    {
        if (meshRenderer)
        {
            meshRenderer.material.color = currentColor;
        }
    }

    public void DisplayInfo()
    {
        UIManager.Instance.DisplayBlockWindow(topic.grade, topic.domain, topic.cluster, topic.standardid, topic.standarddescription);
    }
    public void ResetStack()
    {
        rb.Sleep();

        if (mastery == 0)
        {
            gameObject.SetActive(true);
        }

        transform.position = postion;
        transform.rotation = rotation;
    }


}

