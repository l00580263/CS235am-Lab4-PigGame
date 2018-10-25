using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using PigGame;
using System.Xml.Serialization;
using System.IO;

namespace PigAppGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Fields
        PigLogic game;
        const string BUNLDE_KEY = "bundlekey";
        #endregion



        #region Methods
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set view
            SetContentView(Resource.Layout.PigsUI);

            // new game
            SetUp(bundle);
        }



        protected override void OnSaveInstanceState(Bundle outState)
        {
            // make savedGame
            SaveGame savedGame = new SaveGame() { Player1Score = game.Player1Score,
                Player2Score = game.Player2Score,
                WhosTurn = game.Turn,
                TurnScore = game.TurnPoints,
                Rollable = IsRollButtonEnabled(),
                Player1Name = game.Player1Name,
                Player2Name = game.Player2Name};
            // make writer
            StringWriter writer = new StringWriter();
            // make serializer
            XmlSerializer cereal = new XmlSerializer(typeof(SaveGame));
            // serialize
            cereal.Serialize(writer, savedGame);
            // get string saved game
            string savedGameString = writer.ToString();
            // give bundle saved game
            outState.PutString(BUNLDE_KEY, savedGameString);

            base.OnSaveInstanceState(outState);
        }



        void SetUp(Bundle bundle)
        {
            if (bundle != null)
            {
                // load game
                // get game string
                string gameString = bundle.GetString(BUNLDE_KEY);
                // make serializer
                XmlSerializer cereal = new XmlSerializer(typeof(SaveGame));
                // make reader
                StringReader reader = new StringReader(gameString);
                // get saved game
                SaveGame savedGame = (SaveGame)cereal.Deserialize(reader);

                // remake game
                game = new PigLogic(savedGame.Player1Score, savedGame.Player2Score, savedGame.TurnScore, savedGame.WhosTurn) { Player1Name = savedGame.Player1Name, Player2Name = savedGame.Player2Name};

                // update ui names
                FindViewById<EditText>(Resource.Id.Player1EditText).Text = savedGame.Player1Name;
                FindViewById<EditText>(Resource.Id.Player2EditText).Text = savedGame.Player2Name;

                // check if turn is bombed
                if (!savedGame.Rollable)
                {
                    DisableRollButton();
                }

                // check if last round was over
                WinOrLose();
            }
            else
            {
                // new game
                game = new PigLogic();
                // set player names
                game.Player1Name = FindViewById<EditText>(Resource.Id.Player1EditText).Text;
                game.Player2Name = FindViewById<EditText>(Resource.Id.Player2EditText).Text;
            }
            
            // set up events so new names will update the game
            FindViewById<EditText>(Resource.Id.Player1EditText).AfterTextChanged += (sender, args) => { game.Player1Name = FindViewById<EditText>(Resource.Id.Player1EditText).Text; UpdateWhosTurn(); };
            FindViewById<EditText>(Resource.Id.Player2EditText).AfterTextChanged += (sender, args) => { game.Player2Name = FindViewById<EditText>(Resource.Id.Player2EditText).Text; UpdateWhosTurn(); };

            // set up UI
            FlushUI();

            // set up buttons
            // roll button rolls and updates points for turn UI and update dice image and checks if bad number was rolled
            FindViewById<Button>(Resource.Id.RollButton).Click += (sender, args) => { UpdateDiceImage(game.RollDie()); UpdatePointsForTurn(); BombTurn(); };
            // end turn button changes player and updates who's turn UI and updates points for turn UI and updates player score UI and enables roll button and checks for winner
            // check for winner before enabling roll button
            FindViewById<Button>(Resource.Id.EndTurnButton).Click += (sender, args) => { game.ChangeTurn(); FlushUI(); if (WinOrLose()) { return; } ; EnableRollButton(); };
            // new game turn button resets the game and updates who's turn UI and updates points for turn UI and enables roll button/ new game button
            FindViewById<Button>(Resource.Id.NewGameButton).Click += (sender, args) => { game.ResetGame(); FlushUI(); EnableRollButton(); EnableEndTurnButton(); };
        }



        void UpdatePointsForTurn()
        {
            // set turn points in UI
            FindViewById<TextView>(Resource.Id.ScoreForTurnLabel).Text = game.TurnPoints.ToString();
        }



        void UpdatePlayerScore()
        {
            // set player scores in UI
            FindViewById<TextView>(Resource.Id.Player1ScoreText).Text = game.Player1Score.ToString();
            FindViewById<TextView>(Resource.Id.Player2ScoreText).Text = game.Player2Score.ToString();
        }



        void UpdateWhosTurn()
        {
            // set turn points in UI
            if (game.CheckForWinner().Length > 0)
            {
                // game is over
                return;
            }

            FindViewById<TextView>(Resource.Id.TurnLabel).Text = game.GetCurrentPlayer() + "'s turn";
        }



        void FlushUI()
        {
            // update most ui
            UpdatePlayerScore();
            UpdatePointsForTurn();
            UpdateWhosTurn();
        }



        void UpdateDiceImage(int roll)
        {
            // get image of die
            string imgName = "die8side" + roll;
            int imgID = Resources.GetIdentifier(imgName, "drawable", PackageName);
            FindViewById<ImageView>(Resource.Id.DieImage).SetImageResource(imgID);
        }



        void BombTurn()
        {
            // end turn if the bad number was rolled
            if (game.TurnPoints == 0)
            {
                DisableRollButton();
            }
        }



        void EnableRollButton()
        {
            FindViewById<Button>(Resource.Id.RollButton).Enabled = true;
        }



        void DisableRollButton()
        {
            FindViewById<Button>(Resource.Id.RollButton).Enabled = false;
        }



        bool IsRollButtonEnabled()
        {
            return FindViewById<Button>(Resource.Id.RollButton).Enabled;
        }



        void EnableEndTurnButton()
        {
            FindViewById<Button>(Resource.Id.EndTurnButton).Enabled = true;
        }



        void DisableTurnButton()
        {
            FindViewById<Button>(Resource.Id.EndTurnButton).Enabled = false;
        }



        bool IsTurnButtonEnabled()
        {
            return FindViewById<Button>(Resource.Id.EndTurnButton).Enabled;
        }



        bool WinOrLose()
        {
            // get winner
            string winner = game.CheckForWinner();

            if (winner.Length != 0 && game.Turn == 1)
            {
                // if there is a winner, end game
                DisableRollButton();
                DisableTurnButton();
                // display winner
                FindViewById<TextView>(Resource.Id.TurnLabel).Text = winner + " wins";

                // game over
                return true;
            }

            // game isn't over
            return false;
        }
        #endregion
    }
}