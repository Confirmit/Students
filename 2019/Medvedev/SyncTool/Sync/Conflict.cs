﻿using Sync.Wrappers;

namespace Sync
{
    public class Conflict
    {
        public IFileSystemElementInfoWrapper Source { get; }
        public IFileSystemElementInfoWrapper Destination { get; }

        public Conflict(IFileSystemElementInfoWrapper source, IFileSystemElementInfoWrapper destination)
        {
            Source = source;
            Destination = destination;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is Conflict))
                return false;

            var other = (Conflict) obj;

            return Source.Equals(other.Source) && Destination.Equals(other.Destination);
        }

        public override int GetHashCode()
        {
            if (Source is null)
                return Destination.GetHashCode();
            if (Destination is null)
                return Source.GetHashCode();
            return Source.GetHashCode() ^ Destination.GetHashCode();
        }
    }
}