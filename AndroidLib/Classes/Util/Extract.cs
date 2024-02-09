/*
 * Extract.cs - Extract Embedded Resources
 * Developed by Dan Wager - 06/22/2011
 */

using System.IO;
using System.Linq;
using System.Reflection;

namespace RegawMOD
{
    internal static class Extract
    {
        static readonly string[] extensions =
            {
                ".exe",
                ".dll",
                ".dylib",
                ".chromium",
                ".patch",
                ".DS_Store",
                ".out",
                ".cfg",
                ".css",
                ".rst",
                ".py",
                ".properties",
                ".conf",
                ".xml",
                ".json"
            };

        /// <summary>
        /// Extracts Multiple Embedded Resources From Calling Assembly
        /// </summary>
        /// <param name="obj">Object To Derive Default Namespace From</param>
        /// <param name="internalFolderPath">Period . Delimited path of embedded resources in assembly</param>
        /// <param name="fullPathOfItems">Exact Names Of Embedded Resources to Extract</param>
        /// <param name="outDirectory">Full Directory of Path For Extracted Resources</param>
        internal static void Resources(object obj, string outDirectory, string internalFolderPath, params string[] fullPathOfItems)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            string defaultNamespace = obj.GetType().Namespace;

            foreach (string item in fullPathOfItems)
                using (Stream s = assembly.GetManifestResourceStream(defaultNamespace + "." + (internalFolderPath == null ? "" : internalFolderPath + ".") + item))
                    using (BinaryReader r = new BinaryReader(s))
                        using (FileStream fs = new FileStream(outDirectory + "\\" + item, FileMode.OpenOrCreate))
                            using (BinaryWriter w = new BinaryWriter(fs))
                                w.Write(r.ReadBytes((int)s.Length));
        }

        /// <summary>
        /// Extracts Multiple Embedded Resources From Calling Assembly
        /// </summary>
        /// <param name="nameSpace">Namespace of calling assembly</param>
        /// <param name="outDirectory">Full Directory of Path For Extracted Resources</param>
        /// <param name="internalFolderPath">Period . Delimited path of embedded resources in assembly</param>
        /// <param name="fullPathOfItems">Exact Names Of Embedded Resources to Extract</param>
        internal static void Resources(string nameSpace, string outDirectory, string internalFolderPath, params string[] fullPathOfItems)
        {
            Assembly assembly = Assembly.GetCallingAssembly();

            foreach (string item in fullPathOfItems)
                using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFolderPath == null ? "" : internalFolderPath + ".") + item))
                    using (BinaryReader r = new BinaryReader(s))
                        using (FileStream fs = new FileStream(outDirectory + "\\" + item, FileMode.OpenOrCreate))
                            using (BinaryWriter w = new BinaryWriter(fs))
                                w.Write(r.ReadBytes((int)s.Length));
        }

        /// <summary>
        /// Extracts All Embedded Resources From Calling Assembly with giving internal path
        /// </summary>
        /// <param name="obj">Object To Derive Default Namespace From</param>
        /// <param name="internalFolderPath">Period . Delimited path of embedded resources in assembly</param>
        /// <param name="outDirectory">Full Directory of Path For Extracted Resources</param>
        internal static void Resources(object obj, string outDirectory, string internalFolderPath)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            string defaultNamespace = obj.GetType().Namespace;
            internalFolderPath = internalFolderPath.Replace("-", "_");

            foreach (var manifest in assembly.GetManifestResourceNames())
            {
                if (manifest.IndexOf(internalFolderPath) != -1)
                {
                    using (Stream s = assembly.GetManifestResourceStream(manifest))
                        using (BinaryReader r = new BinaryReader(s))
                    {
                        var appName = string.Empty;
                        var path = outDirectory + GetFileNameInManifest(manifest, internalFolderPath, out appName).Replace(appName, "");

                        var isExist = Directory.Exists(path);
                        if (!isExist)
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (FileStream fs = new FileStream(string.Concat(path, appName), FileMode.OpenOrCreate))
                            using (BinaryWriter w = new BinaryWriter(fs))
                                w.Write(r.ReadBytes((int)s.Length));
                    }
                }
            }
        }

        /// <summary>
        /// Convert Manifest string to file path
        /// </summary>
        /// <param name="manifest">Input manifest</param>
        /// <param name="replacedfolderPath">Internal path to replace (Namespace)</param>
        /// <param name="appName">Get App Name</param>
        /// <returns>Return full app path</returns>
        private static string GetFileNameInManifest(string manifest, string replacedfolderPath, out string appName)
        {
            //Find extension
            var lastDotIndex = manifest.LastIndexOf('.');
            var canBeExtension = manifest.Substring(lastDotIndex);
            var extension = manifest.Length - lastDotIndex < 4 || extensions.Contains(canBeExtension) ? canBeExtension : string.Empty;

            //create output path
            var filePathIndex = manifest.IndexOf(replacedfolderPath) + replacedfolderPath.Length + 1;
            var path = extension == string.Empty ? manifest : manifest.Replace(extension, "");
            path = path.Substring(filePathIndex).Replace(".", "\\");

            var lastOutput = string.Concat(path, extension);
            //Get App Name
            appName = lastOutput.Substring(lastOutput.LastIndexOf("\\") + 1);

            return lastOutput;
        }
    }
}