using System.Diagnostics;
using System.Linq;

namespace ErogameMusicInfo.Tool
{
    class WindowTitleGetTool
    {
        /// <summary>
        /// アプリ名を指定してウィンドウタイトルを取得する
        /// </summary>
        /// <param name="appName">アプリ名</param>
        /// <returns>ウィンドウタイトル</returns>
        public static string GetWindowTitleFromAppName(string appName) => Process
            .GetProcessesByName(appName)
            .Select(process => process.MainWindowTitle)
            .Where(title => title != "")
            .First();
    }
}
