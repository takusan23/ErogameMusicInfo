using ErogameMusicInfo.Tool;
using ErogameMusicInfo.ViewModel.Tool;
using System.Timers;

namespace ErogameMusicInfo.ViewModel
{
    internal class MainWindowViewModel
    {

        // アプリ名
        public string MusicAppName = "MusicCenter";

        // ViewModelからViewへ変更通知を送るやつ
        public ViewModelValueChanged<string> CurrentPlayingMusicTitle { get; } = new ViewModelValueChanged<string>("");

        // タイマー
        public Timer UpdateMusicInfoTimer = new Timer();

        public MainWindowViewModel()
        {
            UpdateMusicInfoTimer.Elapsed += (s, e) =>
            {
                var windowTitleName = WindowTitleGetTool.GetWindowTitleFromAppName(MusicAppName);
                var data = MusicCenterTitleParser.ParseWindowTitle(windowTitleName);
                CurrentPlayingMusicTitle.value = $@"
                曲名：{data.MusicTitle}
                アーティスト：{data.MusicArtist}
                アルバム：{data.MusicAlbum}
                ";
            };
            UpdateMusicInfoTimer.Interval = 1000;
            UpdateMusicInfoTimer.Start();
        }

        /// <summary>
        /// アプリ終了時に呼んで
        /// </summary>
        public void Release()
        {
            UpdateMusicInfoTimer.Stop();
        }

    }
}
