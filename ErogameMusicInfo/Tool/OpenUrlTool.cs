using System.Diagnostics;

namespace ErogameMusicInfo.Tool
{
    class OpenUrlTool
    {

        /// <summary>
        /// URLを指定して開く
        /// </summary>
        /// <param name="url">開くURL</param>
        public static void OpenUrl(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

    }
}
