using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

/// <summary>
/// Establish connection with server and receive data from it.
/// See ReceiveData() method for more information about data format.
/// </summary>
public class DataReceiver : MonoBehaviour
{
    UdpClient pythonServer; // object from which data is received

    [Tooltip("IP address of server.")]
    [SerializeField] string ip = "127.0.0.1";
    [Tooltip("Receiving port of headset.")]
    [SerializeField] int port = 41621;

	// Data received from python server (read-only attributes)
	[HideInInspector] public int timestamp { get; private set; }
    [HideInInspector] public float thumbForceValue { get; private set; }
    [HideInInspector] public float indexForceValue { get; private set; }
    [HideInInspector] public float middleForceValue { get; private set; }
    // [HideInInspector] public float ringForceValue { get; private set; }
    // [HideInInspector] public float pinkyForceValue { get; private set; }
    // [HideInInspector] public Vector3 handPosition { get; private set; }
    // [HideInInspector] public Vector3 handOrientation { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        pythonServer = new UdpClient(port);

        timestamp = 0;
        thumbForceValue = 0;
        indexForceValue = 0;
        middleForceValue = 0;
        // ringForceValue = 0;
        // pinkyForceValue = 0;
        // handPosition = new Vector3(0,0,0);
        // handOrientation = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        // Reading data from server
        pythonServer.BeginReceive(new AsyncCallback(ReceiveData), pythonServer);
    }

    /// <summary>
    /// Read UDP sockets from Python local server containing haptic data.
    /// Callback function for BeginReceive().
    /// </summary>
    /// <remarks>
    /// Socket message must be column-separated format: <c>timestamp;thumb;index;middle</c>
    /// with:
    /// <list type="bullet">
    /// <item><description>
    /// (int) <c>timestamp</c> being the timestamp of measured value (number of milliseconds from begining). Must be positive.
    /// </description></item>
    /// <item><description>
    /// (float) <c>thumb</c>,<c>index</c>,<c>middle</c> being the haptic value associated with thumb, index, and middle finger respectively. Must be between 0 and 1.
    /// </description></item>
    /// </remarks>
    private void ReceiveData(IAsyncResult result)
    {
        UdpClient pythonServer = result.AsyncState as UdpClient;
        IPEndPoint source = new IPEndPoint(IPAddress.Parse(ip), port);
        byte[] dataAsByte = pythonServer.EndReceive(result, ref source);
        string[] dataAsStringArray = Encoding.UTF8.GetString(dataAsByte).Split(';');

        // Checking data format
        if (dataAsStringArray.Length != 4) // change length here if different message format
        {
            Debug.LogWarning("Received wrong data format.");
        }
        else
        {
            // Updating timestamp
            timestamp = int.Parse(dataAsStringArray[0]);

            // Updating haptic value to corresponding finger
            thumbForceValue = float.Parse(dataAsStringArray[1], CultureInfo.InvariantCulture);
            indexForceValue = float.Parse(dataAsStringArray[2], CultureInfo.InvariantCulture);
            middleForceValue = float.Parse(dataAsStringArray[3], CultureInfo.InvariantCulture);
            // ringForceValue = float.Parse(dataAsStringArray[], CultureInfo.InvariantCulture);
            // pinkyForceValue = float.Parse(dataAsStringArray[], CultureInfo.InvariantCulture);
        }
    }
}
