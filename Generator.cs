using System;
using System.Collections.Generic;
using System.Linq;

namespace ChordGeneratorWPF
{

    public partial class Generator
    {
        readonly Random rnd = new Random();

        private char selectedRootNote;
        private char selectedModifier;
        private string selectedMode;
        private List<int> selectedProgression;

        public static List<Note> Scale;

        public List<List<Chord>> AlternativeProgressions;
        public List<Chord> ChordsInKey;
        public List<Chord> MainProgression;

        public Generator() { }
        public void RandomizeProgression()
        {
            Console.WriteLine("getRandomizedProgression");
            selectedRootNote = Note.publicRootNotes.ElementAt(GetRandomItem(Note.publicRootNotes.Length));
            selectedModifier = Note.publicModifiers.ElementAt(GetRandomItem(Note.publicModifiers.Count)).Value;
            selectedMode = Chord.PublicModes.ElementAt(GetRandomItem(Chord.PublicModes.Count)).Name;
            selectedProgression = Chord.PublicProgressions.ElementAt(GetRandomItem(Chord.PublicProgressions.Count)).Value.ToList();
        }
        public string[] GetProgressionInfo()
        {
            string mood = "";

            foreach (var item in Chord.PublicProgressions) {
                if (Enumerable.SequenceEqual(item.Value, selectedProgression)) {
                    mood = item.Key;
                }
            }

            return new string[] { 
                selectedRootNote.ToString(),
                selectedModifier.ToString(),
                selectedMode,
                mood };

        }
        public int GetRandomItem(int length)
        {
            return rnd.Next(0, length);
        }
        public void GenerateChordProgression(string rootNote, string modifier, string mode, string progression)
        {
            if(modifier == "")
            {
                modifier = " ";
            }
            SetValues(rootNote[0], modifier[0], mode, progression);

            Scale = Note.RecalculateScale(selectedRootNote.ToString() + selectedModifier, selectedMode);
            ChordsInKey = Chord.RecalculateAllChordsInKey(selectedMode, Scale);
            MainProgression = Chord.RecalculateMainProgression(selectedProgression, ChordsInKey);

            AlternativeProgressions = Chord.RecalculateAlternatives(selectedRootNote.ToString() + selectedModifier, selectedMode, selectedProgression);
        }
        public List<Chord> GetChordsInKey() {
            return ChordsInKey;
        }


        public void SetValues(char rootNote, char modifier, string mode, string progression)
        {
            selectedRootNote = rootNote;
            selectedModifier = modifier;
            selectedMode = mode;
            selectedProgression = Chord.PublicProgressions[progression].ToList();

        }
        public string GetChordName(Player.PlayType playType, int passedChordNumber, int passedChordProgression = 0)
        {
            string tempChord;
            if (playType == Player.PlayType.Main)
            {
                tempChord = MainProgression[passedChordNumber].Name;
            }
            else if (playType == Player.PlayType.Alternative)
            {
                tempChord = AlternativeProgressions[passedChordProgression][passedChordNumber].Name;
            }
            else
            {
                tempChord = ChordsInKey[passedChordNumber].Name;
            }

            return tempChord;
        }
    }
}