﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PHPCommunicationManager : MonoBehaviour
{
    //determines where the records will be stored on the server
    public enum BuildType
    {
        Male,
        Female,
        Pilot
    };

    public BuildType buildType = BuildType.Male; 

    private static PHPCommunicationManager _instance;

    //private static string SERVER = "http://localhost/AR_AT";
    private static string SERVER = "https://projects.eng.unimelb.edu.au/ar-attention";


    private void Awake()
    {

        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //read previous partocipant number
    //this is used to alternate starting event type
    /*
    public string getLastParticipantNumber()
    {
        string pid = "";
        using (UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "/scripts/getParticipantID.php"))
        {
            uwr.SendWebRequest();

            
            WaitForSeconds w;
            while (!uwr.isDone)
            {
                w = new WaitForSeconds(0.1f);
            }
           
            if (uwr.isNetworkError)
            {
                Debug.Log("error");
            }
            else
            {
                pid = uwr.downloadHandler.text.Trim(' ');
            }
            

            Debug.Log("response = " + pid.Length);
            return pid;
        }
    }
    */

    public IEnumerator GetPID(InstructionController instructionController)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(SERVER + "/scripts/"+buildType.ToString()+"/getParticipantID.php"))
        {
            uwr.SendWebRequest();
            while (!uwr.isDone)
            {
                yield return null;
            }
            if (uwr.isNetworkError)
            {
                Debug.Log("error");
                instructionController.SetCanvasText("Error setting up. Please try again");
            }
            else
            {
                Debug.Log("pid in comm manager ="+ uwr.downloadHandler.text);
                instructionController.SetSettingsID(uwr.downloadHandler.text.Trim(' '));
            }


            yield return null;
        }
    }

    public void SetParticipantNumber(int id)
    {
        WWWForm form = new WWWForm();

        form.AddField("pId", id.ToString());

        StartCoroutine(PostForm(SERVER + "/scripts/"+buildType.ToString()+"/setParticipantID.php", form));

    }

    IEnumerator PostForm(string uri, WWWForm form)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Post(uri, form))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
                SceneManager.LoadScene("MTurkError");
            }
            else
            {
                Debug.Log(uwr.downloadHandler.text);
                if(uwr.downloadHandler.text.Trim(' ').Equals("error"))
                {
                    SceneManager.LoadScene("MTurkError");
                }
            }
        }
    }
    

    //sends the current generated participant number along with the current conditions
    //Example: participant number = p1, currentCondition = pyy
    // different conditions =  eventType_AR_Task
    // so vyn = virtual event, yes - AR elements, no Task.
    //usually getLastParticipantNumber() + 1
    //this will determine the file name.
    public void sendReactionTimeEvent(string folderName, string fileName, string reactionType, float reactionTime, int ledNumber)
    {
        //Debug.Log(folderName + ":" + fileName + ":" + reactionType + " " + reactionTime + " " + ledNumber);
        //Debug.Log(folderName + ":" + fileName + ":" + reactionType + " " + reactionTime + " " + ledNumber);

        WWWForm form = new WWWForm();
        form.AddField("ledNumber", ledNumber);
        form.AddField("reactionTime", reactionTime.ToString());
        form.AddField("reactionType", reactionType);
        form.AddField("fileName", fileName);
        form.AddField("folderName", folderName);

        StartCoroutine(PostForm(SERVER + "/scripts/"+buildType.ToString()+"/saveReactionTime.php", form));
    }


    //type is either - counted or actual
    //counted denotes that the participant counted this number 
    //actual denotes that actual number of passes
    public void sendPassCount(string folderName, string fileName, int passCount, string type)
    {
        //Debug.Log(folderName + ":"+ fileName + ":" + passCount + " " + type);

        WWWForm form = new WWWForm();
        form.AddField("passCount", passCount);
        form.AddField("countType", type);
        form.AddField("fileName", fileName);
        form.AddField("folderName", folderName);

        StartCoroutine(PostForm(SERVER + "/scripts/"+buildType.ToString()+"/savePassCount.php", form));
    }


    //send the generated code at the end to ensure completion
    //always writes to a file named: participant_codes
    public void sendCode(int pid, string generatedCode)
    {
        //Debug.Log(pid + ":" + generatedCode);

        WWWForm form = new WWWForm();
        form.AddField("pid", pid);
        form.AddField("code", generatedCode);

        StartCoroutine(PostForm(SERVER + "/scripts/"+buildType.ToString()+"/saveParticipantCode.php", form));
    }
}
