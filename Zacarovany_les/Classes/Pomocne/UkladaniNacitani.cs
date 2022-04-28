using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Zacarovany_les.Classes.Mapy;

namespace Zacarovany_les.Classes.Pomocne
{
    static public class UkladaniNacitani
    {
        private static void Serializuj(string file)
        {
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(file, FileMode.Create)))
            {
                ZacarovanyLes.utocnik.Write(binWriter);
                ZacarovanyLes.maps.Write(binWriter);
                binWriter.Close();
            }
        }
        private static void Deserializuj(string file)
        {
            using (BinaryReader binReader = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                ZacarovanyLes.game.ChangeCurrentState(ZacarovanyLes.menuState);
                ZacarovanyLes.gameState = null;
                ZacarovanyLes.utocnik = Postava.Read(binReader);
                ZacarovanyLes.maps = MapManager.Read(binReader);
                ZacarovanyLes.mapState = new MapState(ZacarovanyLes.game, ZacarovanyLes.content);
                ZacarovanyLes.mapState.LoadContent();
                ((MapState)ZacarovanyLes.mapState).UpdateTextures();
                ZacarovanyLes.game.ChangeCurrentState(ZacarovanyLes.mapState);
                binReader.Close();
            }
        }
        public static void Nacti()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "save files (*.save)|*.save|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    Deserializuj(Path.GetFullPath(openFileDialog.FileName));
                    
                }
            }
        }
        public static void Uloz()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = "c:\\";
            saveFileDialog1.Filter = "save files (*.save)|*.save|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Serializuj(Path.GetFullPath(saveFileDialog1.FileName));
            }
        }
    }
}
