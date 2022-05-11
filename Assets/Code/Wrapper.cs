using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Wrapper : MonoBehaviour
{
    public struct AerowandData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] position; //3
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] orientation; //4
        public byte trigger;
        public byte face;
        public byte gripper;
    }
    // Import and expose native c++ functions
    [DllImport("aerowand", EntryPoint ="InitializeDevices")]
    public static extern byte InitializeDevices();
    [DllImport("aerowand", EntryPoint ="CloseDevices")]
    public static extern void CloseDevices();
    [DllImport("aerowand", EntryPoint ="GetAerowandHand1")]
    public static extern int GetAerowandHand1();
    [DllImport("aerowand", EntryPoint ="GetAerowandHand2")]
    public static extern int GetAerowandHand2();
    [DllImport("aerowand", EntryPoint ="GetAerowandData")]
    public static extern AerowandData GetAerowandData(int deviceId);
    [DllImport("aerowand", EntryPoint ="TriggerHaptics")]
    public static extern void TriggerHaptics(int deviceId,  byte effect);
    [DllImport("aerowand", EntryPoint ="PrintAerowandData")]
    public static extern void PrintAerowandData(AerowandData aerowandData);

    public int[] devices;
    public Transform[] children;
    
    void Start()
    {
        byte numDevices = InitializeDevices();
        Debug.Log("numDevices is" + numDevices);

        devices = new int[]{GetAerowandHand1(), GetAerowandHand2()};
        Debug.Log("devices are" + devices[0] + ", "+ devices[1]);

      
    }

    // Update is called once per frame
    void Update()
    {
        children = GetComponentsInChildren<Transform>();
        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~For shield~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        var shieldData = GetAerowandData(devices[0]);
        Debug.Log("x:" + shieldData.position[0] + " y:" + shieldData.position[1] + " z:" + shieldData.position[2] + "  rw:" + shieldData.orientation[0] + " rx:" + shieldData.orientation[1] + " ry:" + shieldData.orientation[2] + " rz:" + shieldData.orientation[3] );
        // PrintAerowandData(data);
        children[9].localPosition = new Vector3(-shieldData.position[0] * (float)1.5, shieldData.position[1] - (float)0.6, shieldData.position[2] + (float)1.5);
        // GetComponent<Transform>().rotation = new Quaternion(data.orientation[0], data.orientation[1],
        //     data.orientation[2], data.orientation[3]);
        var euler = new Quaternion(shieldData.orientation[0], shieldData.orientation[1], shieldData.orientation[2], shieldData.orientation[3]).eulerAngles;
        children[9].rotation = Quaternion.Euler(-euler[1] + 90, euler[0] + 20, -euler[2]);
        if (shieldData.trigger > 0)
        {
            TriggerHaptics(devices[0], 1);
        }
        if (shieldData.face > 0) {
            //std::cout << i << " face" << std::endl;
        }
        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~For gun~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        var gunData = GetAerowandData(devices[1]);
        Debug.Log("x:" + gunData.position[0] + " y:" + gunData.position[1] + " z:" + gunData.position[2] + "  rw:" + gunData.orientation[0] + " rx:" + gunData.orientation[1] + " ry:" + gunData.orientation[2] + " rz:" + gunData.orientation[3] );
        // PrintAerowandData(data);
        children[2].localPosition = new Vector3(-gunData.position[0] * (float)1.5, gunData.position[1] - (float)0.6, gunData.position[2] + (float)1.5);
        // GetComponent<Transform>().rotation = new Quaternion(data.orientation[0], data.orientation[1],
        //     data.orientation[2], data.orientation[3]);
        euler = new Quaternion(gunData.orientation[0], gunData.orientation[1], gunData.orientation[2], gunData.orientation[3]).eulerAngles;
        children[2].rotation = Quaternion.Euler(-euler[1] + 90, euler[0] + 20, -euler[2]);
        if (gunData.trigger > 0)
        {
            TriggerHaptics(devices[1], 1);
        }
        if (gunData.face > 0) {
            //std::cout << i << " face" << std::endl;
        }
        
        
        return;
    }
}
