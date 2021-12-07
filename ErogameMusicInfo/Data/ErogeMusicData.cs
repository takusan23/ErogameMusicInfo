namespace ErogameMusicInfo.Data
{
    /// <summary>
    /// エロゲソングとエロゲ情報を繋ぐデータクラス
    /// </summary>
    /// <param name="MusicTitle">エロゲソング名</param>
    /// <param name="Id">ErogameScapeのId</param>
    /// <param name="GameTitle">ゲーム名</param>
    /// <param name="Sellday">発売日</param>
    /// <param name="Thumbnail">サムネイルURL</param>
    /// <param name="BrandName">ブランド名</param>
    /// <param name="Homepage">ホームページのリンク</param>
    /// <param name="IsErogame">エロゲならtrue。全年齢ならfalse</param>
    public record ErogeMusicData
     (
         string Id,
         string MusicTitle,
         string GameTitle,
         string Sellday,
         string Thumbnail,
         string BrandName,
         string Homepage,
         bool IsErogame
     );
}
