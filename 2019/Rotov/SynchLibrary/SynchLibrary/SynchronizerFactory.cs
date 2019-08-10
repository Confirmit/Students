﻿using System;

namespace SynchLibrary
{
    public static class SynchronizerFactory
    {
        public static ISynchronizer Create(string canRemove)
        {
            switch(canRemove)
            {
                case "true":
                    return new RemoveSynchronizer();
                case "false":
                    return new NoRemoveSynchronizer();
            }
            throw new FormatException("Incorrect remove mode");
        }
    }
}
