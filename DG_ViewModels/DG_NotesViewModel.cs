using CommunityToolkit.Mvvm.Input;
using Guerrero_TareaMVVM.DG_Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Guerrero_TareaMVVM.DG_ViewModels
{
    internal class DG_NotesViewModel : IQueryAttributable
    {
        public ObservableCollection<DG_ViewModels.DG_NoteViewModel> AllNotes { get; }
        public ICommand DG_NewCommand { get; }
        public ICommand DG_SelectNoteCommand { get; }
        public DG_NotesViewModel()
        {
            AllNotes = new ObservableCollection<DG_ViewModels.DG_NoteViewModel>(DG_Models.DG_Nota.LoadAll().Select(n => new DG_NoteViewModel(n)));
            DG_NewCommand = new AsyncRelayCommand(DG_NewNoteAsync);
            DG_SelectNoteCommand = new AsyncRelayCommand<DG_ViewModels.DG_NoteViewModel>(DG_SelectNoteAsync);
        }
        private async Task DG_NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(DG_Views.DG_Notas));
        }

        private async Task DG_SelectNoteAsync(DG_ViewModels.DG_NoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(DG_Views.DG_Notas)}?load={note.Identifier}");
        }
        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                DG_NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                DG_NoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.DG_Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                }

                // If note isn't found, it's new; add it.
                else
                    AllNotes.Insert(0, new DG_NoteViewModel(DG_Models.DG_Nota.DG_Load(noteId)));
            }
        }
    }
}
