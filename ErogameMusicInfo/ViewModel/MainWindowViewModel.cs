using ErogameMusicInfo.Data;
using ErogameMusicInfo.Database;
using ErogameMusicInfo.Internet;
using ErogameMusicInfo.Tool;
using ErogameMusicInfo.ViewModel.Tool;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ErogameMusicInfo.ViewModel
{
    internal class MainWindowViewModel
    {

        // アプリ名
        public string MusicAppName = "MusicCenter";

        // ViewModelからViewへ変更通知を送るやつ
        public ViewModelValueChanged<string> CurrentPlayingMusicTitle { get; } = new ViewModelValueChanged<string>("");

        // エロゲソングとエロゲ情報のデータクラス
        public ViewModelValueChanged<ErogeMusicData?> ErogameInfo { get; private set; } = new ViewModelValueChanged<ErogeMusicData?>(null);

        // データベース
        public ErogeMusicDB database = new ErogeMusicDB();

        // 同じ曲名の場合は取得しないので
        private string currentPlayingMusicTitle = "";

        public MainWindowViewModel()
        {
            // データベース用意
            database.CreateDB();
            // 曲名監視へ
            InitLoop();
        }

        /// <summary>
        /// 定期的にMusicCenterのウィンドウタイトルを取得して、データを取得する
        /// </summary>
        public async void InitLoop()
        {
            while (true)
            {
                // 曲名を取得
                var windowTitle = WindowTitleGetTool.GetWindowTitleFromAppName(MusicAppName);
                var musicTitleData = MusicCenterTitleParser.ParseWindowTitle(windowTitle);
                // 曲が変わったとき
                if (currentPlayingMusicTitle != musicTitleData.MusicTitle)
                {
                    currentPlayingMusicTitle = musicTitleData.MusicTitle;
                    // データベースにあるならそっちのデータを使う
                    if (database.IsExistsDataFromMusicTitle(musicTitleData.MusicTitle))
                    {
                        ErogameInfo.value = database.GetDataFromMusicTitle(musicTitleData.MusicTitle);
                        Debug.WriteLine("データベースから取得");
                    }
                    else
                    {
                        // インターネットから取得
                        var erogeMusicDataFromInternet = await ErogameScape.GetErogameData(musicTitleData.MusicTitle);
                        // 取れないときの対策
                        if (erogeMusicDataFromInternet != null)
                        {
                            database.InsertData(erogeMusicDataFromInternet);
                            ErogameInfo.value = erogeMusicDataFromInternet;
                            Debug.WriteLine("インターネットから取得");
                        }
                    }
                    Debug.WriteLine(ErogameInfo.value);
                }
                await Task.Delay(1_000);
            }
        }

    }
}
