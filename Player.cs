using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;


namespace ChordGeneratorWPF
{
    public class Player //TODO implement rythm
    {

        public OutputDevice OutDevice = new OutputDevice(0);

        private readonly OutputDeviceDialog outDialog = new OutputDeviceDialog();


        public Generator generator;


        public enum PlayType
        {
            Key,
            Main,
            Alternative
        };

        public Player(Generator generator) {
            this.generator = generator;
        }


        public void PlayChords(PlayType playType, int chordInOrder, int alternativeIndex)
        {

            if (playType == PlayType.Main)
            {
                PlayMainChord(chordInOrder);
            }
            else if (playType == PlayType.Key)
            {
               PlayKeyChord(chordInOrder);
            }
            else{
               PlayAlternativeChord(chordInOrder, alternativeIndex);
            }

        }
        public void PlayChord(Chord chord) {
            
            foreach ( Note note in chord.ChordNotes)
            {

                PlayNote(note.MidiNumber);

            }
            Thread.Sleep(800);
            foreach (Note note in chord.ChordNotes)
            {

                StopNote(note.MidiNumber);

            }
            
        }
        public void PlayMainChord(int passedChordNumber)
        {

            PlayChord(generator.MainProgression[passedChordNumber]);
        }
        public void PlayAlternativeChord(int progressionId,int passedChordNumber)
        {

            PlayChord(generator.AlternativeProgressions[passedChordNumber][progressionId]);
        }
        public void PlayKeyChord(int passedChordNumber)
        {

            PlayChord(generator.ChordsInKey[passedChordNumber]);
        }

        private void PlayNote(int note, int volume = 127)
        {

            OutDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, note, volume));


        }
        private void StopNote(int midi) {
            
            OutDevice.Send(new ChannelMessage(ChannelCommand.NoteOff,0, midi));
        }
        public void PianoControl1_PianoKeyUp(object sender, PianoKeyEventArgs e)
        {

            OutDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, e.NoteID, 0));
        }
        public void PianoControl1_PianoKeyDown(object sender, PianoKeyEventArgs e)
        {

            OutDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, e.NoteID, 127));
        }
    }
}
