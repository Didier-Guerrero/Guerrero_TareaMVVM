using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Guerrero_TareaMVVM.DG_ViewModels
{
    internal class DG_NoteViewModel : ObservableObject, IQueryAttributable
    {
        private DG_Models.DG_Nota _note;
        public string DG_Text
        {
            get => _note.DG_Text;
            set
            {
                if (_note.DG_Text != value)
                {
                    _note.DG_Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _note.DG_Date;

        public string Identifier => _note.DG_Filename;

        public ICommand DG_SaveCommand { get; private set; }
        public ICommand DG_DeleteCommand { get; private set; }
        public DG_NoteViewModel()
        {
            _note = new DG_Models.DG_Nota();
            DG_SaveCommand = new AsyncRelayCommand(DG_Save);
            DG_DeleteCommand = new AsyncRelayCommand(DG_Delete);
        }

        public DG_NoteViewModel(DG_Models.DG_Nota note)
        {
            _note = note;
            DG_SaveCommand = new AsyncRelayCommand(DG_Save);
            DG_DeleteCommand = new AsyncRelayCommand(DG_Delete);
        }
        private async Task DG_Save()
        {
            _note.DG_Date = DateTime.Now;
            _note.DG_Save();
            await Shell.Current.GoToAsync($"..?saved={_note.DG_Filename}");
        }

        private async Task DG_Delete()
        {
            _note.DG_Delete();
            await Shell.Current.GoToAsync($"..?deleted={_note.DG_Filename}");
        }
        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = DG_Models.DG_Nota.DG_Load(query["load"].ToString());
                RefreshProperties();
            }
        }
        public void DG_Reload()
        {
            _note = DG_Models.DG_Nota.DG_Load(_note.DG_Filename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(DG_Text));
            OnPropertyChanged(nameof(Date));
        }
    }
}
