using System;
using System.IO;
using System.Windows.Forms;
using Zacarovany_les.Classes.Mapy;

namespace Zacarovany_les.Classes.Pomocne
{
    static public class UkladaniNacitani
    {
        private static void Serializuj(string file)
        {
            using BinaryWriter binWriter = new BinaryWriter(File.Open(file, FileMode.Create));
            ZacarovanyLes.utocnik.Write(binWriter);
            ZacarovanyLes.maps.Write(binWriter);
            binWriter.Close();
        }
        private static void Deserializuj(string file)
        {
            using BinaryReader binReader = new BinaryReader(File.Open(file, FileMode.Open));
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
        public static void Nacti()
        {
            try
            {
                string adresar = Directory.GetCurrentDirectory() + "\\saves";
                Directory.CreateDirectory(adresar);
                System.Diagnostics.Debug.WriteLine(adresar);
                using OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    InitialDirectory = adresar,
                    Filter = "save files (*.save)|*.save|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Deserializuj(Path.GetFullPath(openFileDialog.FileName));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Nacti:" + ex.Message);
            }
        }
        public static void Uloz()
        {
            try
            {
                string adresar = Directory.GetCurrentDirectory() + "\\saves";
                Directory.CreateDirectory(adresar);
                System.Diagnostics.Debug.WriteLine(adresar);
                using SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = adresar,
                    Filter = "save files (*.save)|*.save|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Serializuj(Path.GetFullPath(saveFileDialog1.FileName));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Uloz:" + ex.Message);
            }
        }
    }
}
