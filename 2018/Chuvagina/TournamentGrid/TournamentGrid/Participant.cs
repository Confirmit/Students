﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    class Participant 
    {
        public string Name { get; private set; }
        
        public Participant(string nameOfParticipant)
        {
            Name = nameOfParticipant;
        }

    }
}
