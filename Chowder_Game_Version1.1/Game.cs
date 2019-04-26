using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_test
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
            ground = player.Top;
            panini_text.Text = "";
            shnitzel_text.Text = "";
            MungDaal_text.Text = "";
            life_text.Text = Convert.ToString(Lifebar.Value) + " %";

            Cursor.Hide();
            life_player_02_text.Left = -200;
            inventar_player_02.Left = -200;
            Gorgonzola_name.Left = -200;
            chowder_player.Hide();
            chowder_player.SendToBack();
            chowder_player.Left = -200;
            gorgonzola_player.Hide();
            gorgonzola_player.SendToBack();
            gorgonzola_player.Left = -200;



            //Resolution

            x_ax = Screen.PrimaryScreen.Bounds.Width;
            y_ax = Screen.PrimaryScreen.Bounds.Height;
            limit_right = x_ax; //pentru bordare, sa nu iasa de pe ecran



            resolution(x_ax_original, x_ax);

            x_ax_original = numar1;
            x_ax = numar2;

            resolution(y_ax_original, y_ax);
            y_ax_original = numar1;
            y_ax = numar2;

            if (x_ax != 1 || y_ax != 1) if_resolution_differs();
        }

        #region //Initializare

        bool left, right, jump;
        int Force, G = 30;
        int ground;
        int ticket_inventory=0, tick=0;
        bool mung_finish = false;
        int mung_hittime = 0;
        int miscare = 3, panini_hittime = 0, lovit = 50, inchide = 1000; // deplasare inamic
        int miscare_02 = 3, shnitzel_hittime = 0;
        int miscare_03 = 3;
        int dieonce = 0;
        int platform_up = 200, platform_down = 600;     //limite platforma
        int panini_left = 730, panini_right = 1200, shnitzel_left = 210, shnitzel_right = 540;

        PictureBox[] bullet = new PictureBox[10000];
        int bullet_number = 0;
        bool bullet_active = false;
        int direction = 1;
        int[] bullet_direction = new int[10000];

        //player_02
        //Label life_player_02_text = new Label();

        ProgressBar Lifebar_player_02 = new ProgressBar();
        PictureBox player_02 = new PictureBox();
        bool player_02_exist = false;
        bool left_02, right_02, jump_02;
        int Force_02, G_02 = 30;
        int ground_02;

        PictureBox[] bullet_02 = new PictureBox[10000];
        int bullet_number_02 = 0;
        bool bullet_active_02 = false;
        int direction_02 = 0;
        int[] bullet_direction_02 = new int[10000];

        int ticket_inventory_02 = 0;
        PictureBox inventory_01_player_02 = new PictureBox();
        PictureBox inventory_02_player_02 = new PictureBox();
        PictureBox inventory_03_player_02 = new PictureBox();
        PictureBox inventory_04_player_02 = new PictureBox();

        //Resolution
        int x_ax, y_ax;
        int x_ax_original = 1440;
        int y_ax_original = 900;
        int numar1, numar2;
        int limit_right;

