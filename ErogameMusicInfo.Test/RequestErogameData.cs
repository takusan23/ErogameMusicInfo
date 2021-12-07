using ErogameMusicInfo.Internet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;

/// <summary>
/// �e�X�g�ł�
/// </summary>
namespace ErogameMusicInfo.Test
{
    /// <summary>
    /// ErogameScape�̃N�G���@���@�\�������Ɠ����Ă��邩�m�F����e�X�g
    /// </summary>
    [TestClass]
    public class RequestErogameData
    {
        /// <summary>
        /// ���ۂ�POST���N�G�X�g���΂�
        /// </summary>
        [TestMethod]
        public async Task POSTRequest()
        {
            var gameData = await ErogameScape.GetErogameData("�~�ɍ炭��");
            Assert.IsNotNull(gameData, "�Ȗ����Ⴄ���N�G���ԈႦ�Ă܂���");
            Debug.WriteLine(gameData.ToString());
            Assert.AreEqual(gameData.MusicTitle, "�~�ɍ炭��", "�Ȗ����Ⴄ");
            Assert.AreEqual(gameData.GameTitle, "�ޏ��̃Z�C�C�L", "�Q�[�������Ⴄ");
        }
    }
}