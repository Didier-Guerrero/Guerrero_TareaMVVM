using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Guerrero_TareaMVVM.DG_ViewModels
{
    internal class DG_AboutViewModel
    {
        public string DG_Title => AppInfo.Name;
        public string DG_Version => AppInfo.VersionString;
        public string DG_MoreInfoUrl => "https://aka.ms/maui";
        public string DG_Message => "This app is written in XAML and C# with .NET MAUI.";
        public ICommand ShowMoreInfoCommand { get; }

        public DG_AboutViewModel()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync(DG_MoreInfoUrl);
    }
}
