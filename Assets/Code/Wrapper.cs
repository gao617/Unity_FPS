using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Wrapper : MonoBehaviour
{
    
    //Reference to article about calling C++ functions in C#
    //https://levelup.gitconnected.com/integrating-native-c-c-libraries-with-unity-as-plugins-a-step-by-step-guide-17ad70c2e3b4
    public struct AerowandData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] //used to create array in C# class
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
    
    
    private int[] devices; //array of devices, including left and right controllers (0 and 1)
    private Transform[] children; //the array of transform (pos, rot) of all children under this Game object (camera)
    private Quaternion rotationOffset; //the offset used to 
    public bool shieldHit = false; //true when the shield is hit by enemy's bullets
    
    void Start()
    {
        byte numDevices = InitializeDevices();
        Debug.Log("numDevices is" + numDevices);

        devices = new int[]{GetAerowandHand1(), GetAerowandHand2()}; //get left and right controllers
        Debug.Log("devices are" + devices[0] + ", "+ devices[1]);

        rotationOffset = Quaternion.identity; //initialize to be (1,0i,0j,0k) (no rotation)
    }

    // Update is called once per frame
    void Update()
    {
        children = GetComponentsInChildren<Transform>(); //get all the transform of all children, e.g. gun is the first child under camera, so it's children[1] and shield is the 9th children under camera
        
        //get data from devices
        var shieldData = GetAerowandData(devices[0]); 
        var gunData = GetAerowandData(devices[1]);
        
        //reset the rotationOffset if trigger button are pressed on both controllers 
        if (shieldData.trigger > 0 && gunData.trigger > 0)
        {
            //reset rotation & position
            Debug.Log("resetting rotation offset!");
            Quaternion targetQuaternion = new Quaternion( 1, 0, 0, 0);
            Quaternion originalQuaternion = new Quaternion(shieldData.orientation[0], -shieldData.orientation[1], shieldData.orientation[3], -shieldData.orientation[2]);
            rotationOffset = targetQuaternion * Quaternion.Inverse(originalQuaternion);
            return; 
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~For shield~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

         // Debug.Log("  rw:" + shieldData.orientation[0] + " rx:" + shieldData.orientation[1] + " ry:" + shieldData.orientation[2] + " rz:" + shieldData.orientation[3] );
         children[9].localPosition = new Vector3(-shieldData.position[0] * (float)1.5, -shieldData.position[1] * (float)2, shieldData.position[2] + (float)1.5); //scaled to make the position "1 to 1" in the game scene
         children[9].rotation = rotationOffset * (new Quaternion(shieldData.orientation[0], -shieldData.orientation[1],
             shieldData.orientation[3], -shieldData.orientation[2]));

         if (shieldHit == true)
         {
             TriggerHaptics(devices[0], 1);
             shieldHit = false; //reset value
             // Debug.Log(" Shield is hit!!!!!!!!");
         }
       
     
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~For gun~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Debug.Log("x:" + gunData.position[0] + " y:" + gunData.position[1] + " z:" + gunData.position[2] + "  rw:" + gunData.orientation[0] + " rx:" + gunData.orientation[1] + " ry:" + gunData.orientation[2] + " rz:" + gunData.orientation[3] );

        children[1].localPosition = new Vector3(-gunData.position[0] * (float)1.5, -gunData.position[1] * (float)2, gunData.position[2] + (float)1.5);
        children[1].rotation = rotationOffset * (new Quaternion(gunData.orientation[0], -gunData.orientation[1], gunData.orientation[3], -gunData.orientation[2]));
        
        if (gunData.trigger > 0)
        {
            TriggerHaptics(devices[1], 1);
            GunScript gunScript = GameObject.Find("Gun").GetComponent<GunScript>(); //get the script from the gun object
            gunScript.gunTriggered = true; //set trigger to true and shoot bullet
            // Debug.Log("gun triggered!!!!!!!!");
        }
        else
        {
            GunScript gunScript = GameObject.Find("Gun").GetComponent<GunScript>(); //get the script from the gun object
            gunScript.gunTriggered = false; //set trigger to true and shoot bullet
        }

        
        return;
    }
}
