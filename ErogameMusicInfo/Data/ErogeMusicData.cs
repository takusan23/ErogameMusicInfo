namespace ErogameMusicInfo.Data
{
    /// <summary>
    /// エロゲソングとエロゲ情報を繋ぐデータクラス
    /// </summary>
    /// <param name="MusicName">エロゲソング名</param>
    /// <param name="Id">ErogameScapeのId</param>
    /// <param name="GameName">ゲーム名</param>
    /// <param name="Sellday">発売日</param>
    /// <param name="Thumbnail">サムネイルURL</param>
    /// <param name="BrandName">ブランド名</param>
    /// <param name="Homepage">ホームページのリンク</param>
    /// <param name="IsErogame">エロゲならtrue。全年齢ならfalse</param>
    public record ErogeMusicData
     (
         string Id,
         string MusicName,
         string GameName,
         string Sellday,
         string Thumbnail,
         string BrandName,
         string Homepage,
         bool IsErogame
     );
}
