using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace MonopolyNew
{
    /*Logo:*/
    //
    // ███╗   ███╗ ██████╗ ███╗   ██╗ ██████╗ ██████╗  ██████╗ ██╗  ██╗   ██╗
    // ████╗ ████║██╔═══██╗████╗  ██║██╔═══██╗██╔══██╗██╔═══██╗██║  ╚██╗ ██╔╝
    // ██╔████╔██║██║   ██║██╔██╗ ██║██║   ██║██████╔╝██║   ██║██║   ╚████╔╝ 
    // ██║╚██╔╝██║██║   ██║██║╚██╗██║██║   ██║██╔═══╝ ██║   ██║██║    ╚██╔╝      Version 1.3
    // ██║ ╚═╝ ██║╚██████╔╝██║ ╚████║╚██████╔╝██║     ╚██████╔╝███████╗██║           By Ido Zeira
    // ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚═╝      ╚═════╝ ╚══════╝╚═╝
    //
    public partial class Game : Form
    {
        //-------------------------------------------------Debugging_Options-----------------------------------------------//
        public static string picpath = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("MonopolyNew") + ("MonopolyNew").Length) + "\\Pics";
        public static int TileCount = 11;//Sets The Length of a Tile Row
        private static int SizeBuffer = 15;
        public static Size TileSize = new Size(Screen.PrimaryScreen.Bounds.Height / SizeBuffer, Screen.PrimaryScreen.Bounds.Width / SizeBuffer);//Sets The Size Of The Tiles
        public static Size LTileSize = new Size(TileSize.Height, TileSize.Height);//Sets The Size Of The Large Tiles Leave For Default
        public static int StartingBalance = 500;//Sets Starting Balance
        public static int NumOfPlayers = Convert.ToInt32(Prompt.ShowDialog("How Many Players Are Playing?", "Prompt"));
        // --------------------------------------------------------------------------------------------------------------//
        #region Automatically Generated
        public Game()
        {
            InitializeComponent();
            Start();
        }
        private void Game_Load(object sender, EventArgs e) { }
        #endregion
        #region Global Variables
        public static new Panel Container;
        public static Tile[] Tiles;
        public static Player[] Players;
        public static DiceObject Dice;
        public static int turn;
        public static Panel TextBoard;
        public static TextBox MessagePanel;
        public static Label[] PlayerStats;
        public static Size SlotSize;
        public static string[] TileIdentifier;
        public static string[] TileNames;
        #endregion
        #region Main Functions
        public void Start()
        {
            this.BackgroundImage = Image.FromFile(picpath + @"\Backgrounds\Main.jpg");
            if (NumOfPlayers < 1)
                NumOfPlayers = 1;
            if (NumOfPlayers > 4)
                NumOfPlayers = 4;
            Game.Container.BorderStyle = BorderStyle.None;
            Padding = new Padding(300, 0, 0, 0);
            MessageBox.Show("Welcome To BankRupcy!,\n\nThe Rules Are Simple:\n  1. Don't Spend All Your Money,\n  2. Buy Only Places You Think The Other Players Will Fall On,\n  3. And Don't Rely On Chance!\nEnjoy!\n\nDisclaimer: This Game Looks Like Monopoly, But It Is Different, You Lose When You have No Money.", "Welcome", MessageBoxButtons.OK);
            this.GenBoard();
        }
        public void GenBoard()
        {
            #region Player_Creation
            Players = new Player[NumOfPlayers];
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = new Player(i, (TileCount - 1) * 2);
            }
            #endregion
            #region Tile_Creation
            TileNames = new string[] { "Free Parking", "Kentucky Ave.", "Chance", "Indiana Ave.", "Illinois Ave.", "B. & O. Railroad", "Atlantic Ave.", "Ventnor Ave.", "Water Works", "Marvin Gardens", "Go to Jail", "Pacific Ave.", "North Carolina Ave.", "Chest", "Pennsylvania Ave.", "Short Line Railroad", "Chance", "Park Place", "Luxury Tax", "Boardwalk", "Go", "Mediterranean Ave.", "Chest", "Baltic Ave.", "Income Tax", "Reading Railroad", "Oriental Ave.", "Chance", "Vermont Ave.", "Connecticut Ave.", "Jail", "St. Charles Place", "Electric Company", "States Ave.", "Virginia Ave.", "Pennsylvania Railroad", "St. James Place", "Chest", "Tennessee Ave.", "New York Ave." };
            TileIdentifier = new string[] { "0", "220", "Special", "220", "240", "200", "260", "260", "150", "280", "Special", "300", "300", "Special", " 320", " 200", "Special", "350", "-75", "400", "+200", "60", "Special", "60", "-200", "200", "100", "Special", "100", "120", "0", "140", "150", "140", "160", "200", "180", "Special", "180", "200" };
            Tiles = new Tile[4 * (TileCount - 1)];
            for (int i = 0; i < Tiles.Length; i++)
            {
                #region TileId
                Tiles[i] = Tile.Sort(i);
                #endregion
                #region CornerTiles
                if ((i % (TileCount - 1) == 0))
                {
                    Tiles[i].Panel.Size = new Size(TileSize.Height, TileSize.Height);
                    Container.Size = Tiles[i].Panel.Size;
                    if (i == 1 * (TileCount - 1))
                    {
                        Tiles[i].Panel.Location = new Point(Tiles[i - 1].Panel.Location.X + Tiles[i - 1].Panel.Size.Width, 0);
                    }
                    else if (i == 2 * (TileCount - 1))
                        Tiles[i].Panel.Location = new Point(Tiles[i - 1].Panel.Location.X, Tiles[i - 1].Panel.Location.Y + Tiles[i - 1].Panel.Size.Height);
                    else if (i == 3 * (TileCount - 1))
                        Tiles[i].Panel.Location = new Point(0, Tiles[i - 1].Panel.Location.Y);
                    else if (i == 0)
                        Tiles[i].Panel.Location = new Point(0, 0);
                }
                #endregion
                #region Line 1
                else if (i < 1 * (TileCount - 1))
                {
                    Tiles[i].Panel.Size = TileSize;
                    Tiles[i].Panel.Location = new Point(Tiles[i - 1].Panel.Location.X + Tiles[i - 1].Panel.Size.Width, 0);
                }
                #endregion
                #region Line 2
                else if (i < 2 * (TileCount - 1))
                {
                    Tiles[i].Panel.Size = new Size(TileSize.Height, TileSize.Width);
                    Tiles[i].Panel.Location = new Point(Tiles[i - 1].Panel.Location.X, Tiles[i - 1].Panel.Location.Y + Tiles[i - 1].Panel.Size.Height);
                }
                #endregion
                #region Line 3
                else if (i < 3 * (TileCount - 1))
                {
                    Tiles[i].Panel.Size = TileSize;
                    Tiles[i].Panel.Location = new Point(Tiles[i - 1].Panel.Location.X - Tiles[i].Panel.Size.Width, Tiles[i - 1].Panel.Location.Y);
                }
                #endregion
                #region Line 4
                else if (i < 4 * (TileCount - 1))
                {
                    Tiles[i].Panel.Size = new Size(TileSize.Height, TileSize.Width);
                    Tiles[i].Panel.Location = new Point(0, Tiles[i - 1].Panel.Location.Y - Tiles[i].Panel.Size.Height);
                }
                #endregion
                try
                {
                    Tiles[i].P.Size = Tiles[i].Panel.Size;
                    Tiles[i].P.ImageLocation = picpath + @"\Tiles\" + i.ToString() + ".jpg";
                    Tiles[i].P.SizeMode = PictureBoxSizeMode.StretchImage;
                    Tiles[i].P.BackColor = Color.Transparent;
                    Tiles[i].P.BorderStyle = BorderStyle.None;
                    Tiles[i].Panel.Controls.Add(Tiles[i].P);
                }
                catch { }
                Game.Container.Controls.Add(Tiles[i].Panel);
                if (TileSize.Width >= TileSize.Height)
                    SlotSize = new Size(TileSize.Height / 2, TileSize.Height / 2);
                else
                    SlotSize = new Size(TileSize.Width / 2, TileSize.Width / 2);
                int k = 0;
                int ybound = (int)Math.Ceiling((double)NumOfPlayers / 2);
                for (int y = 0; y < ybound; y++)
                {
                    for (int x = 0; x < NumOfPlayers / ybound; x++)
                    {
                        Tiles[i].Slots[k].Size = new Size(SlotSize.Width, SlotSize.Height);
                        Tiles[i].Slots[k].Location = new Point((x) * (SlotSize.Width), (y) * (SlotSize.Height));
                        if (Players[k].Pos == i)
                        {
                            Tiles[i].Slots[k].ImageLocation = Players[k].PicLocation;
                            if (turn == k)
                                Tiles[i].Slots[k].BackColor = Color.FromArgb(100, Color.Yellow);

                        }
                        Tiles[i].P.Controls.Add(Tiles[i].Slots[k]);
                        k++;

                    }

                }
            }
            #endregion
            #region DiceRoller_Creation
            Dice = new DiceObject();
            Dice.DR1.Size = new Size(TileSize.Width, TileSize.Width);
            Dice.DR2.Size = new Size(TileSize.Width, TileSize.Width);
            Game.Dice.DR1.Location = new System.Drawing.Point(Container.Size.Width / 2 - Dice.DR1.Size.Width, Container.Size.Height / 2 - Dice.DR1.Size.Height / 2);
            Game.Dice.DR2.Location = new System.Drawing.Point(Dice.DR1.Location.X + Dice.DR1.Size.Width, Dice.DR1.Location.Y);
            Game.Container.Controls.Add(Game.Dice.DR1);
            Game.Container.Controls.Add(Game.Dice.DR2);
            #endregion
            #region TextBoard_Creation
            UpdateTextBox();
            System.Windows.Forms.Timer T = new System.Windows.Forms.Timer();
            T.Interval = 10;
            T.Tick += T_Tick;
            #endregion
            #region AdminButton
            Button B = new Button();
            B.Location = Tiles[10].Panel.Location;
            B.BringToFront();
            B.UseVisualStyleBackColor = true;
            B.BackColor = Color.Transparent;
            B.Size = Dice.DR1.Size;
            B.Click += B_Click;
            Container.Controls.Add(B);
            #endregion
        }

        private void B_Click(object sender, EventArgs e)
        {
            Players[turn].Move(int.Parse(Prompt.ShowDialog("Steps To Move:", "Move Player "+turn)));
        }
        private void T_Tick(object sender, EventArgs e)
        {
            UpdateTextBox();
        }
        public static int NextTurn()
        {
            if (turn < NumOfPlayers - 1)
                return turn + 1;
            return 0;
        }
        public void End()
        {
            if (MessageBox.Show("Do you want to exit?", "Ending",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                     == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public string[] TileTypes;
        public static void Chest() { Random R = new Random(); int x = R.Next(1, 21) * 10; MessageBox.Show("You Open The Community Chest,\n You See " + x.ToString() + "$ in it.\nYou Picked Them Up.", "Chest", MessageBoxButtons.OK); Players[turn].Balance += x; }
        public static void Chance() { DialogResult a = (MessageBox.Show("You Recived a Chance To Gamble. Would You Like To Gamble?", "Chance", MessageBoxButtons.YesNo)); if (a == DialogResult.Yes) { int c = int.Parse(Prompt.ShowDialog("How Much Would You Like To Gamble?", "Chance")); int x = new Random().Next(0, 2); if (x == 0) { MessageBox.Show("You Won!", "Chance"); Players[turn].Balance += c; } else { MessageBox.Show("You Lost!", "Chance"); Players[turn].Balance -= c; } } }
        public static void UpdateTextBox()
        {
            Container.Controls.Remove(TextBoard);
            TextBoard = new Panel();
            TextBoard.Location = new Point(Tiles[Tiles.Length - 1].Panel.Location.X + Tiles[Tiles.Length - 1].Panel.Size.Width, Tiles[Tiles.Length - 1].Panel.Location.Y/*+ Tiles[Tiles.Length - 1].Panel.Size.Height*/);
            TextBoard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PlayerStats = new Label[Players.Length];
            for (int i = 0; i < PlayerStats.Length; i++)
            {
                TextBoard.Controls.Remove(PlayerStats[i]);
                PlayerStats[i] = new Label();
                PlayerStats[i].Text = ("Player " + (i + 1).ToString() + " - " + Players[i].Balance.ToString() + "$, Owns " + Players[i].CountOwned().ToString() + " Properties.").ToString();
                PlayerStats[i].AutoSize = true;
                PlayerStats[i].Location = new Point(0, i * PlayerStats[i].Size.Height);
                if (Players[i].Balance == 0)
                    Players[i].Do("Lost()");
                TextBoard.Controls.Add(PlayerStats[i]);
            }
            Container.Controls.Add(TextBoard);
        }
    }
    #endregion
    #region Classes
    #region Tile_Class
    public class Tile
    {
        #region Vars
        public int index;
        public Panel Panel;
        public PictureBox P;
        public PictureBox[] Slots;
        public bool IsOwnable;
        public int Owner;
        public bool IsAvailable() { return (Owner == -1) && (IsOwnable); }
        public string name;
        public string desc;
        public int Price;
        public string action;
        #endregion
        #region Constructors
        public Tile(int index, string name, string desc, string action)
        {
            #region Identifiers_Initialization
            this.action = action;
            this.name = name;
            this.Price = -1;
            this.IsOwnable = false;
            Owner = -1;
            this.index = index;
            #endregion
            #region Slot_Creation
            P = new PictureBox();
            Slots = new PictureBox[Game.NumOfPlayers];
            for (int i = 0; i < Game.NumOfPlayers; i++)
            {
                Slots[i] = new PictureBox();
                Slots[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }
            #endregion
            #region Panel(tile)_Creation
            this.Panel = new Panel();
            this.Panel.BackColor = Color.White;
            this.Panel.BorderStyle = BorderStyle.FixedSingle;
            this.Panel.TabIndex = 0;
            #endregion
        }
        public Tile(int index, string name, string desc, int Price, string action)
        {
            #region Identifiers_Initialization
            this.action = action;
            this.name = name;
            this.desc = desc;
            this.IsOwnable = true;
            this.Price = Price;
            Owner = -1;
            this.index = index;
            #endregion
            #region Slot_Creation
            P = new PictureBox();
            Slots = new PictureBox[Game.NumOfPlayers];
            for (int i = 0; i < Game.NumOfPlayers; i++)
            {
                Slots[i] = new PictureBox();
                Slots[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }
            #endregion
            #region Panel(tile)_Creation
            this.Panel = new Panel();
            this.Panel.BackColor = Color.White;
            this.Panel.BorderStyle = BorderStyle.FixedSingle;
            this.Panel.TabIndex = 0;
            #endregion
        }
        #endregion
        #region Functions
        public static Tile Sort(int index)
        {
            if ((Game.TileIdentifier[index].Contains('+')) || (Game.TileIdentifier[index].Contains('-')) || (Game.TileIdentifier[index] == "Special"))
            {
                if (Game.TileNames[index].Contains("Chest"))
                {
                    return new Tile(index, Game.TileNames[index], "Community Chest, Random Reward Upon Landing.", "Chest()");
                }
                else if (Game.TileNames[index].Contains("Chance"))
                {
                    return new Tile(index, Game.TileNames[index], "Chance Card, Random Reward Upon Landing.", "Chance()");
                }
                else if (Game.TileNames[index].Contains("Jail"))
                {
                    return new Tile(index, Game.TileNames[index], "Go To Jail!", "ToJail()");
                }
                else if (Game.TileNames[index].Contains("Go"))
                {
                    return new Tile(index, Game.TileNames[index], "Fresh Start! Recive 200$!", "Go()");
                }
                else if (Game.TileNames[index].Contains('-'))
                {
                    return new Tile(index, Game.TileNames[index], "Pay A Tax, " + Game.TileNames[index].ToString() + "$.", "Tax(" + Game.TileNames[index].ToString().Substring(1) + ")");
                }
            }
            else if (Game.TileIdentifier[index] == "0")
                return new Tile(index, Game.TileNames[index], "A Tile That Does Nothing.", "None()");
            return new Tile(index, Game.TileNames[index], "This Is A Property That Costs " + Game.TileIdentifier[index] + "$.", int.Parse(Game.TileIdentifier[index]), "Property(" + index.ToString() + ")");
        }
        #endregion
    }
    #endregion
    #region Player_Class
    public class Player
    {
        #region Vars
        public int Pos { get; set; }
        public int Balance { get; set; }
        public string PicLocation { get; set; }
        public bool IsJailed { get; set; }
        public int JailedTurnes { get; set; }
        public int CountOwned()
        {
            int x = 0;
            for (int i = 0; i < Game.Tiles.Length; i++)
            {
                if (Game.Tiles[i].Owner == id)
                    x++;
            }
            return x;

        }
        private System.Windows.Forms.Timer T { get; set; }
        private int StepsToTake { get; set; }
        private int StepsTaken { get; set; }
        public int id;
        private static int[] TakenPicture = new int[Game.NumOfPlayers];
        private static Random R = new Random();
        #endregion
        #region Constructors
        public Player(int id, int Pos)
        {
            this.id = id + 1;
            Balance = Game.StartingBalance;
            #region RandomImagePicker
            int x = R.Next(1, 10);
            int i = 0;
            while (x != -1)
            {
                if (TakenPicture[i] == 0)
                {
                    TakenPicture[i] = x;
                    PicLocation = Game.picpath + @"\Players\(" + x.ToString() + ").png";
                    x = -1;
                }
                else if ((TakenPicture[i] != 0) && (TakenPicture[i] != x))
                {
                    i++;
                }
                else
                {
                    x = R.Next(10);
                    i = 0;
                }
            }
            #endregion
            #region TimerInitialization
            T = new System.Windows.Forms.Timer();
            T.Interval = 100;
            T.Tick += Step;
            #endregion
            IsJailed = false;
            this.Pos = Pos;
        }

        #endregion
        #region Functions
        private void Step(object sender, EventArgs e)
        {
            if (StepsTaken < StepsToTake)
            {
                step();
            }
            else
            {
                T.Stop();
                Pos = CalcTile(Pos, StepsToTake);
                Game.Tiles[Pos].Slots[id - 1].BackColor = Color.Transparent;
                Game.Tiles[Game.Players[Game.NextTurn()].Pos].Slots[Game.Players[Game.NextTurn()].id - 1].BackColor = Color.FromArgb(100, Color.Yellow);
                Game.Dice.IsRolling = false;
                if ((Game.Dice.n1 == 6) && (Game.Dice.n1 == Game.Dice.n2))
                {
                }
                else
                {
                    Do(Game.Tiles[Pos].action);
                    Game.UpdateTextBox();
                    Game.turn = Game.NextTurn();
                }
            }
        }
        private int CalcTile(int Start, int Steps)
        {
            int x = Start;
            for (int i = 0; i < Steps; i++)
            {
                if (x + 1 == Game.Tiles.Length)
                    x = 0;
                else
                    x++;
            }
            return x;
        }
        private void step()
        {
            Game.Tiles[CalcTile(Pos, StepsTaken + 1)].Slots[id - 1].ImageLocation = Game.Tiles[CalcTile(Pos, StepsTaken)].Slots[id - 1].ImageLocation;
            Game.Tiles[CalcTile(Pos, StepsTaken + 1)].Slots[id - 1].BackColor = Color.FromArgb(100, Color.Yellow);
            Game.Tiles[CalcTile(Pos, StepsTaken)].Slots[id - 1].ImageLocation = "";
            Game.Tiles[CalcTile(Pos, StepsTaken)].Slots[id - 1].BackColor = Color.Transparent;
            Game.Tiles[CalcTile(Pos, StepsTaken + 1)].P.Controls.Add(Game.Tiles[CalcTile(Pos, StepsTaken + 1)].Slots[id - 1]);

            StepsTaken++;
        }
        public void Move(int Steps)
        {
            StepsToTake = Steps;
            StepsTaken = 0;
            T.Start();
        }
        public void Do(string ActionCode)
        {
            switch (ActionCode.Substring(0, ActionCode.IndexOf("(")))
            {
                case "None":
                    Game.UpdateTextBox();
                    break;
                case "Chest":
                    Game.Chest();
                    break;
                case "ToJail":
                    MessageBox.Show("You Were Sent To Jail!", "Police", MessageBoxButtons.OK);
                    Move(20);
                    IsJailed = true;
                    JailedTurnes = 0;
                    break;
                case "Go":
                    Balance += 200;
                    Game.UpdateTextBox();
                    break;
                case "Tax":
                    MessageBox.Show(Game.Tiles[Pos].desc, "Tax", MessageBoxButtons.OK);
                    Balance -= int.Parse(ActionCode.Remove(ActionCode.IndexOf(")")).Substring(ActionCode.IndexOf("(") + 1));
                    Game.UpdateTextBox();
                    break;
                case "Property":
                    if (!Game.Tiles[Pos].IsAvailable())
                    {
                        MessageBox.Show("You Stepped In Player " + Game.Tiles[Pos].Owner + "'s Property.\nYou Pay Him " + int.Parse(Game.TileIdentifier[Pos]) / 10 + "$.", "Property", MessageBoxButtons.OK);
                        Balance -= int.Parse(Game.TileIdentifier[Pos]) / 10;
                        Game.Players[Game.Tiles[Pos].Owner - 1].Balance += int.Parse(Game.TileIdentifier[Pos]) / 10;
                        Game.UpdateTextBox();
                    }
                    else if (int.Parse(Game.TileIdentifier[Pos]) <= Balance)
                    {
                        if (MessageBox.Show("Would You Like To Buy \"" + Game.TileNames[Pos] + "\" For " + Game.TileIdentifier[Pos] + "$", "Property Listing", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Balance -= int.Parse(Game.TileIdentifier[Pos]);
                            Game.Tiles[Pos].Owner = id;
                            Game.UpdateTextBox();
                        }
                    }
                    break;
                case "Lost":
                    MessageBox.Show("You Lost.\nThats a Shame.\nThere's no fancy Outro,\nHow Lame...", "The End", MessageBoxButtons.OK);
                    Application.Exit();
                    break;
            }
        }
        #endregion
    }
    #endregion
    #region Dice_Class
    public class DiceObject
    {
        #region Vars
        public PictureBox DR1;
        public PictureBox DR2;
        public Random _r = new Random();
        public System.Windows.Forms.Timer T;
        public int ticks;
        public int n1;
        public int n2;
        public bool IsRolling;
        #endregion
        #region Constructors
        public DiceObject()
        {
            IsRolling = false;
            DR1 = new PictureBox();
            this.DR1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.DR1.Name = "DiceRoller";
            this.DR1.TabIndex = 0;
            this.DR1.Text = "Roll!";
            DR1.ImageLocation = Game.picpath + @"\Dice\Dice_1.png";
            n1 = 1;
            this.DR1.Click += DiceRoller_Click;
            DR2 = new PictureBox();
            this.DR2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.DR2.Name = "DiceRoller";
            this.DR2.TabIndex = 0;
            this.DR2.Text = "Roll!";
            DR2.ImageLocation = Game.picpath + @"\Dice\Dice_1.png";
            n2 = 1;
            this.DR2.Click += DiceRoller_Click;
        }
        #endregion
        #region Functions
        void DiceRoller_Click(object sender, EventArgs e)
        {
            if ((!IsRolling) && (!Game.Players[Game.turn].IsJailed))
            {
                T = new System.Windows.Forms.Timer();
                T.Interval = 4;
                ticks = 0;
                T.Tick += T_Tick;
                IsRolling = true;
                T.Start();
            }
            else if (Game.Players[Game.turn].IsJailed)
            {if (Game.Players[Game.turn].JailedTurnes < 2)
                {
                    MessageBox.Show("You Are In Jail, You Cannot Play Until\n" + (2 - Game.Players[Game.turn].JailedTurnes).ToString() + " More Games.");
                    Game.Players[Game.turn].JailedTurnes++;
                    Game.Players[Game.turn].Move(0);
                }
                else
                    Game.Players[Game.turn].IsJailed = false;


            }
        }
        public void T_Tick(object sender, EventArgs e)
        {
            if (ticks < 15)
            {
                int x = _r.Next(1, 6);
                //System.Windows.Forms.Timer T = new System.Windows.Forms.Timer();
                DR1.ImageLocation = Game.picpath + @"\Dice\Dice_" + x + ".png";
                n1 = x;
                x = _r.Next(1, 6);
                DR2.ImageLocation = Game.picpath + @"\Dice\Dice_" + x + ".png";
                n2 = x;
                ticks++;
            }
            else
            {
                T.Stop();
                Game.Players[Game.turn].Move(n1 + n2);

            }
        }
        #endregion
    }
    #endregion
    #region Prompt_Class
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
    #endregion
    #endregion
}
