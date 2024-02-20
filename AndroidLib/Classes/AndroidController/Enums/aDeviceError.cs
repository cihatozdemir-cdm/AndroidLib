namespace RegawMOD.Android
{
    /// <summary>Error Codes</summary>
    public enum aDeviceError
    {
        DeviceHasNoRoot = -8,
        Timeout = -7, // 0xFFFFFFF9
        AdbNotFound = -6,
        NotEnoughData = -4, // 0xFFFFFFFC
        NoDevice = -3, // 0xFFFFFFFD
        UnknownError = -2, // 0xFFFFFFFE
        InvalidArg = -1, // 0xFFFFFFFF
        Success = 0,
    }
    
    
}