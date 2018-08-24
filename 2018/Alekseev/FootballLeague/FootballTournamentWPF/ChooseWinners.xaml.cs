﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Football_League;

namespace FootballTournamentWPF
{
    /// <summary>
    /// Interaction logic for ChooseWinners.xaml
    /// </summary>
    public partial class ChooseWinners : Window
    {
        public FullGrid Grid;
        public List<int>[] Choices;
        public ChooseWinners(FullGrid grid)
        {
            Grid = grid;
            Choices = new List<int>[Grid.Grid.Count];
            InitializeComponent();
        }     
    }
}
