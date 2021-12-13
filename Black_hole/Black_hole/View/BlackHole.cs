using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Black_hole.Model;
using Black_hole.Persistence;
namespace Black_hole.View
{
    public partial class BlackHole : Form
    {
        private BlackHoleModel model;
        private Button[,] buttons;
        private const int ButtonSize = 85;
        private List<Button> previousButtons;
        public BlackHole()
        {
            InitializeComponent();
        }

        private void BlackHole_Load(object sender, EventArgs e)
        {
            model = new BlackHoleModel();
            previousButtons = new List<Button>();
            model.ChangeLabel += Label;
            model.GameOver += GameOver;
            model.RefreshTable += RefreshTable;
            btnNewGame.Click += NewGame;
            btnGameSave.Click += SaveGame;
            btnLoadGame.Click += LoadGame;
            SetupMenus();
            GenerateTable();
        }

        private void SetupMenus()
        {
        }

        private void GameOver(object sender, int e)
        {
            DisableButtons();
            MessageBox.Show("Gratulálok!\nA "+ (e == 1 ? "piros":"kék")+" színű játékos győzött!", "Gratuláció");
        }

        private void DisableButtons()
        {
            foreach(Button btn in buttons)
            {
                btn.Enabled = false;
            }
        }
        private void DeleteButtons()
        {
            foreach(Button btn in buttons)
            {
                Controls.Remove(btn);
            }
        }
        private void Label(object sender, int e)
        {
            label1.Text = "Jelenlegi játékos: " + (e == 1 ? "piros" : "kék");
        }
        private void NewGame(object sender, EventArgs e)
        {
            BlackHoleDialog ujjatek = new BlackHoleDialog("Válassz pályaméretet!",new string[] { "5x5", "7x7", "9x9" });
            ujjatek.ShowDialog();
            if(ujjatek.result != -1 )
            {
                DeleteButtons();
                model.newGame(ujjatek.result);
                GenerateTable();
            }
        }
        private async void SaveGame(object sender, EventArgs e)
        {

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // játé mentése
                    await model.SaveGameAsync(saveFileDialog.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    private async void LoadGame(object sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog() == DialogResult.OK) // ha kiválasztottunk egy fájlt
        {
            try
            {
                // játék betöltése
                await model.LoadGameAsync(openFileDialog.FileName);

            }
            catch (Exception)
            {
                MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                model.newGame(model.size);
            }
            DeleteButtons();
            GenerateTable();
        }
    }
    private void GenerateTable()
        {
            int size = model.size;
            this.Size = new Size(size * 100 +25 , size * 100 + 75);   
            buttons = new Button[size, size];
            for (Int32 i = 0; i < size; i++)
                for (Int32 j = 0; j < size; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Location = new Point(30 + ButtonSize * j, 90 + ButtonSize * i); // elhelyezkedés
                    buttons[i, j].Size = new Size(ButtonSize, ButtonSize); // méret
                    buttons[i, j].Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold); // betűtípus
                    buttons[i, j].Name = i + ";" + j  ; // a gomb indexeit a nevében tároljuk
                    buttons[i, j].MouseClick += new MouseEventHandler(ButtonsClicked);
                    buttons[i,j].BackgroundImageLayout = ImageLayout.Stretch;
                    if (model.table[i,j] != 0)
                    {
                        if(model.table[i,j] == -1)
                        {
                            buttons[i, j].BackgroundImage = Black_hole.Properties.Resources.black_hole;
                        } else
                        {
                            buttons[i, j].BackgroundImage = model.table[i, j] == 1 ? Black_hole.Properties.Resources.red_starship : Black_hole.Properties.Resources.blue_starship;
                        }

                    } else
                    {
                        buttons[i, j].BackgroundImage = null;
                    }
                    Controls.Add(buttons[i, j]);
                }
        }
        private void DeletePreviousButtons()
        {
            foreach (Button forbtn in previousButtons)
            {
                forbtn.Name = Convert.ToInt32(forbtn.Name.Split(";")[0]) + ";" + Convert.ToInt32(forbtn.Name.Split(";")[1]);//nev frissitese
                Int32 i = Convert.ToInt32(forbtn.Name.Split(";")[0]);
                Int32 j = Convert.ToInt32(forbtn.Name.Split(";")[1]);
                if (model.table[i, j] != 0)
                {
                    if (model.table[i, j] == -1)
                    {
                        buttons[i, j].BackgroundImage = Black_hole.Properties.Resources.black_hole;
                    }
                    else
                    {
                        buttons[i, j].BackgroundImage = model.table[i, j] == 1 ? Black_hole.Properties.Resources.red_starship : Black_hole.Properties.Resources.blue_starship;
                    }

                }
                else
                {
                    buttons[i, j].BackgroundImage = null;
                }
            }
            previousButtons.Clear();
        }
        private void ButtonsClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Int32 size = model.size;
            Int32 x = Convert.ToInt32((sender as Button).Name.Split(";")[0]);
            Int32 y = Convert.ToInt32((sender as Button).Name.Split(";")[1]);
            if (model.table[x,y] == 0 || model.table[x, y] == -1 || model.table[x,y] != model.currentPlayer)
            {
                if(model.table[x, y] != 1 && model.table[x, y] != 2 && buttons[x, y].BackgroundImage != Black_hole.Properties.Resources.black_hole && buttons[x,y].BackgroundImage != null)
                {
                    ArrowClicked(x, y);
                }
                DeletePreviousButtons();
                return;
            }
            // EZ azért szükséges, hogy az elöző NYILAKAT eltuntesse a nezetrol
            DeletePreviousButtons();

