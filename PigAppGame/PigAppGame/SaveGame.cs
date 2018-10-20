using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PigAppGame
{
    public class SaveGame
    {
        #region Properties
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int WhosTurn { get; set; }
        public int TurnScore { get; set; }
        public bool Rollable { get; set; }
        #endregion



        #region Constuctors
        public SaveGame()
        {

        }
        #endregion
    }
}