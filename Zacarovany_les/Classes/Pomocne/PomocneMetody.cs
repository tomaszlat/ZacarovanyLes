using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zacarovany_les.Classes.Interface;

namespace Zacarovany_les.Classes.Pomocne
{
    static class PomocneMetody
    {
        public static string TridaToString(Trida trida, Pohlavi pohlavi)
        {
            switch (pohlavi)
            {
                case Pohlavi.Muz:
                    switch (trida)
                    {
                        case Trida.Valecnik: return "Válečník";
                        case Trida.Lucistnik: return "Lučištník";
                        case Trida.Kouzelnik: return "Kouzelník";
                    }
                    break;
                case Pohlavi.Zena:
                    switch (trida)
                    {
                        case Trida.Valecnik: return "Válečnice";
                        case Trida.Lucistnik: return "Lučištnice";
                        case Trida.Kouzelnik: return "Kouzelnice";
                    }
                    break;

            }
            return "";
        }

        public static string SchopnostToString(Druh druh)
        {
            return druh switch
            {
                Druh.Utok_Mecem => "Útok mečem",
                Druh.Obrana_Stitem => "Obrana štítem",
                Druh.Bojovy_Pokrik => "Bojový pokřik",
                Druh.Regenerace => "Regenerace",
                Druh.Uder_stitem => "Úder štítem",
                Druh.Vrh_sekerou => "Vrh sekerou",
                Druh.Berserk => "Berserk",
                Druh.Bodnuti_Dykou => "Bodnutí dýkou",
                Druh.Strelba_Lukem => "Střelba lukem",
                Druh.Magicky_sip => "Magický šíp",
                Druh.Uskok => "Úskok",
                Druh.Rychlost => "Rychlost",
                Druh.Lesni_bobule => "Lesní bobule",
                Druh.Jedova_sipka => "Jedová šipka",
                Druh.Uder_Holi => "Úder Holí",
                Druh.Ledove_Kopi => "Ledové kopí",
                Druh.Magicky_Stit => "Magický šťít",
                Druh.Ohniva_Koule => "Ohnivá koule",
                Druh.Vysati_zivota => "Vysátí života",
                Druh.Vysati_many => "Vysátí many",
                Druh.Magicke_soustredeni => "Magické soustředění",
                Druh.Lahvicka_Many => "Lahvička many",
                Druh.Lahvicka_Zdravi => "Lahvička zdraví",
                Druh.Utek => "Útěk",
                _ => "",
            };
        }

        public static string PohlaviToString(Pohlavi pohlavi)
        {
            return pohlavi switch
            {
                Pohlavi.Muz => "Muž",
                Pohlavi.Zena => "Žena",
                _ => "",
            };
        }

        public static Pohlavi StringToPohlavi(string pohlavi)
        {
            return pohlavi switch
            {
                "Muž" => Pohlavi.Muz,
                "Žena" => Pohlavi.Zena,
                _ => Pohlavi.Muz,
            };
        }

        public static Majitel StringToMajitel(string majitel)
        {
            return majitel switch
            {
                "Hráč" => Majitel.Hrac,
                "AI" => Majitel.Pocitac_Tezky,
                _ => Majitel.Hrac,
            };
        }
        public static Minulost StringToMinulost(string minulost)
        {
            return minulost switch
            {
                "Rytíř" => Minulost.Rytir,
                "Lovec" => Minulost.Lovec,
                "Čaroděj" => Minulost.Carodej,
                _ => Minulost.Rytir,
            };
        }

        public static Trida StringToTrida(string trida)
        {
            return trida switch
            {
                "Válečník" => Trida.Valecnik,
                "Lučištník" => Trida.Lucistnik,
                "Kouzelník" => Trida.Kouzelnik,
                _ => Trida.Valecnik,
            };
        }

        public static void NastavButtonSchopnosti(Efekty efekty, Button butt, Schopnost sch, Postava pos, int x, int y, int width, int height, SpriteFont font, Texture2D texture, Color buttonCol, Color textCol)
        {
            string text = SchopnostToString(sch.Druh);
            switch (sch.Druh)
            {
                case Druh.Lahvicka_Many:
                    text += " [" + pos.Inventar.LahvickyMany + "]";
                    break;
                case Druh.Lahvicka_Zdravi:
                    text += " [" + pos.Inventar.LahvickyZdravi + "]";
                    break;
                default:
                    if (sch.CenaMany > 0)
                    {
                        text += " [" + (efekty.Soustredeni > 0 ? sch.CenaMany / 2 : sch.CenaMany) + "]";
                    }
                    break;
            }
            butt.Position = new Vector2(x, y);
            butt.Texture = texture;
            butt.Color1 = buttonCol;
            butt.FontColor = textCol;
            butt.Text = text;
            butt.Druh = sch.Druh;
            butt.Height = height;
            butt.Width = width;
            butt.Font = font;

            if (sch.Cd > 0)
            {
                butt.Text += " (" + sch.Cd + ")";
            }
            if (sch.Cd > 0 ||  (sch.CenaMany > pos.Mana && efekty.Soustredeni==0) || (sch.CenaMany/2 > pos.Mana && efekty.Soustredeni > 0))
            {
                butt.Clicable = false;
            }
            else
            {
                butt.Clicable = true;
                if ((sch.Druh == Druh.Lahvicka_Many && pos.Inventar.LahvickyMany <= 0) || (sch.Druh == Druh.Lahvicka_Zdravi && pos.Inventar.LahvickyZdravi <= 0))
                {
                    butt.Clicable = false;
                }
            }
        }
    }
}
