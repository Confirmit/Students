﻿using System;
using System.Collections.Generic;
using static TournamentGrid.Tournament;

namespace TournamentGrid
{
    internal class TournamentBracket
    {
        private int _roundIndex;
        private BracketCell[,] _resultBracket;
        private List<Participant> _bracket;
        private int[] _columnNamesMaxLength;
        private KindOfBracket _kindOfBracket;

        public TournamentBracket(int roundIndex, List<Participant> Bracket, KindOfBracket kindOfBracket)
        {
            _roundIndex = roundIndex;
            _bracket = Bracket;
            _kindOfBracket = kindOfBracket;

        }

        public void PrintBracket()
        {
            _columnNamesMaxLength = new int[_roundIndex + 2];

            if (_kindOfBracket == KindOfBracket.Horizontal)
            {
                CreateHorizontal();
                PrintHorizontalBracket();
            }
            else if (_kindOfBracket == KindOfBracket.PlayOff)
            {
                CreatePlayOff();
                PrintPlayOffBracket();
            }
        }

        private void CreateHorizontal()
        {
            HorizontalBracket horizontal = new HorizontalBracket(_bracket, _roundIndex);
            _resultBracket = horizontal.GetHorizontalBracket();
        }

        private void CreatePlayOff()
        {
            List<Participant> left = _bracket.GetRange(0, _bracket.Count / 2);
            List<Participant> right = _bracket.GetRange(_bracket.Count/2+_bracket.Count%2, _bracket.Count / 2);
            PlayOffBracket playOff = new PlayOffBracket(left, right, _roundIndex);
            _resultBracket = playOff.GetPlayOffBracket();

            if (_bracket.Count % 2 == 1)
                _resultBracket[_bracket.Count / 2-1, (_roundIndex + 1) * 2] = new BracketCell(_bracket[_bracket.Count / 2].Name);
        }


        private void PrintHorizontalBracket()
        {
            int[] maxLength=FindMaxNameLength(_bracket.Count, (_roundIndex + 1) * 2);
            for (int i = 0; i < _bracket.Count; i++)
            {

                for (int j = 0; j < (_roundIndex + 1) * 2; j++)
                {
                    string format = "{0, "+maxLength[j]+"}";
                    PrintName(_resultBracket[i, j], format);
                }             
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void PrintPlayOffBracket()
        {
            int[] maxLength = FindMaxNameLength(_bracket.Count/2, (_roundIndex + 1) * 4);
            for (int i = 0; i < _bracket.Count/2 ; i++)
            {
                for (int j = 0; j < (_roundIndex + 1)*2; j++)
                {
                    string format = "{0, " + maxLength[j] + "}";
                    PrintName(_resultBracket[i, j], format);
                }
                Console.Write(" ");
                for (int j = (_roundIndex + 1) * 2; j < (_roundIndex + 1) *4; j++)
                {
                    string format = "{0, " + -maxLength[j] + "}";
                    PrintName(_resultBracket[i, j], format);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        private int[] FindMaxNameLength(int rowsCount, int columnsCount)
        {
            int[] maxLength = new int[columnsCount];

            for (int i = 0; i < rowsCount; i++)
                for (int j = 0; j < columnsCount; j++)
                    if (_resultBracket[i, j] != null && _resultBracket[i, j].Text.Length > maxLength[j])
                        maxLength[j] = _resultBracket[i, j].Text.Length;

            return maxLength;
        }

        private void PrintName(BracketCell participant, string format)
        {
            if (participant!=null)
            {
                Console.ForegroundColor = participant.Color;
                Console.Write(format, participant.Text);
            }
            else
                Console.Write(format,"");
        }
       
    }
}
