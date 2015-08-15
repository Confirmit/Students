﻿using FileLib;
using System.Collections.Generic;

namespace Tests.Fakes
{
    class FakeFileExistenceChecker : BaseFileExistenceChecker
    {
        private readonly HashSet<string> _paths = new HashSet<string>
        {
            @"D:\TestPerformer.mp3", @"D:\TestPerformer (1).mp3"
        };

        protected override bool Exists(string path)
        {
            return _paths.Contains(path);
        }
    }
}