#endregion

        void resolution(int x, int y)
        {
            int contor, a, b;
            contor = x;
            a = x;
            b = y;

            while (contor != 1)
            {
                while (a != b) if (a > b) a -= b; else b -= a;
                if (a > 1) { x /= a; y /= a; }
                contor = a;

                a = x; b = y;

            }

            numar1 = x;
            numar2 = y;
        }

        void if_resolution_differs()
        {
            //G = y_ax * G / y_ax_original;
            G = x_ax * G / x_ax_original;

            player.Height = y_ax * player.Height / y_ax_original;
            player.Width = x_ax * player.Width / x_ax_original;
            player.Top = y_ax * player.Top / y_ax_original;
            player.Left = x_ax * player.Left / x_ax_original;
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            ground = player.Top;

            enemy.Height = y_ax * enemy.Height / y_ax_original;     //Panini
            enemy.Width = x_ax * enemy.Width / x_ax_original;
            enemy.Top = y_ax * enemy.Top / y_ax_original;
            enemy.Left = x_ax * enemy.Left / x_ax_original;
            enemy.SizeMode = PictureBoxSizeMode.StretchImage;

            enemy_02.Height = y_ax * enemy_02.Height / y_ax_original;       //Shnitzel
            enemy_02.Width = x_ax * enemy_02.Width / x_ax_original;
            enemy_02.Top = y_ax * enemy_02.Top / y_ax_original;
            enemy_02.Left = x_ax * enemy_02.Left / x_ax_original;
            enemy_02.SizeMode = PictureBoxSizeMode.StretchImage;

            panini_text.Height = y_ax * panini_text.Height / y_ax_original;
            panini_text.Width = x_ax * panini_text.Width / x_ax_original;         
            panini_text.Top = y_ax * panini_text.Top / y_ax_original;
            panini_text.Left = x_ax * panini_text.Left / x_ax_original;
            panini_text.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);
            panini_left = x_ax * panini_left / x_ax_original;
            panini_right = x_ax * panini_right / x_ax_original-50;

            shnitzel_text.Height = y_ax * shnitzel_text.Height / y_ax_original;
            shnitzel_text.Width = x_ax * shnitzel_text.Width / x_ax_original;
            shnitzel_text.Top = y_ax * shnitzel_text.Top / y_ax_original;
            shnitzel_text.Left = x_ax * shnitzel_text.Left / x_ax_original;
            shnitzel_text.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);
            shnitzel_left = x_ax * shnitzel_left / x_ax_original;
            shnitzel_right = x_ax * shnitzel_right / x_ax_original;

            MungDaal.Height = y_ax * MungDaal.Height / y_ax_original;       //MungDaal
            MungDaal.Left = x_ax * MungDaal.Left / x_ax_original;
            MungDaal.Top = y_ax * MungDaal.Top / y_ax_original;
            MungDaal.Left = x_ax * MungDaal.Left / x_ax_original;
            MungDaal.SizeMode = PictureBoxSizeMode.StretchImage;

            MungDaal_text.Height = y_ax * MungDaal_text.Height / y_ax_original;
            MungDaal_text.Width = x_ax * MungDaal_text.Width / x_ax_original;        
            MungDaal_text.Top = y_ax * MungDaal_text.Top / y_ax_original;
            MungDaal_text.Left = x_ax * MungDaal_text.Left / x_ax_original;
            MungDaal_text.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);

            Lifebar.Height = y_ax * Lifebar.Height / y_ax_original;
            Lifebar.Width = x_ax * Lifebar.Width / x_ax_original;
            Lifebar.Top = y_ax * Lifebar.Top / y_ax_original;
            Lifebar.Left = x_ax * Lifebar.Left / x_ax_original;

            life_text.Height = y_ax * life_text.Height / y_ax_original;
            life_text.Width = x_ax * life_text.Width / x_ax_original;
            life_text.Top = y_ax * life_text.Top / y_ax_original;
            life_text.Left = x_ax * life_text.Left / x_ax_original;
            life_text.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);

            Chowder_name.Height = y_ax * Chowder_name.Height / y_ax_original;
            Chowder_name.Width = x_ax * Chowder_name.Width / x_ax_original;
            Chowder_name.Top = y_ax * Chowder_name.Top / y_ax_original;
            Chowder_name.Left = x_ax * Chowder_name.Left / x_ax_original;
            Chowder_name.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);

            inventar.Height = y_ax * inventar.Height / y_ax_original;
            inventar.Width = x_ax * inventar.Width / x_ax_original;
            inventar.Top = y_ax * inventar.Top / y_ax_original;
            inventar.Left = x_ax * inventar.Left / x_ax_original;
            inventar.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);

            Gorgonzola_name.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);
            inventar_player_02.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);
            life_player_02_text.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);
            chowder_player.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);
            gorgonzola_player.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);

            inventory_1.Height = y_ax * inventory_1.Height / y_ax_original;
            inventory_1.Width = x_ax * inventory_1.Width / x_ax_original;
            inventory_1.Top = y_ax * inventory_1.Top / y_ax_original;
            inventory_1.Left = x_ax * inventory_1.Left / x_ax_original;
            inventory_1.SizeMode = PictureBoxSizeMode.StretchImage;

            inventory_2.Height = y_ax * inventory_2.Height / y_ax_original;
            inventory_2.Width = x_ax * inventory_2.Width / x_ax_original;
            inventory_2.Top = y_ax * inventory_2.Top / y_ax_original;
            inventory_2.Left = x_ax * inventory_2.Left / x_ax_original;
            inventory_2.SizeMode = PictureBoxSizeMode.StretchImage;

            inventory_3.Height = y_ax * inventory_3.Height / y_ax_original;
            inventory_3.Width = x_ax * inventory_3.Width / x_ax_original;
            inventory_3.Top = y_ax * inventory_3.Top / y_ax_original;
            inventory_3.Left = x_ax * inventory_3.Left / x_ax_original;
            inventory_3.SizeMode = PictureBoxSizeMode.StretchImage;

            inventory_4.Height = y_ax * inventory_4.Height / y_ax_original;
            inventory_4.Width = x_ax * inventory_4.Width / x_ax_original;
            inventory_4.Top = y_ax * inventory_4.Top / y_ax_original;
            inventory_4.Left = x_ax * inventory_4.Left / x_ax_original;
            inventory_4.SizeMode = PictureBoxSizeMode.StretchImage;

            food_1.Height = y_ax * food_1.Height / y_ax_original;
            food_1.Width = x_ax * food_1.Width / x_ax_original;
            food_1.Top = y_ax * food_1.Top / y_ax_original;
            food_1.Left = x_ax * food_1.Left / x_ax_original;
            food_1.SizeMode = PictureBoxSizeMode.StretchImage;

            food_2.Height = y_ax * food_2.Height / y_ax_original;
            food_2.Width = x_ax * food_2.Width / x_ax_original;
            food_2.Top = y_ax * food_2.Top / y_ax_original;
            food_2.Left = x_ax * food_2.Left / x_ax_original;
            food_2.SizeMode = PictureBoxSizeMode.StretchImage;

            food_3.Height = y_ax * food_3.Height / y_ax_original;
            food_3.Width = x_ax * food_3.Width / x_ax_original;
            food_3.Top = y_ax * food_3.Top / y_ax_original;
            food_3.Left = x_ax * food_3.Left / x_ax_original;
            food_3.SizeMode = PictureBoxSizeMode.StretchImage;

            food_4.Height = y_ax * food_4.Height / y_ax_original;
            food_4.Width = x_ax * food_4.Width / x_ax_original;
            food_4.Top = y_ax * food_4.Top / y_ax_original;
            food_4.Left = x_ax * food_4.Left / x_ax_original;
            food_4.SizeMode = PictureBoxSizeMode.StretchImage;

            solid_ground.Height = y_ax * solid_ground.Height / y_ax_original;
            solid_ground.Width = x_ax * solid_ground.Width / x_ax_original;
            solid_ground.Top = y_ax * solid_ground.Top / y_ax_original;
            solid_ground.Left = x_ax * solid_ground.Left / x_ax_original;
            solid_ground.SizeMode = PictureBoxSizeMode.StretchImage;

            block.Height = y_ax * block.Height / y_ax_original;
            block.Width = x_ax * block.Width / x_ax_original;
            block.Top = y_ax * block.Top / y_ax_original;
            block.Left = x_ax * block.Left / x_ax_original;
            block.SizeMode = PictureBoxSizeMode.StretchImage;

            block_02.Height = y_ax * block_02.Height / y_ax_original;
            block_02.Width = x_ax * block_02.Width / x_ax_original;
            block_02.Top = y_ax * block_02.Top / y_ax_original;
            block_02.Left = x_ax * block_02.Left / x_ax_original;
            block_02.SizeMode = PictureBoxSizeMode.StretchImage;

            block_03.Height = y_ax * block_03.Height / y_ax_original;
            block_03.Width = x_ax * block_03.Width / x_ax_original;
            block_03.Top = y_ax * block_03.Top / y_ax_original;
            block_03.Left = x_ax * block_03.Left / x_ax_original;
            block_03.SizeMode = PictureBoxSizeMode.StretchImage;

            platform.Height = y_ax * platform.Height / y_ax_original;
            platform.Width = x_ax * platform.Width / x_ax_original;
            platform.Top = y_ax * platform.Top / y_ax_original;
            platform.Left = x_ax * platform.Left / x_ax_original;
            platform.SizeMode = PictureBoxSizeMode.StretchImage;
            platform_up = y_ax * platform_up / y_ax_original;
            platform_down = y_ax * platform_down / y_ax_original;

        }

        void if_resolution_differs_add_player_02()
        {
            G_02 = G;

            player_02.Height = y_ax * player_02.Height / y_ax_original;
            player_02.Width = x_ax * player_02.Width / x_ax_original;
            player_02.Top = y_ax * player_02.Top / y_ax_original;
            player_02.Left = x_ax * player_02.Left / x_ax_original;
            player_02.SizeMode = PictureBoxSizeMode.StretchImage;
            ground_02 = solid_ground.Top - player_02.Height;

            Lifebar_player_02.Height = y_ax * Lifebar_player_02.Height / y_ax_original;
            Lifebar_player_02.Width = x_ax * Lifebar_player_02.Width / x_ax_original;
            Lifebar_player_02.Top = y_ax * Lifebar_player_02.Top / y_ax_original;
            Lifebar_player_02.Left = x_ax * Lifebar_player_02.Left / x_ax_original;

            life_player_02_text.Height = y_ax * life_player_02_text.Height / y_ax_original;
            life_player_02_text.Width = x_ax * life_player_02_text.Width / x_ax_original;
            life_player_02_text.Top = y_ax * life_player_02_text.Top / y_ax_original;
            life_player_02_text.Left = x_ax * life_player_02_text.Left / x_ax_original;

            inventory_01_player_02.Height = y_ax * inventory_01_player_02.Height / y_ax_original;
            inventory_01_player_02.Width = x_ax * inventory_01_player_02.Width / x_ax_original;
            inventory_01_player_02.Top = y_ax * inventory_01_player_02.Top / y_ax_original;
            inventory_01_player_02.Left = x_ax * inventory_01_player_02.Left / x_ax_original;
            inventory_01_player_02.SizeMode = PictureBoxSizeMode.StretchImage;

            inventory_02_player_02.Height = y_ax * inventory_02_player_02.Height / y_ax_original;
            inventory_02_player_02.Width = x_ax * inventory_02_player_02.Width / x_ax_original;
            inventory_02_player_02.Top = y_ax * inventory_02_player_02.Top / y_ax_original;
            inventory_02_player_02.Left = x_ax * inventory_02_player_02.Left / x_ax_original;
            inventory_02_player_02.SizeMode = PictureBoxSizeMode.StretchImage;

            inventory_03_player_02.Height = y_ax * inventory_03_player_02.Height / y_ax_original;
            inventory_03_player_02.Width = x_ax * inventory_03_player_02.Width / x_ax_original;
            inventory_03_player_02.Top = y_ax * inventory_03_player_02.Top / y_ax_original;
            inventory_03_player_02.Left = x_ax * inventory_03_player_02.Left / x_ax_original;
            inventory_03_player_02.SizeMode = PictureBoxSizeMode.StretchImage;

            inventory_04_player_02.Height = y_ax * inventory_04_player_02.Height / y_ax_original;
            inventory_04_player_02.Width = x_ax * inventory_04_player_02.Width / x_ax_original;
            inventory_04_player_02.Top = y_ax * inventory_04_player_02.Top / y_ax_original;
            inventory_04_player_02.Left = x_ax * inventory_04_player_02.Left / x_ax_original;
            inventory_04_player_02.SizeMode = PictureBoxSizeMode.StretchImage;

            inventar_player_02.Height = y_ax * inventar_player_02.Height / y_ax_original;
            inventar_player_02.Width = x_ax * inventar_player_02.Width / x_ax_original;
            inventar_player_02.Top = y_ax * inventar_player_02.Top / y_ax_original;
            inventar_player_02.Left = x_ax * inventar_player_02.Left / x_ax_original;
            inventar_player_02.Font = new Font("Microsoft Sans Serif", y_ax * 16 / y_ax_original, FontStyle.Bold);

            Gorgonzola_name.Left = x_ax * Gorgonzola_name.Left / x_ax_original;
            Gorgonzola_name.Height = y_ax * Gorgonzola_name.Height / y_ax_original;
        
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)           // go to MAIN MENU
            {
                menu main_menu = new menu();
                main_menu.Show();
                Cursor.Show();
                this.Close();
            }
            
            if (e.KeyCode == Keys.D)                        // deplasare DREAPTA
            {
                right = true;
                if (jump == false)
                //player.Image = Image.FromFile("chowder_right.png");
                player.Image = Properties.Resources.chowder_right;
                direction = 1;
            }
            
            if (e.KeyCode == Keys.A)                        // deplasare STANGA
            {
                left = true;
                if (jump == false)
                //player.Image = Image.FromFile("chowder_left.png");
                player.Image = Properties.Resources.chowder_left;
                direction = 0;
            }

            if (e.KeyCode == Keys.W)                        // SARITURA
            
            if (jump == false)
            {
                jump = true;
                Force = G;
                //player.Image = Image.FromFile("chowder_jump.png");
                player.Image = Properties.Resources.chowder_jump;
            }

            if (e.KeyCode == Keys.Space)
            {
                bullet_number++;
                bullet[bullet_number] = new PictureBox();
                bullet[bullet_number].Top = player.Top + player.Height / 3;
                bullet[bullet_number].Left = player.Left;
                bullet[bullet_number].Height = 14;
                bullet[bullet_number].Width = 20;
                //bullet[bullet_number].Image = Image.FromFile("taco_bullet.png");
                bullet[bullet_number].Image = Properties.Resources.taco_bullet;
                Controls.Add(bullet[bullet_number]);
                bullet_active = true;

                bullet_direction[bullet_number] = new int();
                bullet_direction[bullet_number] = direction;

                //Resolution changed
                if (x_ax != 1 || y_ax != 1)
                {
                    bullet[bullet_number].Height = y_ax * bullet[bullet_number].Height / y_ax_original;
                    bullet[bullet_number].Width = x_ax * bullet[bullet_number].Width / x_ax_original;
                    bullet[bullet_number].Top = player.Top + player.Height / 3;
                    bullet[bullet_number].Left = player.Left;
                    bullet[bullet_number].SizeMode = PictureBoxSizeMode.StretchImage;
                }

            }


            //player_02
            if (e.KeyCode == Keys.D2)
            {
                if (player_02_exist == false)
                {
                    add_player_02();
                    player_02_exist = true;
                }
                //else                      //Stergere Gorgonzola
                //{
                //    player_02_exist = false;
                //    player_02.Top = 3000;
                //    inventory_01_player_02.Image = Image.FromFile("inventory_slot.jpg");
                //    inventory_02_player_02.Image = Image.FromFile("inventory_slot.jpg");
                //    inventory_03_player_02.Image = Image.FromFile("inventory_slot.jpg");
                //    inventory_04_player_02.Image = Image.FromFile("inventory_slot.jpg");
                //    ticket_inventory_02 = 0;
                //    chowder_player.Hide();
                //    gorgonzola_player.Hide();
                //}
            }
            
            if (player_02_exist == true)
            {
                if (e.KeyCode == Keys.Right)
                {
                    right_02 = true;
                    direction_02 = 1;
                }

                if (e.KeyCode == Keys.Left)
                {
                    left_02 = true;
                    direction_02 = 0;
                }

                if (e.KeyCode == Keys.Up)                    // SARITURA

                    if (jump_02 == false)
                    {
                        jump_02 = true;
                        Force_02 = G_02;
                    }

                if (e.KeyCode == Keys.ControlKey)
                {
                    bullet_number_02++;
                    bullet_02[bullet_number_02] = new PictureBox();
                    bullet_02[bullet_number_02].Top = player_02.Top + player_02.Height / 3;
                    bullet_02[bullet_number_02].Left = player_02.Left;
                    bullet_02[bullet_number_02].Height = 14;
                    bullet_02[bullet_number_02].Width = 20;
                    //bullet_02[bullet_number_02].Image = Image.FromFile("fish_bullet.png");
                    bullet_02[bullet_number_02].Image = Properties.Resources.fish_bullet;
                    Controls.Add(bullet_02[bullet_number_02]);
                    bullet_active_02 = true;

                    bullet_direction_02[bullet_number_02] = new int();
                    bullet_direction_02[bullet_number_02] = direction_02;


                    //Resolution changed
                    if (x_ax != 1 || y_ax != 1)
                    {
                        bullet_02[bullet_number_02].Height = y_ax * bullet_02[bullet_number_02].Height / y_ax_original;
                        bullet_02[bullet_number_02].Width = x_ax * bullet_02[bullet_number_02].Width / x_ax_original;
                        bullet_02[bullet_number_02].Top = player_02.Top + player_02.Height / 3;
                        bullet_02[bullet_number_02].Left = player_02.Left;
                        bullet_02[bullet_number_02].SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
        }

        void add_player_02()
        {
            player_02 = new PictureBox();
            player_02.Height = 64;
            player_02.Width = 41;
            player_02.Top = 376;
            player_02.Left = 1300;
            //player_02.Image = Image.FromFile("gorgonzola_left.png");
            player_02.Image = Properties.Resources.gorgonzola_left;
            Controls.Add(player_02);

            ground_02 = solid_ground.Top - player_02.Height;
            player_02.SendToBack();

            // lifebar
            Lifebar_player_02 = new ProgressBar();
            Lifebar_player_02.Height = 30;
            Lifebar_player_02.Width = 280;
            Lifebar_player_02.Top = 13;
            Lifebar_player_02.Left = 190;
            Lifebar_player_02.Value = 100;
            Controls.Add(Lifebar_player_02);

            //life_player_02_text = new Label();
            life_player_02_text.Top = 12;
            life_player_02_text.Left = 490;
            life_player_02_text.Text = Convert.ToString(Lifebar_player_02.Value) + " %";
            Controls.Add(life_player_02_text);

            //inventory creation
            inventory_01_player_02 = new PictureBox();
            inventory_02_player_02 = new PictureBox();
            inventory_03_player_02 = new PictureBox();
            inventory_04_player_02 = new PictureBox();

            inventory_01_player_02.Top = 13;
            inventory_01_player_02.Left = 13;
            inventory_01_player_02.Height = 32;
            inventory_01_player_02.Width = 32;
            //inventory_01_player_02.Image = Image.FromFile("inventory_slot.jpg");
            inventory_01_player_02.Image = Properties.Resources.inventory_slot;
            Controls.Add(inventory_01_player_02);

            inventory_02_player_02.Top = 13;
            inventory_02_player_02.Left = 51;
            inventory_02_player_02.Height = 32;
            inventory_02_player_02.Width = 32;
            //inventory_02_player_02.Image = Image.FromFile("inventory_slot.jpg");
            inventory_02_player_02.Image = Properties.Resources.inventory_slot;
            Controls.Add(inventory_02_player_02);

            inventory_03_player_02.Top = 13;
            inventory_03_player_02.Left = 89;
            inventory_03_player_02.Height = 32;
            inventory_03_player_02.Width = 32;
            //inventory_03_player_02.Image = Image.FromFile("inventory_slot.jpg");
            inventory_03_player_02.Image = Properties.Resources.inventory_slot;
            Controls.Add(inventory_03_player_02);

            inventory_04_player_02.Top = 13;
            inventory_04_player_02.Left = 127;
            inventory_04_player_02.Height = 32;
            inventory_04_player_02.Width = 32;
            //inventory_04_player_02.Image = Image.FromFile("inventory_slot.jpg");
            inventory_04_player_02.Image = Properties.Resources.inventory_slot;
            Controls.Add(inventory_04_player_02);

            inventar_player_02.Left = 50;
            inventar_player_02.Top = 47;
            Gorgonzola_name.Left = 575;
            chowder_player.Show();
            gorgonzola_player.Show();


            if (x_ax != 1 || y_ax != 1) if_resolution_differs_add_player_02();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //player
            if (e.KeyCode == Keys.D)
            {
                right = false;
                if (jump == false)
                //player.Image = Image.FromFile("chowder_small.png");
                player.Image = Properties.Resources.Chowder_small;
            }
            if (e.KeyCode == Keys.A)
            {
                left = false;
                if (jump == false)
                //player.Image = Image.FromFile("chowder_small.png");
                player.Image = Properties.Resources.Chowder_small;
            }

            //player_02
            if (e.KeyCode == Keys.Right)
            {
                right_02 = false;

            }
            if (e.KeyCode == Keys.Left)
            {
                left_02 = false;
            }
        }

        void right_left_move_player_02()
        {
            if (right_02 == true)      //move right
            {
                player_02.Left += 5;
                //player_02.Image = Image.FromFile("gorgonzola_right.png");
                player_02.Image = Properties.Resources.gorgonzola_right;
            }
            else if (left_02 == true)      //move left
            {
                player_02.Left -= 5;
                //player_02.Image = Image.FromFile("gorgonzola_left.png");
                player_02.Image = Properties.Resources.gorgonzola_left;
            }
        }

        void jumping_player_02()
        {
            if (jump_02 == true)
            {
                player_02.Top -= Force_02;
                Force_02 -= 1;
            }

            if (player_02.Top >= ground_02)
            {
                player_02.Top = ground_02;
                jump_02 = false;
            }

            else player_02.Top += 5;
        }

        void collision_01_player_02(PictureBox block)
        {
            int k = 0;// resetare, daca sari la block.bottom atunci se reseteaza in stanga / dreapta

            if (player_02.Left + player_02.Width - 5 > block.Left) // blocked. bottom - limita
                if (player_02.Left + player_02.Width + 5 < block.Left + block.Width + player_02.Width)
                    if (player_02.Top <= block.Bottom)  // or k = 1;
                        if (player_02.Top > block.Top)
                        {
                            k = 1;  // or  if (player.Top <= block.Bottom+10);
                            //player_02.Image = Image.FromFile("chowder_small.png");
                            jump_02 = false;
                            Force_02 = 0;
                            jump_02 = true;
                        }

            if (player_02.Right > block.Left)  // blocked. left - limita
                if (player_02.Left < block.Right - player_02.Width / 2)
                    if (player_02.Bottom > block.Top + 20)   // +20 pentru platforma (sa nu se reseteze in stanga)
                        if (player_02.Top < block.Bottom)
                            if (k == 0)
                            {
                                right_02 = false;
                                player_02.Left = block.Left - player_02.Width;
                            }

            if (player_02.Left < block.Right) // blocked. right - limita
                if (player_02.Right > block.Left + player_02.Width / 2)
                    if (player_02.Bottom > block.Top + 20)   // +20 pentru platforma (sa nu se reseteze in dreapta)
                        if (player_02.Top < block.Bottom)
                            if (k == 0)
                            {
                                left_02 = false;
                                player_02.Left = block.Right;
                            }
            k = 0;
            //if (!(player.Left + player.Width > block.Left && player.Left + player.Width < block.Left + block.Width + player.Width) && player.Top + player.Height >= block.Top && player.Top < block.Top) jump = true;
        }

        void collision_02_player_02(PictureBox block)
        {
            if (player_02.Left + player_02.Width - 5 > block.Left) // blocked. top - limita
                if (player_02.Left + player_02.Width + 5 < block.Left + block.Width + player_02.Width)
                    if (player_02.Top + player_02.Height >= block.Top)
                        if (player_02.Top < block.Top)
                        {
                            player_02.Top = block.Top - player_02.Height; // block.top = block.location.Y
                            jump_02 = false;
                            // Force = 0;
                        }
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        void bullet_moving_player_02()
        {
            int k = 0;
            for (int i = 1; i <= bullet_number_02; i++)
            {
                if (bullet_direction_02[i] == 1)
                {
                    if (bullet_02[i].Bounds.IntersectsWith(block.Bounds)) bullet_02[i].Left = 3000;
                    else if (bullet_02[i].Bounds.IntersectsWith(block_02.Bounds)) bullet_02[i].Left = 3000;
                    else if (bullet_02[i].Bounds.IntersectsWith(block_03.Bounds)) bullet_02[i].Left = 3000;
                    else if (bullet_02[i].Bounds.IntersectsWith(platform.Bounds)) bullet_02[i].Left = 3000;

                    else if (bullet_02[i].Bounds.IntersectsWith(food_1.Bounds))
                    {
                        if (Convert.ToString(food_1.Tag) != Convert.ToString(0)) food_1.Tag = 0;
                        //food_1.Image = Image.FromFile("apple_slot.png");
                        food_1.Image = Properties.Resources.apple_slot;
                        bullet_02[i].Left = 3000;
                    }
                    else if (bullet_02[i].Bounds.IntersectsWith(food_2.Bounds))
                    {
                        if (Convert.ToString(food_2.Tag) != Convert.ToString(0)) food_2.Tag = 0;
                        //food_2.Image = Image.FromFile("banana_slot.png");
                        food_2.Image = Properties.Resources.banana_slot;
                        bullet_02[i].Left = 3000;
                    }
                    else if (bullet_02[i].Bounds.IntersectsWith(food_3.Bounds))
                    {
                        if (Convert.ToString(food_3.Tag) != Convert.ToString(0)) food_3.Tag = 0;
                        //food_3.Image = Image.FromFile("apple_slot.png");
                        food_3.Image = Properties.Resources.apple_slot;
                        bullet_02[i].Left = 3000;
                    }
                    else if (bullet_02[i].Bounds.IntersectsWith(food_4.Bounds))
                    {
                        if (Convert.ToString(food_4.Tag) != Convert.ToString(0)) food_4.Tag = 0;
                        //food_4.Image = Image.FromFile("banana_slot.png");
                        food_4.Image = Properties.Resources.banana_slot;
                        bullet_02[i].Left = 3000;
                    }

                    else if (bullet_02[i].Bounds.IntersectsWith(player.Bounds))
                    {
                        bullet_02[i].Left = 3000;
                        if (Lifebar.Value > 30)
                        {
                            Lifebar.Value -= 30;
                            life_text.Text = Convert.ToString(Lifebar.Value) + " %";
                        }
                        else
                        {
                            Lifebar.Value = 0;
                            life_text.Text = "0 %";
                            timer1.Stop();
                            MessageBox.Show("Gorgonzola castiga !");
                            Cursor.Show();
                            this.Close();
                            menu main_menu = new menu();
                            main_menu.Show();
                        }
                    }

                    else if (bullet_02[i].Left < 2000) { bullet_02[i].Left += 15; k++; }
                }
                else if (bullet_direction_02[i] == 0)
                {
                    if (bullet_02[i].Bounds.IntersectsWith(block.Bounds)) bullet_02[i].Left = -200;
                    else if (bullet_02[i].Bounds.IntersectsWith(block_02.Bounds)) bullet_02[i].Left = -200;
                    else if (bullet_02[i].Bounds.IntersectsWith(block_03.Bounds)) bullet_02[i].Left = -200;
                    else if (bullet_02[i].Bounds.IntersectsWith(platform.Bounds)) bullet_02[i].Left = -200;

                    else if (bullet_02[i].Bounds.IntersectsWith(food_1.Bounds))
                    {
                        if (Convert.ToString(food_1.Tag) != Convert.ToString(0)) food_1.Tag = 0;
                        //food_1.Image = Image.FromFile("apple_slot.png");
                        food_1.Image = Properties.Resources.apple_slot;
                        bullet_02[i].Left = -200;
                    }
                    else if (bullet_02[i].Bounds.IntersectsWith(food_2.Bounds))
                    {
                        if (Convert.ToString(food_2.Tag) != Convert.ToString(0)) food_2.Tag = 0;
                        //food_2.Image = Image.FromFile("banana_slot.png");
                        food_2.Image = Properties.Resources.banana_slot;
                        bullet_02[i].Left = -200;
                    }
                    else if (bullet_02[i].Bounds.IntersectsWith(food_3.Bounds))
                    {
                        if (Convert.ToString(food_3.Tag) != Convert.ToString(0)) food_3.Tag = 0;
                        //food_3.Image = Image.FromFile("apple_slot.png");
                        food_3.Image = Properties.Resources.apple_slot;
                        bullet_02[i].Left = -200;
                    }
                    else if (bullet_02[i].Bounds.IntersectsWith(food_4.Bounds))
                    {
                        if (Convert.ToString(food_4.Tag) != Convert.ToString(0)) food_4.Tag = 0;
                        //food_4.Image = Image.FromFile("banana_slot.png");
                        food_4.Image = Properties.Resources.banana_slot;
                        bullet_02[i].Left = -200;
                    }

                    else if (bullet_02[i].Bounds.IntersectsWith(player.Bounds))
                    {
                        bullet_02[i].Left = -200;
                        if (Lifebar.Value > 30)
                        {
                            Lifebar.Value -= 30;
                            life_text.Text = Convert.ToString(Lifebar.Value) + "%";
                        }
                        else
                        {
                            Lifebar.Value = 0;
                            life_text.Text = "0 %";
                            timer1.Stop();
                            MessageBox.Show("Gorgonzola castiga !");
                            Cursor.Show();
                            this.Close();
                            menu main_menu = new menu();
                            main_menu.Show();
                        }
                    }

                    else if (bullet_02[i].Left > -100) { bullet_02[i].Left -= 15; k++; }
                }
            }
            if (k == 0) bullet_active_02 = false;
        }

        void collect_player_02(PictureBox item, int count)
        {
            if (player_02.Bounds.IntersectsWith(item.Bounds))
                if (Convert.ToString(item.Tag) == Convert.ToString(0))
                {
                    ticket_inventory_02++;

                    if (ticket_inventory_02 == 1)
                    {
                        if (count == 1) inventory_01_player_02.Image = Properties.Resources.inventory_apple;
                        else inventory_01_player_02.Image = Properties.Resources.inventory_banana;
                        if (Lifebar_player_02.Value + 20 < 100) Lifebar_player_02.Value += 20;         //life increases
                        else Lifebar_player_02.Value = 100;

                    }

                    else if (ticket_inventory_02 == 2)
                    {
                        if (count == 1) inventory_02_player_02.Image = Properties.Resources.inventory_apple;
                        else inventory_02_player_02.Image = Properties.Resources.inventory_banana;
                        if (Lifebar_player_02.Value + 20 < 100) Lifebar_player_02.Value += 20;         //life increases
                        else Lifebar_player_02.Value = 100;
                    }

                    else if (ticket_inventory_02 == 3)
                    {
                        if (count == 1) inventory_03_player_02.Image = Properties.Resources.inventory_apple;
                        else inventory_03_player_02.Image = Properties.Resources.inventory_banana;
                        if (Lifebar_player_02.Value + 20 < 100) Lifebar_player_02.Value += 20;         //life increases
                        else Lifebar_player_02.Value = 100;
                    }

                    else if (ticket_inventory_02 == 4)
                    {
                        if (count == 1) inventory_04_player_02.Image = Properties.Resources.inventory_apple;
                        else inventory_04_player_02.Image = Properties.Resources.inventory_banana;
                        if (Lifebar_player_02.Value + 20 < 100) Lifebar_player_02.Value += 20;         //life increases
                        else Lifebar_player_02.Value = 100;
                    }
                    item.Top = 2000;
                    life_player_02_text.Text = Convert.ToString(Lifebar_player_02.Value + " %");
                }
        }

        void enemy_collision_player_02(PictureBox enemy)     //Panini
        {   
            if (player_02.Bounds.IntersectsWith(enemy.Bounds))
            {
                panini_text.Text = "Saruta-ma, Chowder !!!";
                panini_hittime = 1;

                if (lovit > 100)
                {
                    lovit = 0;
                    if (Lifebar_player_02.Value - 25 > 0)
                    {
                        Lifebar_player_02.Value -= 25;
                        life_player_02_text.Text = Convert.ToString(Lifebar_player_02.Value + " %");
                    }
                    else
                    {
                        Lifebar_player_02.Value = 0;
                        life_player_02_text.Text = Convert.ToString("0 %");
                        jump_02 = true;
                        Force_02 = G;
                        ground_02 += 2000;
                        inchide = 490;
                        dieonce++;
                        if (dieonce == 1) MessageBox.Show("Gorgonzola a pierdut");
                        this.Close();
                        Cursor.Show();
                        menu main_menu = new menu();
                        main_menu.Show();

                    }
                }
            }
            if (panini_hittime != 0) panini_hittime++;
            if (panini_hittime > 200)
            {
                panini_hittime = 0;
                panini_text.Text = "";
            }

            if (lovit < 500) lovit++;
            if (inchide <= lovit)
            {
                this.Close();
                Cursor.Show();
                menu main_menu = new menu();
                main_menu.Show();
            }
        }

        void enemy_02_collision_player_02(PictureBox enemy)      //Shnitzel
        {
            if (player_02.Bounds.IntersectsWith(enemy.Bounds))
            {
                shnitzel_text.Text = "RADA RADA RADA !!!";
                shnitzel_hittime = 1;

                if (lovit > 100)
                {
                    lovit = 0;
                    if (Lifebar_player_02.Value - 25 > 0)
                    {
                        Lifebar_player_02.Value -= 25;
                        life_player_02_text.Text = Convert.ToString(Lifebar_player_02.Value + " %");
                    }
                    else
                    {
                        Lifebar_player_02.Value = 0;
                        life_player_02_text.Text = Convert.ToString("0 %");
                        jump_02 = true;
                        Force_02 = G;
                        ground += 2000;
                        inchide = 490;
                        dieonce++;
                        if (dieonce == 1) MessageBox.Show("Gorgonzola a pierdut");
                        this.Close();
                        Cursor.Show();
                        menu main_menu = new menu();
                        main_menu.Show();
                    }
                }
            }
            if (shnitzel_hittime != 0) shnitzel_hittime++;
            if (shnitzel_hittime > 200)
            {
                shnitzel_hittime = 0;
                shnitzel_text.Text = "";
            }

            if (lovit < 500) lovit++;
            if (inchide <= lovit)
            {
                this.Close();
                Cursor.Show();
                menu main_menu = new menu();
                main_menu.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)        //operatii pe FPS
        {
            right_left_move();
            //playername_move(player, chowder_player);
            if (bullet_active == true) bullet_moving();
            limits(player);

            collision_01(block);
            collision_01(block_02);
            collision_01(block_03);

            jumping();

            collision_02(block);
            collision_02(block_02);
            collision_02(block_03);

            collect(food_1, 1);     // 1 - mar ; 2 - banana
            collect(food_2, 2);
            collect(food_3, 1);
            collect(food_4, 2);

            MungDaal_finish();
            enemy_move(enemy, panini_left, panini_right);
            enemy_move_02(enemy_02, shnitzel_left, shnitzel_right);

            platform_move(platform, platform_up, platform_down);


            //player_02
            if (player_02_exist == true)
            {
                right_left_move_player_02();
                //playername_move(player_02, gorgonzola_player);
                if (bullet_active_02 == true) bullet_moving_player_02();
                limits(player_02);

                collision_01_player_02(block);
                collision_01_player_02(block_02);
                collision_01_player_02(block_03);

                jumping_player_02();

                collision_02_player_02(block);
                collision_02_player_02(block_02);
                collision_02_player_02(block_03);

                //player_02 platform collision
                collision_01_player_02(platform);
                collision_02_player_02(platform);

                collect_player_02(food_1, 1);
                collect_player_02(food_2, 2);
                collect_player_02(food_3, 1);
                collect_player_02(food_4, 2);

                enemy_collision_player_02(enemy);
                enemy_02_collision_player_02(enemy_02);


            }
        }

        void right_left_move()
        {
            if (right == true)      //move right
            {
                player.Left += 5;
                if (jump == false)
                    player.Image = Properties.Resources.chowder_right;
                    //player.Image = Image.FromFile("chowder_right.png");
            }
            else if (left == true)      //move left
            {
                player.Left -= 5;
                if (jump == false)
                    player.Image = Properties.Resources.chowder_left;
                    //player.Image = Image.FromFile("chowder_left.png");
            }
        }

        void playername_move(PictureBox player, Label name)
        {
            name.Left = player.Left - 10;
            name.Top = player.Top - 20;
        }

        void bullet_moving()
        {
            int k = 0;
            for (int i = 1; i <= bullet_number; i++)
            {
                if (bullet_direction[i] == 1)
                {
                    if (bullet[i].Bounds.IntersectsWith(block.Bounds)) bullet[i].Left = 3000;
                    else if (bullet[i].Bounds.IntersectsWith(block_02.Bounds)) bullet[i].Left = 3000;
                    else if (bullet[i].Bounds.IntersectsWith(block_03.Bounds)) bullet[i].Left = 3000;
                    else if (bullet[i].Bounds.IntersectsWith(platform.Bounds)) bullet[i].Left = 3000;

                    else if (bullet[i].Bounds.IntersectsWith(food_1.Bounds))
                    {
                        if (Convert.ToString(food_1.Tag) != Convert.ToString(0)) food_1.Tag = 0;
                        //food_1.Image = Image.FromFile("apple_slot.png");
                        food_1.Image = Properties.Resources.apple_slot;
                        bullet[i].Left = 3000;
                    }
                    else if (bullet[i].Bounds.IntersectsWith(food_2.Bounds))
                    {
                        if (Convert.ToString(food_2.Tag) != Convert.ToString(0)) food_2.Tag = 0;
                        //food_2.Image = Image.FromFile("banana_slot.png");
                        food_2.Image = Properties.Resources.banana_slot;
                        bullet[i].Left = 3000;
                    }
                    else if (bullet[i].Bounds.IntersectsWith(food_3.Bounds))
                    {
                        if (Convert.ToString(food_3.Tag) != Convert.ToString(0)) food_3.Tag = 0;
                        //food_3.Image = Image.FromFile("apple_slot.png");
                        food_3.Image = Properties.Resources.apple_slot;
                        bullet[i].Left = 3000;
                    }
                    else if (bullet[i].Bounds.IntersectsWith(food_4.Bounds))
                    {
                        if (Convert.ToString(food_4.Tag) != Convert.ToString(0)) food_4.Tag = 0;
                        //food_4.Image = Image.FromFile("banana_slot.png");
                        food_4.Image = Properties.Resources.banana_slot;
                        bullet[i].Left = 3000;
                    }
                    

                    else if (bullet[i].Left < 2000) { bullet[i].Left += 15; k++; }
                    

                    if (player_02_exist == true)
                    {
                        if (bullet[i].Bounds.IntersectsWith(player_02.Bounds))
                        {
                            bullet[i].Left = 3000;
                            if (Lifebar_player_02.Value > 30)
                            {
                                Lifebar_player_02.Value -= 30;
                                life_player_02_text.Text = Convert.ToString(Lifebar_player_02.Value) + " %";
                            }
                            else
                            {
                                Lifebar_player_02.Value = 0;
                                life_player_02_text.Text = "0 %";
                                timer1.Stop();
                                MessageBox.Show("Chowder castiga !");
                                Cursor.Show();
                                this.Close();
                                menu main_menu = new menu();
                                main_menu.Show();
                                
                            }
                        }
                    }              
                }
                else if (bullet_direction[i] == 0)
                {
                    if (bullet[i].Bounds.IntersectsWith(block.Bounds)) bullet[i].Left = -200;
                    else if (bullet[i].Bounds.IntersectsWith(block_02.Bounds)) bullet[i].Left = -200;
                    else if (bullet[i].Bounds.IntersectsWith(block_03.Bounds)) bullet[i].Left = -200;
                    else if (bullet[i].Bounds.IntersectsWith(platform.Bounds)) bullet[i].Left = -200;

                    else if (bullet[i].Bounds.IntersectsWith(food_1.Bounds))
                    {
                        if (Convert.ToString(food_1.Tag) != Convert.ToString(0)) food_1.Tag = 0;
                        //food_1.Image = Image.FromFile("apple_slot.png");
                        food_1.Image = Properties.Resources.apple_slot;
                        bullet[i].Left = -200;
                    }
                    else if (bullet[i].Bounds.IntersectsWith(food_2.Bounds))
                    {
                        if (Convert.ToString(food_2.Tag) != Convert.ToString(0)) food_2.Tag = 0;
                        //food_2.Image = Image.FromFile("banana_slot.png");
                        food_2.Image = Properties.Resources.banana_slot;
                        bullet[i].Left = -200;
                    }
                    else if (bullet[i].Bounds.IntersectsWith(food_3.Bounds))
                    {
                        if (Convert.ToString(food_3.Tag) != Convert.ToString(0)) food_3.Tag = 0;
                        //food_3.Image = Image.FromFile("apple_slot.png");
                        food_3.Image = Properties.Resources.apple_slot;
                        bullet[i].Left = -200;
                    }
                    else if (bullet[i].Bounds.IntersectsWith(food_4.Bounds))
                    {
                        if (Convert.ToString(food_4.Tag) != Convert.ToString(0)) food_4.Tag = 0;
                        //food_4.Image = Image.FromFile("banana_slot.png");
                        food_4.Image = Properties.Resources.banana_slot;
                        bullet[i].Left = -200;
                    }

                    else if (bullet[i].Left > -100) { bullet[i].Left -= 15; k++; }

                    if (player_02_exist == true)
                    {
                        if (bullet[i].Bounds.IntersectsWith(player_02.Bounds))
                        {
                            bullet[i].Left = -200;
                            if (Lifebar_player_02.Value > 30)
                            {
                                Lifebar_player_02.Value -= 30;
                                life_player_02_text.Text = Convert.ToString(Lifebar_player_02.Value) + " %";
                            }
                            else
                            {
                                Lifebar_player_02.Value = 0;
                                life_player_02_text.Text = "0 %";
                                timer1.Stop();
                                MessageBox.Show("Chowder castiga !");
                                Cursor.Show();
                                this.Close();
                                menu main_menu = new menu();
                                main_menu.Show();
                            }
                        }
                    }
                }
            }
            if (k == 0) bullet_active = false;
        }                   // Si aici se adauga inventory cand sunt mai multe

        void jumping()
        {
            if (jump == true)
            {
                player.Top -= Force;
                Force -= 1;
            }

            if (player.Top >= ground)
            {
                player.Top = ground;
                if (jump == true) 
                //player.Image = Image.FromFile("chowder_small.png");
                player.Image = Properties.Resources.Chowder_small;
                jump = false;  
            }

            else player.Top +=5;
        }

        void collision_01(PictureBox block)
        {
            int k = 0;// resetare, daca sari la block.bottom atunci se reseteaza in stanga / dreapta

            if (player.Left + player.Width - 5 > block.Left) // blocked. bottom - limita
                if (player.Left + player.Width + 5 < block.Left + block.Width + player.Width)
                    if (player.Top <= block.Bottom)  // or k = 1;
                        if (player.Top > block.Top)
                        {
                            k = 1;  // or  if (player.Top <= block.Bottom+10);
                            //player.Image = Image.FromFile("chowder_small.png");
                            player.Image = Properties.Resources.Chowder_small;
                            jump = false;
                            Force = 0;
                            jump = true;
                        }

            if (player.Right > block.Left)  // blocked. left - limita
                if (player.Left < block.Right - player.Width / 2)
                    if (player.Bottom > block.Top+20)   // +20 pentru platforma (sa nu se reseteze in stanga)
                        if (player.Top < block.Bottom)
                            if(k==0)
                        {   
                            right = false;
                            player.Left = block.Left - player.Width;
                        }               

            if (player.Left < block.Right) // blocked. right - limita
                if (player.Right > block.Left + player.Width / 2)
                    if (player.Bottom > block.Top + 20)   // +20 pentru platforma (sa nu se reseteze in dreapta)
                        if (player.Top < block.Bottom)
                            if(k==0)
                        {
                            left = false;
                            player.Left = block.Right;
                        }
            k = 0;
            //if (!(player.Left + player.Width > block.Left && player.Left + player.Width < block.Left + block.Width + player.Width) && player.Top + player.Height >= block.Top && player.Top < block.Top) jump = true;
        }
      
        void collision_02(PictureBox block)
        {
            if (player.Left + player.Width - 5 > block.Left) // blocked. top - limita
                if (player.Left + player.Width + 5 < block.Left + block.Width + player.Width)
                    if (player.Top + player.Height >= block.Top)
                        if (player.Top < block.Top)
                        {
                            if (jump == true) player.Image = Properties.Resources.Chowder_small;
                            player.Top = block.Top- player.Height; // block.top = block.location.Y
                            jump = false;
                            // Force = 0;
                        }       
        }

        void collect(PictureBox item, int count)
        {
            if (player.Bounds.IntersectsWith(item.Bounds))
                if (Convert.ToString(item.Tag) == Convert.ToString(0))
                {
                    ticket_inventory++;

                    if (ticket_inventory == 1)
                    {
                        if (count == 1) inventory_1.Image = Properties.Resources.inventory_apple;     //sau * = item.Image;
                        else inventory_1.Image = Properties.Resources.inventory_banana;
                        if (Lifebar.Value + 20 < 100) Lifebar.Value += 20;         //life increases
                        else Lifebar.Value = 100;
                        
                    }

                    else if (ticket_inventory == 2)
                    {
                        if (count == 1) inventory_2.Image = Properties.Resources.inventory_apple;
                        else inventory_2.Image = Properties.Resources.inventory_banana;
                        if (Lifebar.Value + 20 < 100) Lifebar.Value += 20;         //life increases
                        else Lifebar.Value = 100;
                    }

                    else if (ticket_inventory == 3)
                    {
                        if (count == 1) inventory_3.Image = Properties.Resources.inventory_apple;
                        else inventory_3.Image = Properties.Resources.inventory_banana;
                        if (Lifebar.Value + 20 < 100) Lifebar.Value += 20;         //life increases
                        else Lifebar.Value = 100;
                    }

                    else if (ticket_inventory == 4)
                    {
                        if (count == 1) inventory_4.Image = Properties.Resources.inventory_apple;
                        else inventory_4.Image = Properties.Resources.inventory_banana;
                        if (Lifebar.Value + 20 < 100) Lifebar.Value += 20;         //life increases
                        else Lifebar.Value = 100;
                    }
                    item.Top = 2000;
                    life_text.Text = Convert.ToString(Lifebar.Value + " %");
                }
        }

        void enemy_move(PictureBox enemy, int lft, int rgt)     //Panini
        {
            if (enemy.Left <= lft) miscare = 3;
            if (enemy.Right >= rgt) miscare = -3;
            enemy.Left += miscare;
            panini_text.Left = enemy.Left;

            if (miscare == 3) enemy.Image = Properties.Resources.panini_right;
            if (miscare == -3) enemy.Image = Properties.Resources.panini_left;

            if (player.Bounds.IntersectsWith(enemy.Bounds))
            {
                panini_text.Text = "Saruta-ma, Chowder !!!";
                panini_hittime = 1;

                if (lovit > 100)
                {
                    lovit = 0;
                    if (Lifebar.Value - 25 > 0)
                    {
                        Lifebar.Value -= 25;
                        life_text.Text = Convert.ToString(Lifebar.Value + " %");
                    }
                    else
                    {
                        Lifebar.Value = 0;
                        life_text.Text = Convert.ToString("0 %");
                        jump = true;
                        Force = G;
                        ground += 2000;
                        inchide = 150;
                        //timer1.Stop();
                        dieonce++;
                        if (dieonce == 1) MessageBox.Show("Chowder a pierdut");
                        Cursor.Show();
                        this.Close();
                        menu main_menu = new menu();
                        main_menu.Show();
                    }
                }
            }
            if (panini_hittime != 0) panini_hittime++;
            if (panini_hittime > 200)
            {
                panini_hittime = 0;
                panini_text.Text = "";
            }

            if (lovit < 210) lovit++;
            //if (inchide <= lovit) Application.Exit();
        }

        void enemy_move_02(PictureBox enemy, int lft, int rgt)      //Shnitzel
        {
            if (enemy.Left <= lft) miscare_02 = 3;
            if (enemy.Right >= rgt) miscare_02 = -3;
            enemy.Left += miscare_02;
            shnitzel_text.Left = enemy_02.Left;

            if (miscare_02 == 3) enemy.Image = Properties.Resources.shnitzel_right;
            if (miscare_02 == -3) enemy.Image = Properties.Resources.shnitzel_left;

            if (player.Bounds.IntersectsWith(enemy.Bounds))
            {
                shnitzel_text.Text = "RADA RADA RADA !!!";
                shnitzel_hittime = 1;

                if (lovit > 100)
                {
                    lovit = 0;
                    if (Lifebar.Value - 25 > 0)
                    { 
                        Lifebar.Value -= 25;
                        life_text.Text = Convert.ToString(Lifebar.Value + " %");
                    }
                    else
                    {
                        Lifebar.Value = 0;
                        life_text.Text = Convert.ToString("0 %");
                        jump = true;
                        Force = G;
                        ground += 2000;
                        inchide = 150;
                        //timer1.Stop();
                        dieonce++;
                        if (dieonce == 1) MessageBox.Show("Chowder a pierdut");
                        Cursor.Show();
                        this.Close();
                        menu main_menu = new menu();
                        main_menu.Show();
                    }
                }
            }
            if (shnitzel_hittime != 0) shnitzel_hittime++;
            if (shnitzel_hittime > 200)
            {
                shnitzel_hittime = 0;
                shnitzel_text.Text = "";
            }

            if (lovit < 210) lovit++;
            //if (inchide <= lovit) Application.Exit();
        }

        void MungDaal_finish()
        {
            if (player.Bounds.IntersectsWith(MungDaal.Bounds))
            {
                if (ticket_inventory == 4)
                {
                    mung_finish = true;
                    MungDaal.Image = Properties.Resources.MungDaal_2;
                    MungDaal_text.Text = "Bravo Chowder !";
                    MungDaal_text.Left = MungDaal.Left-80;
                }
                else { MungDaal_text.Text = "Du-te si adu-mi ingredientele, Chowder !"; mung_hittime = 1; }

                left = false;
                right = false;
                if (player.Left < MungDaal.Left) player.Left = MungDaal.Left - player.Width;
                else player.Left = MungDaal.Right;
            }
            if (mung_hittime != 0) mung_hittime++;
            if (mung_hittime > 200)
            {
                mung_hittime = 0;
                MungDaal_text.Text = "";
            }
            if (mung_finish == true) tick++;
            if (tick == 100)
            {
                timer1.Stop();
                MessageBox.Show("Chowder a castigat");
                Cursor.Show();
                this.Close();
                menu main_menu = new menu();
                main_menu.Show();
            }  //  dupa 3 secunde se inchide
        }

        void platform_move(PictureBox floating, int up, int down)
        {
            if (floating.Top <= up) miscare_03 = 3;
            if (floating.Top >= down) miscare_03 = -3;
            floating.Top += miscare_03;

            collision_01(platform);
            collision_02(platform);          
        }

        void limits(PictureBox person)
        {
            if (person.Right > limit_right) person.Left = limit_right - person.Width;
            if (person.Left < 0) person.Left = 0;           
        }
    }
}
