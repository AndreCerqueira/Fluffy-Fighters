using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluffyFighters.Args;
using System.Threading;

namespace FluffyFighters.UI.Components.Menus
{
    public class LevelUpMenu : DrawableGameComponent
    {
        // Constants
        private const int MONSTER_BUTTON_OFFSET_X = 80;
        private const int XP_SLIDER_OFFSET_X = 117;
        private const int XP_SLIDER_OFFSET_Y = 160;
        private const int LABEL_OFFSET_X = -125;
        private const int LABEL_OFFSET_Y = 27;
        private const int BUTTON_OFFSET_X = 100;
        private const int BUTTON_OFFSET_Y = 175;
        private const string BUTTON_ASSET_PATH = "sprites/ui/nextButton";
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/LevelUpDilaog";

        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D texture { get; private set; }
        private Rectangle rectangle;
        private Team team;
        private Team opposingTeam;
        private Queue<Monster> monsters;
        private bool _isVisible;
        private bool isVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;

                if (!_isVisible)
                    nextButton?.Block();
            }
        }

        private MonsterButton monsterButton;
        private Point monsterButtonPosition => new(GraphicsDevice.Viewport.Width / 2 - MONSTER_BUTTON_OFFSET_X,
            GraphicsDevice.Viewport.Height / 3 - texture.Height);

        private Slider xpSlider;
        private Point xpSliderPosition => new(GraphicsDevice.Viewport.Width / 2 - XP_SLIDER_OFFSET_X,
            GraphicsDevice.Viewport.Height / 3 - texture.Height + XP_SLIDER_OFFSET_Y);

        private Label levelLabel;
        private Point levelPosition => new(GraphicsDevice.Viewport.Width / 2 - LABEL_OFFSET_X,
            GraphicsDevice.Viewport.Height / 3 - texture.Height + LABEL_OFFSET_Y);

        public Button nextButton;
        private Point buttonNextPosition => new(GraphicsDevice.Viewport.Width / 2 + BUTTON_OFFSET_X,
            GraphicsDevice.Viewport.Height / 3 - texture.Height + BUTTON_OFFSET_Y);

        // Events
        public event EventHandler onMonstersLeveledUp;


        // Constructors
        public LevelUpMenu(Game game, Team team, Team opposingTeam) : base(game)
        {
            this.team = team;
            this.opposingTeam = opposingTeam;
            texture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            rectangle = new(0, 0, texture.Width, texture.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            isVisible = false;

            xpSlider = new Slider(game, 100);
            xpSlider.SetPosition(xpSliderPosition);
            xpSlider.SetColor(Color.MediumSlateBlue);
            xpSlider.SetValue(0, false);

            levelLabel = new Label(game, "1");
            levelLabel.SetScale(2);
            levelLabel.SetPosition(levelPosition);

            nextButton = new Button(game, customAssetPath: BUTTON_ASSET_PATH);
            nextButton.SetPosition(buttonNextPosition);
            nextButton.Block();

            nextButton.OnClicked += SetMonster;
            nextButton.OnClicked += BlockButton;

            xpSlider.onValueMaxed += UpdateMonsterMenu;
            xpSlider.onValueFinishUpdating += UnblockButton;
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            nextButton.Update(gameTime);

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if (!isVisible) return;

            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            monsterButton?.Draw(gameTime);
            xpSlider.Draw(gameTime);
            levelLabel.Draw(gameTime);
            nextButton.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
        }


        public void StartQueue()
        {
            monsters = new Queue<Monster>();
            foreach (var monster in team.GetMonsters())
                monsters.Enqueue(monster);

            SetMonster(this, EventArgs.Empty);
        }


        public int GetXpDroped()
        {
            // get xp droped based on level and opposing team
            return 250;
        }


        int xpDroped = 0;
        public void SetMonster(object sender, EventArgs e)
        {
            if (monsters.Count == 0)
            {
                Close();
                return;
            }

            Monster monster = monsters.Dequeue();

            monsterButton = new MonsterButton(Game, monster, 1);
            monsterButton.SetPosition(monsterButtonPosition);
            monsterButton.Unblock();
            monsterButton.isInteractible = false;

            // Initialize Slider
            xpSlider.SetMaxValue(monster.maxXp);
            xpSlider.SetValue(monster.xp, false);
            xpSlider.SetPosition(xpSliderPosition);

            xpDroped = GetXpDroped();

            UpdateMonsterMenu(sender, e);
        }


        private void UpdateMonsterMenu(object sender, EventArgs e)
        {
            Monster monster = monsterButton.monster;

            levelLabel.SetText(monster.level.ToString());
            levelLabel.SetPosition(levelPosition);

            xpSlider.SetMaxValue(monster.maxXp);
            xpSlider.SetValue(0, false);

            // Set new xp value target of the slider, if bigger than max only set max
            int xpSliderValue = Math.Min(monster.xp + xpDroped, monster.maxXp);
            xpDroped -= xpSliderValue;
            xpSlider.SetValue(xpSliderValue);
            monster.GainXp(xpSliderValue);
        }


        public void Show(object sender, LoseEventArgs e) // won team
        {
            // SetMonster(team.GetSelectedMonster());
            StartQueue();
            isVisible = true;
        }


        public void BlockButton(object sender, EventArgs e) => nextButton.Block();


        public void UnblockButton(object sender, EventArgs e) => nextButton.Unblock(); 


        public void Close()
        {
            isVisible = false;

            onMonstersLeveledUp?.Invoke(this, EventArgs.Empty);
        }
    }
}
