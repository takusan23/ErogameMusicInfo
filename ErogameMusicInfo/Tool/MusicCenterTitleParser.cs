namespace ErogameMusicInfo.Tool
{

    /// <summary>
    /// MusicCenterアプリのタイトルから音楽情報を取得してそれぞれ分ける
    /// </summary>
    /// <param name="MusicTitle">曲名</param>
    /// <param name="MusicArtist">アーティスト名</param>
    /// <param name="MusicAlbum">アルバム名</param>
    record MusicCenterTitleData(string MusicTitle, string MusicArtist, string MusicAlbum);

    class MusicCenterTitleParser
    {

        /// <summary>
        /// MusicCenterのウィンドウタイトルが / 区切りになっているのでそれぞれ分けるメソッド
        /// </summary>
        /// <param name="windowTitle">ウィンドウタイトル</param>
        /// <returns> MusicCenterTitleData </returns>
        public static MusicCenterTitleData ParseWindowTitle(string windowTitle)
        {
            var list = windowTitle.Split(" / ");
            return new MusicCenterTitleData(list[0], list[1], list[2]);
        }

    }
}
