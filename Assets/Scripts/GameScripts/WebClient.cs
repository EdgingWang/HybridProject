using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebClient : MonoBehaviour
{
    private string baseUri, uriCheckIn, uriGetRevolutions;

    public static bool currentIsCheckedIn = false;

    [SerializeField]
    private Text infoDisplay;

    [SerializeField]
    private ProgressController progressController;

    // Start is called before the first frame update
    void Start()
    {
        string host = "http://192.168.1.185";
        string port = "3333";
        baseUri = host + ":" + port + "/";

        uriGetRevolutions = baseUri + "revolutions?uuid=" + SystemInfo.deviceUniqueIdentifier;
        uriCheckIn = baseUri + "checkIn?uuid=" + SystemInfo.deviceUniqueIdentifier;
    }

    public void getRevolutions()
    {
        Debug.Log("in get revolutions");
        StartCoroutine(apiGetRevolutions());
    }

    public void checkIn()
    {
        Debug.Log("in check in");
        StartCoroutine(apiCheckIn());
    }

    private IEnumerator apiCheckIn()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(uriCheckIn))
        {
            Debug.Log("api check in url: " + uriCheckIn);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                infoDisplay.text = "Error checking in!";
            }
            else
            {
                Debug.Log("unity response: " + www.downloadHandler.text);
                setIsCheckedIn(JsonUtility.FromJson<CheckIn>(www.downloadHandler.text).isCheckedIn);
                infoDisplay.text = "Check-in successful!";
            }
        }
    }

    private IEnumerator apiGetRevolutions()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(uriGetRevolutions))
        {
            Debug.Log("api get revolutions url: " + uriGetRevolutions);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                infoDisplay.text = "Error getting revolutions!";
            }
            else
            {
                Debug.Log("unity response: " + www.downloadHandler.text);
                setRevolutions(JsonUtility.FromJson<Revolutions>(www.downloadHandler.text).revolutions);
                infoDisplay.text = "Revolutions received!";
            }
        }
    }

    private void setRevolutions(int revolutions)
    {      
        Debug.Log("old revolutions: " + progressController.getTotalRevolutions());
        progressController.updateRevolutions(revolutions);
        Debug.Log("new revolutions: " + progressController.getTotalRevolutions());
    }

    private void setIsCheckedIn(bool isCheckedIn)
    {
        Debug.Log("old checked in status: " + currentIsCheckedIn);
        currentIsCheckedIn = isCheckedIn;
        Debug.Log("new checked in status: " + currentIsCheckedIn);
    }

    [SerializeField]
    private class Revolutions
    {
        public int revolutions;
    }

    [SerializeField]
    private class CheckIn
    {
        public bool isCheckedIn;
    }
}
