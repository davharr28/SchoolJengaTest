using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Build a stack of jenga 3 per roll
/// </summary>

public class StackBuilder : MonoBehaviour
{
    public GradeType grade = GradeType.sixth;
    [SerializeField] private GameObject JengaBlock;

    [SerializeField] private float jengaBlockHeight = 1f;
    [SerializeField] private float jengaBlockHeightSpace = .02f;
    [SerializeField] private float jengaBlockWidth = 2f;
    [SerializeField] private float jengaBlockWidthSpace = .5f;
    [SerializeField, ReadOnlyField] private float jengaBlockLength;

    // Start is called before the first frame update
    void Start()
    {

        jengaBlockLength = jengaBlockWidth * 3 + jengaBlockWidthSpace * 2;
        JengaBlock.transform.localScale = new Vector3(jengaBlockLength, jengaBlockHeight, jengaBlockWidth);
        
        //ToDo:change with a unity event listner on the API
        //Give time to download the data
       Invoke("SetupStackInfo",4f);

    }

    private void SetupStackInfo()
    {
        string gradeIn = "";
        switch (grade)
        {
            case GradeType.algebra:
                gradeIn = "Alebra 1";
                break;
            default:
                gradeIn = (int)grade + "th Grade";
                break;
        }
        gameObject.name = gradeIn;
        SchoolAPI.Math.Grades[gradeIn].topics.Sort();
        BuildStack(SchoolAPI.Math.Grades[gradeIn].topics);
    }


    private void BuildStack(List<SchoolTopic> schoolTopicGradeList)
    {
        int stack1Row = 1;
        int stack1Col = 0;
        Vector3 stack1CoorPlacement = transform.position;
        Quaternion stack1CoorRotation = Quaternion.Euler(Vector3.zero);
        float jengaBlockSpacing = (jengaBlockWidth + jengaBlockWidthSpace);
        foreach (SchoolTopic topic in schoolTopicGradeList)
        {
            if (stack1Row % 2 == 0)
                stack1CoorPlacement.x = transform.position.x + jengaBlockSpacing + stack1Col * -jengaBlockSpacing;
            else
                stack1CoorPlacement.z = transform.position.z + stack1Col * jengaBlockSpacing;

            stack1Col++;

            GameObject block = Instantiate(JengaBlock, stack1CoorPlacement, stack1CoorRotation, transform);

            //set up the variables for a new row
            if (stack1Col == 3)
            {
                stack1CoorPlacement.y += jengaBlockHeight + jengaBlockHeightSpace;
                stack1Row++;
                stack1Col = 0;
                if (stack1Row % 2 == 0)
                {
                    stack1CoorRotation.eulerAngles = Vector3.up * 90;
                    stack1CoorPlacement.z = transform.position.z + jengaBlockSpacing;
                }
                else
                {
                    stack1CoorRotation.eulerAngles = Vector3.zero;
                    stack1CoorPlacement.x = transform.position.x;
                }

            }
        }
    }
}
