﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mp3lib.Args_Managing;
using mp3lib.Rollback;

namespace mp3lib.Core.Actions
{
	public class Mp3TagChanger
	{
		private readonly List<KeyValuePair<TagType, string>> _changes = new List<KeyValuePair<TagType, string>>();
		private ISaver Backuper { get; }

		private string Mp3RealName { get; }
		private IMp3File Mp3 { get; }
		private readonly string _mask;

		private readonly DataExtracter _dataExtracter;

		public Mp3TagChanger(IMp3File mp3File, string mask, ISaver backuper)
		{
			Mp3 = mp3File;

			Backuper = backuper;
			_mask = mask;

			Mp3RealName = Path.GetFileNameWithoutExtension(mp3File.FilePath);
			_dataExtracter = new DataExtracter(_mask);
		}

		public void ChangeTags()
		{
			var tags = _dataExtracter.GetTags();
			var prefixesQueue = _dataExtracter.FindAllPrefixes(tags);

			var data = _dataExtracter.GetFullDataFromString(prefixesQueue, Mp3RealName, tags);

			ChangeMp3Tags(data);
		}

		private void ChangeMp3Tags(Dictionary<TagType, string> data)
		{
			foreach (var item in data)
			{
				_changes.Add(new KeyValuePair<TagType, string>(item.Key, Mp3[item.Key]));
				switch (item.Key)
				{
					case TagType.Artist:
						Mp3.Artist = item.Value;
						break;
					case TagType.Id:
						Mp3.TrackId = item.Value;
						break;
					case TagType.Title:
						Mp3.Title = item.Value;
						break;
					case TagType.Album:
						Mp3.Album = item.Value;
						break;
					case TagType.Genre:
						Mp3.Genre = item.Value;
						break;
					case TagType.Year:
						Mp3.Year = item.Value;
						break;
					case TagType.Comment:
						Mp3.Comment = item.Value;
						break;
				}
			}
		}
	}
}