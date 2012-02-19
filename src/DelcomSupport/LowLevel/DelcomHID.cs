using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
// extra added for HID API

// Marshal functions

//using System.Collections;
//using System.Data;
//using System.Diagnostics;
//using System.Drawing;
//using System.Runtime.InteropServices;
//using System.Windows.Forms;

namespace DelcomSupport.LowLevel
{
    public class DelcomHID
    {
        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct HidTxPacketStruct
        {
            public Byte MajorCmd;
            public Byte MinorCmd;
            public Byte LSBData;
            public Byte MSBData;
            public Byte HidData0;
            public Byte HidData1;
            public Byte HidData2;
            public Byte HidData3;
            public Byte ExtData0;
            public Byte ExtData1;
            public Byte ExtData2;
            public Byte ExtData3;
            public Byte ExtData4;
            public Byte ExtData5;
            public Byte ExtData6;
            public Byte ExtData7;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HidRxPacketStruct
        {
            public Byte Data0;
            public Byte Data1;
            public Byte Data2;
            public Byte Data3;
            public Byte Data4;
            public Byte Data5;
            public Byte Data6;
            public Byte Data7;
            

            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            //public byte[] Data;      //Data 1 .. 8
        }     


        // Class variables
        private SafeFileHandle myDeviceHandle;
        private Boolean myDeviceDetected;
        private String myDevicePathName;
        private DeviceManagement MyDeviceManagement = new DeviceManagement();
        private Hid MyHid = new Hid();
        private const String MODULE_NAME = "Delcom HID USB CS";
        private UInt32 MatchingDevicesFound = 0;
        private HidTxPacketStruct myTxHidPacket;
        private HidRxPacketStruct myRxHidPacket;
        


        /// <summary>
        /// Initializes the class
        /// </summary>
        public DelcomHID()
        {
            //System.Windows.Forms.MessageBox.Show("Initializing DelcomHID");
            
        }



        /// <summary>
        /// Reads the serial Number
        /// returns zero on sucess, else non-zero erro
        /// </summary>
        public UInt32 GetDeviceInfo(ref UInt32 SerialNumber, ref UInt32 Version, ref UInt32 Date, ref UInt32 Month, ref UInt32 Year )
        {

            try
            {
                Byte[] Ans = new Byte[16];
                Ans[0] = 10;
                if (Hid.HidD_GetFeature(myDeviceHandle, Ans, 8) == false) return (1);
                SerialNumber = Convert.ToUInt32(Ans[0]);
                SerialNumber += Convert.ToUInt32(Ans[1] <<8);
                SerialNumber += Convert.ToUInt32(Ans[2] << 16);
                SerialNumber += Convert.ToUInt32(Ans[3] << 24);
                Version = Convert.ToUInt32(Convert.ToByte(Ans[4]));
                Date = Convert.ToUInt32(Convert.ToByte(Ans[5]));
                Month = Convert.ToUInt32(Convert.ToByte(Ans[6]));
                Year = Convert.ToUInt32(Convert.ToByte(Ans[7]));
                return (0);

            }

            catch (Exception ex)
            {
                DisplayException(MODULE_NAME, ex);
                //throw;
                return (2);
            }
        }


        /// <summary>
        /// Writes the ports values
        /// returns zero on sucess, else non-zero erro
        /// </summary>
        public UInt32 SendCommand( HidTxPacketStruct TxCmd)
        {

            try
            {
                int Length;
                if (TxCmd.MajorCmd == 102) Length = 16;
                else Length = 8;
                if (Hid.HidD_SetFeature(myDeviceHandle, StructureToByteArray(TxCmd), Length) == false) return (1);
                else return (0);
            }

            catch (Exception ex)
            {
                DisplayException(MODULE_NAME, ex);
                //throw;
                return (2);
            }
        }


        /// <summary>
        /// Writes the ports values
        /// returns zero on sucess, else non-zero erro
        /// </summary>
        public UInt32 WritePorts(UInt32 Port0, UInt32 Port1)
        {

            try
            {
                myTxHidPacket.MajorCmd = 101;
                myTxHidPacket.MinorCmd = 10;
                myTxHidPacket.LSBData = Convert.ToByte(Port0);
                myTxHidPacket.MSBData = Convert.ToByte(Port1);
                if (Hid.HidD_SetFeature(myDeviceHandle, StructureToByteArray(myTxHidPacket), 8) == false) return (1);
                else return (0);
            }

            catch (Exception ex)
            {
                DisplayException(MODULE_NAME, ex);
                //throw;
                return (2);
            }
        }


        
        /// <summary>
        /// Reads the currenly value at port zero
        /// returns zero on sucess, else non-zero erro
        /// </summary>
        public UInt32 ReadPort0(ref UInt32 Port0)
        {
           
           try
                {
                Byte[] Ans = new Byte[16];
                Ans[0] = 100;
                if (Hid.HidD_GetFeature(myDeviceHandle, Ans, 8) == false) return (1);
                Port0 = Convert.ToUInt32(Ans[0]);
                return (0);
            
            }

            catch (Exception ex)
            {
                DisplayException(MODULE_NAME, ex);
                //throw;
                return (2);
            }
        }


        /// <summary>
        /// Reads the currenly value at both ports
        /// returns zero on sucess, else non-zero erro
        /// </summary>
        public UInt32 ReadPorts(ref UInt32 Port0, ref UInt32 Port1)
        {

            try
            {
                Byte[] Ans = new Byte[16];
                Ans[0] = 100;
                if (Hid.HidD_GetFeature(myDeviceHandle, Ans, 8) == false) return (1);
                Port0 = Convert.ToUInt32(Ans[0]);
                Port1 = Convert.ToUInt32(Ans[1]);
                return (0);

            }

            catch (Exception ex)
            {
                DisplayException(MODULE_NAME, ex);
                //throw;
                return (2);
            }
        }
    
        /// <summary>
        /// Closed the USB HID devicde
        /// </summary>
        public UInt32 Close()
        {
            try
            {
                if (!myDeviceHandle.IsClosed)
                {
                    myDeviceHandle.Close();
                }
            }
            catch (Exception ex)
            {
                //throw;
                return (2);
            }
            return (0);
        }



        ///  <summary>
        /// Opens the first matching device found
        /// Return zero on success,
        /// otherwise non-zero error
        ///  </summary>
        public UInt32 Open()
        {
            return (OpenNthDevice(1));
        }


        ///  <summary>
        /// Return non-zero if openned
        /// otherwise non-zero error
        ///  </summary>
        public bool IsOpen()
        {
            if (myDeviceHandle==null || myDeviceHandle.IsClosed || myDeviceHandle.IsClosed) return (false);
            else return (true);
        }


        ///  <summary>
        /// Opens the Nth matching device found
        /// Return zero on success,
        /// otherwise non-zero error
        ///  </summary>
        public UInt32 OpenNthDevice(UInt32 NthDevice)
        {
            if (myDeviceHandle != null) Close();    // if the device is already open, then close it first.

            if (!FindTheHid(NthDevice)) return (1);
            myDeviceHandle = FileIO.CreateFile(myDevicePathName, FileIO.GENERIC_READ | FileIO.GENERIC_WRITE, FileIO.FILE_SHARE_READ | FileIO.FILE_SHARE_WRITE, IntPtr.Zero, FileIO.OPEN_EXISTING, 0, 0);
            if (myDeviceHandle.IsInvalid) return (2);
            return (0);  // device found
        }
        

        // Get the device name of the current device
        public string GetDeviceName()
        {
            return (myDevicePathName);
        }

        // Returns a count of the matching device on the current system
        public UInt32 GetDevicesCount()
        {
            FindTheHid(0);
            return (MatchingDevicesFound);
        }
   



        ///  <summary>
        ///  Uses a series of API calls to locate a HID-class device
        ///  by its Vendor ID, Product ID and by the Nth number on the list.
        ///  NthDevice: 0=none, used to determine how many matching device are currently
        ///  installed on the system. 1=Find the first matching device, 2=the second matching device,
        ///  and so on...
        ///  </summary>
        ///          
        ///  <returns>
        ///   True if the device is detected, False if not detected.
        ///  </returns>
        private Boolean FindTheHid(UInt32 NthDevice)
        {
            Boolean deviceFound = false;
            String[] devicePathName = new String[512];      // Apr 20, 2009 - Increase size from 128 to 512.
            SafeFileHandle hidHandle;
            Guid hidGuid = Guid.Empty;
            
            Int32 memberIndex = 0;
            UInt16 myProductID = 0xB080;
            UInt16 myVendorID = 0x0FC5;
            Boolean success = false;
            UInt32 MatchingDevices = 0;

            try
            {
                myDeviceDetected = false;
                Hid.HidD_GetHidGuid(ref hidGuid);       //Retrieves the interface class GUID for the HID class.

                //  Fill an array with the device path names of all attached HIDs.
                deviceFound = MyDeviceManagement.FindDeviceFromGuid(hidGuid, ref devicePathName);

                //  If there is at least one HID, attempt to read the Vendor ID and Product ID
                //  of each device until there is a match or all devices have been examined.
                if (deviceFound)
                {
                    memberIndex = 0;
                    do
                    {
                        //  Open the device
                        hidHandle = FileIO.CreateFile(devicePathName[memberIndex], 0, FileIO.FILE_SHARE_READ | FileIO.FILE_SHARE_WRITE, IntPtr.Zero, FileIO.OPEN_EXISTING, 0, 0);
                                           
                        if (!hidHandle.IsInvalid)
                        {   //  Device openned, now find out if it's the device we want.
                            MyHid.DeviceAttributes.Size = Marshal.SizeOf(MyHid.DeviceAttributes);
                            //  Retrieves a HIDD_ATTRIBUTES structure containing the Vendor ID, 
                            //  Product ID, and Product Version Number for a device.
                            success = Hid.HidD_GetAttributes(hidHandle, ref MyHid.DeviceAttributes);
                            if (success)
                            {
                                //  Find out if the device matches the one we're looking for.
                                if ((MyHid.DeviceAttributes.VendorID == myVendorID) & (MyHid.DeviceAttributes.ProductID == myProductID))
                                {
                                    MatchingDevices++;
                                    myDeviceDetected = true;
                                }

                                if( myDeviceDetected && (MatchingDevices == NthDevice ) )
                                {
                                    // Device found
                                    //  Save the DevicePathName
                                    myDevicePathName = devicePathName[memberIndex];
                                    hidHandle.Close();
                                }
                                else
                                {
                                    //  It's not a match, so close the handle. try the next one
                                    myDeviceDetected = false;
                                    hidHandle.Close();
                                }
                            }
                            else
                            {
                                //  There was a problem in retrieving the information.
                                myDeviceDetected = false;
                                hidHandle.Close();
                            }
                        }

                        //  Keep looking until we find the device or there are no devices left to examine.
                        memberIndex = memberIndex + 1;
                    }
                    while (!((myDeviceDetected | (memberIndex == devicePathName.Length))));
                }

                MatchingDevicesFound = MatchingDevices; // save the device found count

                return myDeviceDetected;
            }
            catch (Exception ex)
            {
                DisplayException(MODULE_NAME, ex);
                throw;
            }
        }




        ///  <summary>
        ///  Provides a central mechanism for exception handling.
        ///  Displays a message box that describes the exception.
        ///  </summary>
        ///  
        ///  <param name="moduleName"> the module where the exception occurred. </param>
        ///  <param name="e"> the exception </param>
        //TODO: Fix
        public static void DisplayException(String moduleName, Exception e)
        {
            String message = null;
            String caption = null;
            //  Create an error message.
            message = "Exception: " + e.Message + "\r\n" + "Module: " + moduleName + "\r\n" + "Method: " + e.TargetSite.Name;
            caption = "Unexpected Exception";
            //System.Windows.Forms.MessageBox.Show(message, caption, System.Windows.Forms.MessageBoxButtons.OK);
            //throw;
        }


        // Converts a Structure to byte[]
        static byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);
            byte[] arr = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);
            return arr;

        }

        // Converts a byte[] to Structure
        static void ByteArrayToStructure(byte[] bytearray, ref object obj)
        {
            int len = Marshal.SizeOf(obj);
            IntPtr i = Marshal.AllocHGlobal(len);
            Marshal.Copy(bytearray, 0, i, len);
            obj = Marshal.PtrToStructure(i, obj.GetType());
            Marshal.FreeHGlobal(i);

        }

    }   // end of the DelcomHID class
}
