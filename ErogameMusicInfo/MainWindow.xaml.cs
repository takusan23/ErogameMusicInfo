using ErogameMusicInfo.Tool;
using ErogameMusicInfo.ViewModel;
using System.Windows;

namespace ErogameMusicInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitButtonClickEvent();
        }

        /// <summary>
        /// 各ボタンにクリックイベントを設定する
        /// </summary>
        private void InitButtonClickEvent()
        {
            // XamlでつなげたViewModelを取る
            var dataContext = DataContext as MainWindowViewModel;
            CloseButton.MouseDown += (sender, e) => { Application.Current.Shutdown(); };
            // どこ掴んでも動かせるように
            MouseLeftButtonDown += (sender, e) => { DragMove(); };
            OpenErogameScapeButton.Click += (sender, e) =>
            {
                if (dataContext?.ErogameInfo.value != null)
                {
                    var erogameScapeUrl = $"https://erogamescape.dyndns.org/~ap2/ero/toukei_kaiseki/game.php?game={dataContext.ErogameInfo.value.Id}";
                    OpenUrlTool.OpenUrl(erogameScapeUrl);
                }
            };
            OpenWebSiteButton.Click += (sender, e) =>
            {
                if (dataContext?.ErogameInfo.value != null)
                {
                    OpenUrlTool.OpenUrl(dataContext.ErogameInfo.value.Homepage);
                }

            };
            // 最前面切り替えチェックボックス
            var checkBoxClickEvent = () =>
            {
                Topmost = !Topmost;
                SetTopMostCheckbox.IsChecked = Topmost;
            };
            SetTopMostCheckbox.Checked += (sender, e) => checkBoxClickEvent();
            SetTopMostCheckbox.Unchecked += (sender, e) => checkBoxClickEvent();
        }

    }
}
