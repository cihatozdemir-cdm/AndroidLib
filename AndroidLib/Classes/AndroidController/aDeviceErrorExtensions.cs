namespace RegawMOD.Android
{
    /// <summary>
    /// Extensions for Device Exceptions
    /// </summary>
    public static class aDeviceErrorExtensions
    {
        /// <summary>
        /// Throw when any error occcured
        /// </summary>
        /// <param name="deviceError"></param>
        /// <param name="message"></param>
        /// <exception cref="aDeviceException"></exception>
        public static void ThrowOnError(this aDeviceError deviceError)
        {
            if (deviceError != aDeviceError.Success)
                throw new aDeviceException(deviceError);
        }

        /// <summary>
        /// Throw when any error occcured
        /// </summary>
        /// <param name="deviceError"></param>
        /// <param name="message"></param>
        /// <exception cref="aDeviceException"></exception>
        public static void ThrowOnError(this aDeviceError deviceError, string message)
        {
            if (deviceError != aDeviceError.Success)
                throw new aDeviceException(deviceError, message);
        }

        /// <summary>
        /// Check output is give a Error
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsError(this aDeviceError value) => value != aDeviceError.Success;
    }
}
