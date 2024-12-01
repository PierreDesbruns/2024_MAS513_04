using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Update timer value from timestamp.
/// Timer format is: m:ss
/// </summary>
public class TimestampUpdater : MonoBehaviour
{
    [Tooltip("Object receiving data from server.")]
    [SerializeField] DataReceiver dataReceiver;
    TMP_Text timestampText;

    // Start is called before the first frame update
    void Start()
    {
        timestampText = GetComponent<TMP_Text>();
        timestampText.text = "0:00";
    }

    // Update is called once per frame
    void Update()
    {
        int timestamp = dataReceiver.timestamp;

        int seconds = (timestamp / 1000) % 60;
        int minutes = timestamp / 60000;

        // Updating text
        // Adding a 0 to seconds if less than 10 (01, 02, 03, etc.)
        timestampText.text = seconds < 10 ?
            minutes.ToString() + ":" + "0" + seconds.ToString() :
            minutes.ToString() + ":" + seconds.ToString();
    }
}
