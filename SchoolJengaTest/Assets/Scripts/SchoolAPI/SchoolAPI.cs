using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// To retrive the JSON data from the url and put into a data container 
/// </summary>
public  class SchoolAPI:MonoBehaviour
{
    private const string JSON_PATH_URL = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
    public static Subject Math { get; private set; }

    void Awake()
    {
        if(Math != null)
            Destroy(this.gameObject);
        
        StartCoroutine(RetriveData());
    }
    /// <summary>
    /// Retrieve the Json data from the Url
    /// </summary>
    /// <returns></returns>
    private IEnumerator RetriveData()
    {
        Math = new Subject("Math");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(JSON_PATH_URL))
        {

            string[] pages = JSON_PATH_URL.Split('/');
            int page = pages.Length - 1;

            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    FillData(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    /// <summary>
    /// Put Json Text to readable class data
    /// </summary>
    /// <param name="data"></param>
    private void FillData(string data)
    {

        SchoolTopic[] topics = JsonHelper.FromJsonArray<SchoolTopic>(data);

        foreach (SchoolTopic topic in topics)
        {
            if (topic.subject != "Math")
                continue;

            if (!Math.Grades.ContainsKey(topic.grade))
                Math.Grades.Add(topic.grade, new Grade());

            Math.Grades[topic.grade].topics.Add(topic);
        }
    }
  
}
