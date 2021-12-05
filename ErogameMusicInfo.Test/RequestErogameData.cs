using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErogameMusicInfo.Internet;
using System.Threading.Tasks;
using System.Diagnostics;

/// <summary>
/// �e�X�g�ł�
/// </summary>
namespace ErogameMusicInfo.Test
{
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
            Debug.WriteLine(gameData.ToString());
            Assert.AreEqual(gameData.GameName, "�ޏ��̃Z�C�C�L", "�Q�[�������Ⴄ");
        }
    }
}