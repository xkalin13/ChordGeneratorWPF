using System;
using System.Collections.Generic;
using System.Linq;

namespace ChordGeneratorWPF
{
    public class Note 
    {
        public static char[] publicRootNotes = new char[] { 'C', 'D', 'E', 'F', 'G', 'A', 'B' };
        public static Dictionary<int, char> publicModifiers = new Dictionary<int, char>();
        public static List<Note> publicDetails = new List<Note>();

        public string Name { get; set; }
        public int MidiNumber;
        public int Offset;
        public string[] NoteArray;

        public Note(int midiNumber, string[] noteArray) {
            MidiNumber = midiNumber;
            NoteArray = noteArray;
        }

        public static void SetModifiers() { 
            publicModifiers.Add(-1, 'b');
            publicModifiers.Add(0, ' ');
            publicModifiers.Add(1, '#');
        }
        public static void SetNoteDetails() {
            publicDetails.Add(new Note(60, new string[] { "C ", "B#", "Dbb" }));
            publicDetails.Add(new Note(49, new string[] { "C#", "Db" }));
            publicDetails.Add(new Note(50, new string[] { "D ", "C##", "Ebb" }));
            publicDetails.Add(new Note(51, new string[] { "D#", "Eb", "Fbb" }));

            publicDetails.Add(new Note(52, new string[] { "E ", "Fb", "D##" }));
            publicDetails.Add(new Note(53, new string[] { "F ", "E#", "Gbb" }));
            publicDetails.Add(new Note(54, new string[] { "F#", "Gb" }));
            publicDetails.Add(new Note(55, new string[] { "G ", "F##", "Abb" }));

            publicDetails.Add(new Note(56, new string[] { "G#", "Ab" }));
            publicDetails.Add(new Note(57, new string[] { "A ", "G##", "Bbb" }));
            publicDetails.Add(new Note(58, new string[] { "A#", "Bb", "Cbb" }));
            publicDetails.Add(new Note(59, new string[] { "B ", "Cb", "A##" }));
        }

        public static List<Note> GetScale(string rootNoteName, int[] steps)
        {

            Note rootNote = GetNoteDetails(rootNoteName);//vraci  jednu notu - midi cislo a array stringumena tonu 
            List<Note> notesInOrder = GetAllNotesInOrder(rootNote);// seradi noty ve stupnici ||| Ddur =  D D# E F F# G G# A A# B C C#


            // pole not tonin
            List<Note> detailScale = new List<Note>();//7

            for (int i = 0; i < steps.Length-1; i++)//7
            {
                //vlozi noty ze stupnice do pole ale pouze ty ktere tam patri pomoci krokovani||| Ddur =  D E F F# G A B C#
                detailScale.Add(notesInOrder[steps[i]]);
            }

            List<Note> tmpScale = new List<Note>(); //todo 12 CDEFGAHC

            char currentLetter = rootNoteName[0];

            foreach (Note tmpNoteDetails in detailScale)
            {
                foreach (String currentNote in tmpNoteDetails.NoteArray)
                {
                    if (currentNote[0] == currentLetter)
                    {

                        tmpScale.Add(tmpNoteDetails);
                        tmpScale.ElementAt(tmpScale.Count-1).Name = currentNote;
                    }
                }
                if (currentLetter.Equals('G'))
                {
                    // otoceni abecedy na zacatek
                    currentLetter = 'A';
                }
                else
                {
                    //inkrement abecedne pomoci ascii tabulky  a+1=b b+1=c
                    currentLetter = (Char)(Convert.ToUInt16(currentLetter) + 1);
                }
            }
           
            return tmpScale;
        }

        public static Note GetNoteDetails(string noteStr)//vrati konretni popis noty podle zadane noty
        {
            Console.WriteLine("getNoteDetails");
            foreach (Note currentNote in publicDetails)
            {
                foreach (string noteName in currentNote.NoteArray)
                {
                    if (noteStr == noteName)
                    {
                        return currentNote;
                    }
                }
            }
            return null;
        }

        public static List<Note> GetAllNotesInOrder(Note rootNote)
        {
            Console.WriteLine("getAllNotesInOrder");
            List<Note> tmpNotes = publicDetails;
            int shiftValue = 0;

            foreach (Note note in publicDetails)
            {                
                if (rootNote.MidiNumber == note.MidiNumber)
                {
                    break;
                }
                shiftValue++;
            }

            //rychly shift not

            for (int i = 0; i < shiftValue; i++)
            {
                Note tmpNote = tmpNotes[0];
                tmpNotes.RemoveAt(0);
                tmpNotes.Add(tmpNote);
            }
            return tmpNotes;
        }

        public static List<Note> RecalculateScale(string note,string selectedMode)
        {
            Console.WriteLine("recalculateScale");
            return GetScale(note, Chord.GetAbsoluteSteps(selectedMode));
        }

        public static List<Note> GetCircleOfFifths(Note passedNote)
        {
            // podle referencni noty vytvori kvintovy kruh
            List<Note> circle = new List<Note>();

            circle.Add(passedNote);

            for (int i = 1; i < 12; i++)
            {
                Note tmpLast = circle.Last();
                Note tmpSeventh = GetAllNotesInOrder(tmpLast).ElementAt(7);
                circle.Add(tmpSeventh);
            }

            return circle;
        }

        public static List<Note> RotateCircleOfFifths(List<Note> circleOfFifths, int offset)
        {
            List<Note> newCircle = circleOfFifths;
 
            for (int i = 0; i < offset; i++)
            {
                Note tmpNote = newCircle[0];
                newCircle.RemoveAt(0);
                newCircle.Add(tmpNote);
            }

            return newCircle;

        }
    }
    
}
