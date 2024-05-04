using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderApplication
{
    public class JournalEntryModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public List<string> TextNotes { get; set; }

        public JournalEntryModel()
        {
            TextNotes = new List<string>();
            Date = DateTime.Now; // Set the date to the current date and time by default
        }
    }

}
