using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Framework;
using System.Threading;

namespace Game
{
    public partial class Play : Form
    {
        public Play()
        {
            InitializeComponent();
        }

        private void load()
        {
            Games obj;
            obj = Games.etinstance("F:/testing/Game/Game/map.txt");
            Factory f;
            f = Factory.createInstance();
            GameObjects enemy1 = f.load(pictureBox2,40,FactoryMovement.move.left,ObjectType.objectType.enemy);
            GameObjects enemy2 = f.load(pictureBox3, 40, ObjectType.objectType.enemy);
            GameObjects enemy3 = f.load(pictureBox5, 40, FactoryMovement.move.left, ObjectType.objectType.enemy);
            GameObjects enemy4 = f.load(pictureBox4, 40, FactoryMovement.move.right, ObjectType.objectType.enemy);
            GameObjects player1 = f.load(player,15,FactoryMovement.move.keyBoardMovement,ObjectType.objectType.player);
            obj.addRecord(player1);
            obj.addRecord(enemy1);
            obj.addRecord(enemy2);
            obj.addRecord(enemy3);
            obj.addRecord(enemy4);
            
        }

        //private Movement get_move(FactoryMovement.move state)
        //{
        //    FactoryMovement obj;
        //    obj = FactoryMovement.getInstance();
        //    Movement mave = obj.getsource(state);
        //    return mave;
        //}

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            bool isAlive;
            Games obj;
            obj = Games.etinstance("F:/testing/Game/Game/map.txt");
            obj.updateEnemy();
            CollisionDetection c;
            c = CollisionDetection.getInstance();
            isAlive = c.playerCollision(player,pictureBox2, Reaction.effect.gameOver);
            Alive(isAlive);
            isAlive = c.playerCollision(player, pictureBox3, Reaction.effect.gameOver);
            Alive(isAlive);
            isAlive = c.playerCollision(player, pictureBox4, Reaction.effect.gameOver);
            Alive(isAlive);
            isAlive = c.playerCollision(player, pictureBox5, Reaction.effect.gameOver);
            Alive(isAlive);
        }
        private void Alive(bool isAlive)
        {
            if (isAlive == false)
            {
                GameOver();
            }
        }
        private void win()
        {
            
        }

        //public void load_player()
        //{
        //    Player p;
        //    p = Player.getinstance();
        //    PlayerObject player1 = new PlayerObject(player, 10);
        //    p.addRecord(player1);
        //}

        public void Play_KeyDown(object sender, KeyEventArgs e)
        {
            //Player p;
            //p = Player.getinstance();
            //p.updatePlayer(player,e,sender);
            //check();
            Games g = Games.etinstance();
            g.updatePlayer(e, 20, player);
        }

        private void win_Loop_Tick(object sender, EventArgs e)
        {
            FinishLine.Visible = true;
        }

        private void GameOver()
        {
            foreach (PictureBox item in Controls)
            {
                item.Visible = false;
            }

            this.BackgroundImage = new Bitmap("F:/testing/Game/Game/Resources/over.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            timer1.Enabled = true;

        }
        
        
        private void check()
        {
            foreach (Control item in this.Controls)
            {
                if(item is PictureBox)
                {
                    if (player.Location == item.Location || item.Left == player.Left || item.Top == player.Top )
                    {
                            GameOver();
                    }
                    
                    if (player.Bounds.IntersectsWith(item.Bounds))
                    {
                        GameOver();
                    }

                }
            }
        }

        private void upside()
        {
            int s = player.Top; 
            if (pictureBox2.Left > 1166)
            {
                pictureBox2.Location = new Point(1,37);
                pictureBox2.Left = pictureBox2.Left+10;
            }
            if (pictureBox3.Left > 1166)
            {
                pictureBox3.Location = new Point(1,300);
                pictureBox3.Left = pictureBox3.Left + 10;
            }
            if (pictureBox5.Left > 1166)
            {
                pictureBox5.Location = new Point(1, s);
                pictureBox5.Left = pictureBox5.Left + 30;
            }
            if (pictureBox4.Left < 10)
            {
                pictureBox4.Location = new Point(1166,s);
                pictureBox4.Left = pictureBox4.Left - 30;
            }
            check();
            
        }

        private void Play_Load(object sender, EventArgs e)
        {
            //string map = "F:/ testing / Game / Game / map.txt";
            //Games g = Games.etinstance(map);
            //g.createMap(Play,map);
            load();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Close();
            f.showit();
            //FactoryMovement obj;
            //obj = FactoryMovement.getInstance();
            //Movement m = obj.newMovement(FactoryMovement.move.backward);
            //obj.release(m);
        }

    }
}
