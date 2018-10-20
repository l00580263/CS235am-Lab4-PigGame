using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using PigGame;

namespace PigAppGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Fields
        PigLogic game;
        #endregion



        #region Methods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set view
            SetContentView(Resource.Layout.PigsUI);

            // new game
            SetUp();
        }



        void SetUp()
        {
            // new game
            game = new PigLogic();

            // set player names
            game.Player1Name = FindViewById<TextView>(Resource.Id.Player1EditText).Text;
            game.Player2Name = FindViewById<TextView>(Resource.Id.Player2EditText).Text;
            
            // set up events so new names will update the game
            FindViewById<EditText>(Resource.Id.Player1EditText).TextChanged += (sender, args) => { game.Player1Name = FindViewById<EditText>(Resource.Id.Player1EditText).Text; UpdateWhosTurn(); };
            FindViewById<EditText>(Resource.Id.Player2EditText).TextChanged += (sender, args) => { game.Player2Name = FindViewById<EditText>(Resource.Id.Player2EditText).Text; UpdateWhosTurn(); };
           
            // set turn points in UI
            UpdatePointsForTurn();

            // set scores
            UpdatePlayerScore();
           
            // update whos turn ui
            UpdateWhosTurn();

            // set up buttons
            // roll button rolls and updates points for turn UI and update dice image and checks if bad number was rolled
            FindViewById<Button>(Resource.Id.RollButton).Click += (sender, args) => { UpdateDiceImage(game.RollDie()); UpdatePointsForTurn(); BombTurn(); };
            // end turn button changes player and updates who's turn UI and updates points for turn UI and updates player score UI and enables roll button and checks for winner
            FindViewById<Button>(Resource.Id.EndTurnButton).Click += (sender, args) => { game.ChangeTurn(); FlushUI(); EnableRollButton(); WinOrLose(); };
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



        void EnableEndTurnButton()
        {
            FindViewById<Button>(Resource.Id.EndTurnButton).Enabled = true;
        }



        void DisableTurnButton()
        {
            FindViewById<Button>(Resource.Id.EndTurnButton).Enabled = false;
        }



        void WinOrLose()
        {
            // get winner
            string winner = game.CheckForWinner();

            if (winner.Length != 0)
            {
                // if there is a winner, end game
                DisableRollButton();
                DisableTurnButton();
                // display winner
                FindViewById<TextView>(Resource.Id.TurnLabel).Text = winner + " wins";
            }
        }
        #endregion
    }
}