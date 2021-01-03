using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioRecordingScript : MonoBehaviour
{
    bool isRecording = false;

    AudioClip recording;

    int seconds = 20;
    int frequency = 44100;

    float startRecordingTime;

    string micID = "Microphone (Realtek(R) Audio)";
    string path = "";

    void Start()
    {
        path = Path.Combine(Application.dataPath, "my_clip");
    }

    void StartRecording()
    {
        int minFreq;
        int maxFreq;
        int freq = 44100;
        Microphone.GetDeviceCaps(micID, out minFreq, out maxFreq);
        if (maxFreq < 44100)
            freq = maxFreq;

        //Start the recording, the length of 300 gives it a cap of 5 minutes
        recording = Microphone.Start(micID, false, 300, freq);
        startRecordingTime = Time.time;
    }

    void EndRecording()
    {
        //End the recording when the mouse comes back up, then play it
        Microphone.End(micID);

        //Trim the audioclip by the length of the recording
        AudioClip recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
        float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
        recording.GetData(data, 0);
        recordingNew.SetData(data, 0);
        this.recording = recordingNew;

        SaveClip();

        PostRequest();
        //Play recording
        //audioSource.clip = recording;
        //audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isRecording)
        {
            isRecording = true;
            Debug.Log("Recording");
            StartRecording();
        }

        if(Input.GetKeyUp(KeyCode.Space) && isRecording)
        {
            isRecording = false;
            Debug.Log("Not recording");
            EndRecording();
            //SaveClip();
            //PostRequest();
        }
    }

    void SaveClip()
    {
        SavWav.Save(path, recording);
    }

    public byte[] FloatToByteArray(float[] floatArray)
    {
        int len = floatArray.Length * 4;
        byte[] byteArray = new byte[len];
        int pos = 0;
        foreach (float f in floatArray)
        {
            byte[] data = System.BitConverter.GetBytes(f);
            System.Array.Copy(data, 0, byteArray, pos, 4);
            pos += 4;
        }
        return byteArray;
    }

    void PostRequest()
    {
        //float[] floatArray = new float[recording.samples * recording.channels];
        //recording.GetData(floatArray, 0);

        byte[] bytes = File.ReadAllBytes(path + ".wav");

        //byte[] bytes = FloatToByteArray(floatArray);

        string bytesString = Convert.ToBase64String(bytes);

        var client = new RestSharp.RestClient("http://127.0.0.1:5000/speechToText");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        string json = "{\"frequency\": " + frequency + ", \"audio\":\"" +
            bytesString + "\", \"path\": \"audio2.wav\"}";
        Debug.Log(json);
        request.AddParameter("application/json",
            json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
    }
}