            if (x+1<size && (model.table[x + 1, y] == 0 || model.table[x+1,y] == -1))
            {
                buttons[x + 1, y].Name += ";vertical;1";//elretjük a nevébe az irányt
                buttons[x + 1, y].BackgroundImage = model.currentPlayer == 1 ? Black_hole.Properties.Resources.red_arrow_down : Black_hole.Properties.Resources.blue_arrow_down;
                previousButtons.Add(buttons[x+1,y]);
            }
            if(x - 1 >= 0 && (model.table[x - 1, y] == 0 || model.table[x - 1, y] == -1))
            {
                buttons[x - 1, y].Name += ";vertical;-1";
                buttons[x - 1, y].BackgroundImage = model.currentPlayer == 1 ? Black_hole.Properties.Resources.red_arrow_up : Black_hole.Properties.Resources.blue_arrow_up;
                previousButtons.Add(buttons[x - 1, y]);
            }
            if (y + 1 < size && (model.table[x,y + 1] == 0 || model.table[x,y+ 1] == -1))
            {
                buttons[x, y + 1].Name += ";horizontal;1";
                buttons[x,y + 1].BackgroundImage = model.currentPlayer == 1 ? Black_hole.Properties.Resources.red_arrow_right : Black_hole.Properties.Resources.blue_arrow_right;
                previousButtons.Add(buttons[x, y+1]);
            }
            if (y - 1 >= 0 && (model.table[x,y - 1] == 0 || model.table[x, y - 1] == -1))
            {
                buttons[x, y - 1].Name += ";horizontal;-1";
                buttons[x ,y - 1].BackgroundImage = model.currentPlayer == 1 ? Black_hole.Properties.Resources.red_arrow_left : Black_hole.Properties.Resources.blue_arrow_left;
                previousButtons.Add(buttons[x, y-1]);
            }
        }
        
        private void ArrowClicked(int x, int y)
        {
            if (buttons[x, y].Name.Split(";").Length == 2) return;
            Direction dir = buttons[x, y].Name.Split(";")[2] == "vertical" ? Direction.Vertical : Direction.Horizontal;
            int way = buttons[x, y].Name.Split(";")[3] == "1" ? 1 : -1;
            if(dir == Direction.Vertical)
            {
                x -= way;
            } else
            {
                y -= way;
            }
            model.Step(x,y,way,dir);
        }

        private void RefreshTable( object sender = null, EventArgs e = null)
        {
            foreach(Button btn in buttons)
            {
                btn.Name = Convert.ToInt32(btn.Name.Split(";")[0]) + ";" + Convert.ToInt32(btn.Name.Split(";")[1]);//nev frissitese
                Int32 i = Convert.ToInt32(btn.Name.Split(";")[0]);
                Int32 j = Convert.ToInt32(btn.Name.Split(";")[1]);
                if (model.table[i, j] != 0)
                {
                    if (model.table[i, j] == -1)
                    {
                        buttons[i, j].BackgroundImage = Black_hole.Properties.Resources.black_hole;
                    }
                    else
                    {
                        buttons[i, j].BackgroundImage = model.table[i, j] == 1 ? Black_hole.Properties.Resources.red_starship : Black_hole.Properties.Resources.blue_starship;
                    }

                }
                else
                {
                    buttons[i, j].BackgroundImage = null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e){}
    }
}
