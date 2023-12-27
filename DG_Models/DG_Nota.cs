using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guerrero_TareaMVVM.DG_Models
{
    internal class DG_Nota
    {
        public string DG_Filename { get; set; }
        public string DG_Text { get; set; }
        public DateTime DG_Date { get; set; }

        public DG_Nota()
        {
            DG_Filename = $"{Path.GetRandomFileName()}.notes.txt";
            DG_Date = DateTime.Now;
            DG_Text = "";
        }

        public void DG_Save() =>
    File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, DG_Filename), DG_Text);

        public void DG_Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, DG_Filename));

        public static DG_Nota DG_Load(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return
                new()
                {
                    DG_Filename = Path.GetFileName(filename),
                    DG_Text = File.ReadAllText(filename),
                    DG_Date = File.GetLastWriteTime(filename)
                };
        }

        public static IEnumerable<DG_Nota> LoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory

                    // Select the file names from the directory
                    .EnumerateFiles(appDataPath, "*.notes.txt")

                    // Each file name is used to load a note
                    .Select(filename => DG_Nota.DG_Load(Path.GetFileName(filename)))

                    // With the final collection of notes, order them by date
                    .OrderByDescending(note => note.DG_Date);
        }
    }
}
