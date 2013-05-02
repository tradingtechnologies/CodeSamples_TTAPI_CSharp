using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TTAPI_Sample_Console_OrderRouting
{
    /// <summary>
    /// Utility to confirm that the compiler settings are compatible with the version of TT API installed
    /// </summary>
    class TTAPIArchitectureCheck
    {
        private string errorString = null;

        public string ErrorString
        {
            get
            {
                return errorString;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TTAPIArchitectureCheck()
        {
        }

        /// <summary>
        /// Verify the application build settings match the architecture of the TT API installed
        /// </summary>
        public bool validate()
        {
            try
            {
                System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo((new System.IO.FileInfo("TradingTechnologies.TTAPI.dll")).FullName);

                System.Reflection.Assembly appAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Reflection.Assembly apiAssembly = System.Reflection.Assembly.ReflectionOnlyLoadFrom(fileVersionInfo.FileName);

                System.Reflection.PortableExecutableKinds appKinds, apiKinds;
                System.Reflection.ImageFileMachine appImgFileMachine, apiImgFileMachine;

                appAssembly.ManifestModule.GetPEKind(out appKinds, out appImgFileMachine);
                apiAssembly.ManifestModule.GetPEKind(out apiKinds, out apiImgFileMachine);

                if (!appKinds.HasFlag(apiKinds))
                {
                    errorString = String.Format("WARNING: This application must be compiled as a {0} application to run with a {0} version of TT API.",
                                  (apiKinds.HasFlag(System.Reflection.PortableExecutableKinds.Required32Bit) ? "32Bit" : "64bit"));
                    return false;
                }
                else
                {
                    errorString = "";
                    return true;
                }
            }
            catch (Exception err)
            {
                errorString = String.Format("ERROR: An error occured while attempting to verify the application build settings match the architecture of the TT API installed. {0}", err.Message);
                return false;
            }
        }
    }
}
