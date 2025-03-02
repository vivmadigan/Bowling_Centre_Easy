using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bowling_Centre_Easy.Interfaces;

namespace Bowling_Centre_Easy.MenuOptions
{
    public class MainMenu
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICommand Command { get; set; }
    }
}
