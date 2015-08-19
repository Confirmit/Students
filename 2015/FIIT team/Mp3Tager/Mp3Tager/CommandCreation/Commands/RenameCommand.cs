﻿using FileLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TagLib;

namespace CommandCreation
{
    // todo: for folder
    internal class RenameCommand : Command
    {
        private readonly IEnumerable<IMp3File> _mp3Files;
        private readonly string _mask;
        private readonly BaseUniquePathCreator _pathCreator;

        public RenameCommand(IEnumerable<IMp3File> mp3Files, BaseUniquePathCreator pathCreator, string mask)
        {
            _mp3Files = mp3Files;
            _mask = mask;
            _pathCreator = pathCreator;
        }

        public override string Execute()
        {
            var resultMessage = new StringBuilder();
            foreach (var mp3File in _mp3Files)
            {
                resultMessage.Append(Rename(mp3File));
            }
            return resultMessage.ToString();
        }

        public override void Complete()
        {
            foreach (var mp3File in _mp3Files)
            {
                mp3File.Save();
            }
        }

        private string Rename(IMp3File mp3File)
        {
            if (_mask == String.Empty)
                throw new ArgumentException(_mask);

            var newName = GetNewName(mp3File);
            var directory = Path.GetDirectoryName(mp3File.FullName);
            var uniqueName = _pathCreator.CreateUniqueName(Path.Combine(directory, newName + @".mp3"));

            var resultMessage = mp3File.FullName + " ---> " + uniqueName + "\n";
            mp3File.FullName = uniqueName;

            return resultMessage;
        }

        private string GetNewName(IMp3File mp3File)
        {
            var newName = new StringBuilder(_mask);

            if (_mask.Contains(TagNames.Artist) && !string.IsNullOrEmpty(mp3File.Tags.Artist))                
                newName.Replace(TagNames.Artist, mp3File.Tags.Artist);
            if (_mask.Contains(TagNames.Title) && !string.IsNullOrEmpty(mp3File.Tags.Title))
                newName.Replace(TagNames.Title, mp3File.Tags.Title);
            if (_mask.Contains(TagNames.Genre) && !string.IsNullOrEmpty(mp3File.Tags.Genre))
                newName.Replace(TagNames.Genre, mp3File.Tags.Genre);
            if (_mask.Contains(TagNames.Album) && !string.IsNullOrEmpty(mp3File.Tags.Album))
                newName.Replace(TagNames.Album, mp3File.Tags.Album);
            if (_mask.Contains(TagNames.Track) && !string.IsNullOrEmpty(mp3File.Tags.Track.ToString()))
                newName.Replace(TagNames.Track, mp3File.Tags.Track.ToString());

            return newName.ToString();
        }

        
    }
}