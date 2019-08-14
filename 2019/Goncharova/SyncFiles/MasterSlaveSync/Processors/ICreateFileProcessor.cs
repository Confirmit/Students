﻿using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface ICopyFileProcessor
    {
        event EventHandler<ResolverEventArgs> FileCopied;
        void Execute(IFileInfo masterFile, string masterPath, string slavePath);
    }
}