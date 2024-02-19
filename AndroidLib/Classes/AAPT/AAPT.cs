using System;
using System.Collections.Generic;
using System.IO;

namespace RegawMOD.Android
{
    /// <summary>
    /// Wrapper for the AAPT Android binary
    /// </summary>
    public partial class AAPT : IDisposable
    {
        private string resDir;

        /// <summary>
        /// Initializes a new instance of the <c>AAPT</c> class
        /// </summary>
        public AAPT()
        {
            ResourceFolderManager.Register("AAPT");
            this.resDir = ResourceFolderManager.GetRegisteredFolderPath("AAPT");
        }

        /// <summary>
        /// Dumps the specified Apk's badging information
        /// </summary>
        /// <param name="source">Source Apk on local machine</param>
        /// <returns><see cref="AAPT.Badging"/> object containing badging information</returns>
        public Badging DumpBadging(FileInfo source)
        {
            if (!source.Exists)
                throw new FileNotFoundException();

            return new Badging(source, Command.RunProcessReturnOutput(Path.Combine(this.resDir, "aapt.exe"), "dump badging \"" + source.FullName + "\"", true, Command.DEFAULT_TIMEOUT));
        }

        /// <summary>
        /// Call to free up resources after use of <c>AAPT</c>
        /// </summary>
        public void Dispose()
        {
            ResourceFolderManager.Unregister("AAPT");
        }
    }
}
