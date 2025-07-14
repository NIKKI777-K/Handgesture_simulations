using UnityEngine;
using System.IO.Ports;

public class TwoFingerFlex : MonoBehaviour
{
    SerialPort serialPort;
    public string portName = "COM11";  
    public int baudRate = 9600;

    // Index Finger joints
    public Transform index1, index2, index3;

    // Middle Finger joints
    public Transform middle1, middle2, middle3;

    // Calibration range for index finger
    public float indexMin = 750f;
    public float indexMax = 950f;

    // Calibration range for middle finger
    public float middleMin = 690f;
    public float middleMax = 750f;

    public float minAngle = -110f;
    public float maxAngle = 90f;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 100;

        try
        {
            serialPort.Open();
            Debug.Log("Serial port opened.");
            System.Threading.Thread.Sleep(2000); // Give Arduino time to reset
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();  // Expected: value1,value2
                string[] values = data.Split(',');

                if (values.Length == 2)
                {
                    int indexValue = int.Parse(values[0]);
                    int middleValue = int.Parse(values[1]);

                    float indexAngle = Map(indexValue, indexMin, indexMax, minAngle, maxAngle);
                    float middleAngle = Map(middleValue, middleMin, middleMax, minAngle, maxAngle);

                    ApplyRotation(index1, indexAngle);
                    ApplyRotation(index2, indexAngle);
                    ApplyRotation(index3, indexAngle);

                    ApplyRotation(middle1, middleAngle);
                    ApplyRotation(middle2, middleAngle);
                    ApplyRotation(middle3, middleAngle);
                }
                else
                {
                    Debug.LogWarning("Invalid serial data: " + data);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Serial read error: " + e.Message);
            }
        }
    }

    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return Mathf.Clamp((value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin, outMin, outMax);
    }

    void ApplyRotation(Transform bone, float angle)
    {
        if (bone != null)
            bone.localRotation = Quaternion.Euler(angle, 0f, 0f);  // Bend on X-axis
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}
