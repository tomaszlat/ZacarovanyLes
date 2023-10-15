using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
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
                        case Trida.Bojovnik: return "Bojovník";
                        case Trida.Lucistnik: return "Lučištník";
                        case Trida.Kouzelnik: return "Kouzelník";
                    }
                    break;
                case Pohlavi.Zena:
                    switch (trida)
                    {
                        case Trida.Bojovnik: return "Bojovnice";
                        case Trida.Lucistnik: return "Lučištnice";
                        case Trida.Kouzelnik: return "Kouzelnice";
                    }
                    break;

            }
            return "";
        }

        public static string SchopnostToString(Druh druh)
        {
            switch (druh)
            {
                case Druh.Utok_Mecem:
                    return "Útok mečem";
                case Druh.Obrana_Stitem:
                    return "Obrana štítem";
                case Druh.Bojovy_Pokrik:
                    return "Bojový pokřik";
                case Druh.Regenerace:
                    return "Regenerace";
                case Druh.Uder_stitem:
                    return "Úder štítem";
                case Druh.Vrh_sekerou:
                    return "Vrh sekerou";
                case Druh.Berserk:
                    return "Berserk";
                case Druh.Bodnuti_Dykou:
                    return "Bodnutí dýkou";
                case Druh.Strelba_Lukem:
                    return "Střelba lukem";
                case Druh.Magicky_sip:
                    return "Magický šíp";
                case Druh.Uskok:
                    return "Úskok";
                case Druh.Rychlost:
                    return "Rychlost";
                case Druh.Lesni_bobule:
                    return "Lesní bobule";
                case Druh.Jedova_sipka:
                    return "Jedová šipka";
                case Druh.Uder_Holi:
                    return "Úder Holí";
                case Druh.Ledove_Kopi:
                    return "Ledové kopí";
                case Druh.Magicky_Stit:
                    return "Magický šťít";
                case Druh.Ohniva_Koule:
                    return "Ohnivá koule";
                case Druh.Vysati_zivota:
                    return "Vysátí života";
                case Druh.Vysati_many:
                    return "Vysátí many";
                case Druh.Magicke_soustredeni:
                    return "Magické soustředění";
                case Druh.Lahvicka_Many:
                    return "Lahvička many";
                case Druh.Lahvicka_Zdravi:
                    return "Lahvička zdraví";
                case Druh.Utek:
                    return "Útěk";
                case Druh.Zadna:
                default:
                    return "";
            }
        }

        public static string PohlaviToString(Pohlavi pohlavi)
        {
            switch (pohlavi)
            {
                case Pohlavi.Muz: return "Muž";
                case Pohlavi.Zena: return "Žena";
            }
            return "";
        }

        public static Pohlavi StringToPohlavi(string pohlavi)
        {
            switch (pohlavi)
            {
                case "Muž": return Pohlavi.Muz;
                case "Žena": return Pohlavi.Zena;
                default: return Pohlavi.Muz;
            }
        }

        public static Majitel StringToMajitel(string majitel)
        {
            switch (majitel)
            {
                case "Hráč": return Majitel.Hrac;
                case "AI": return Majitel.Pocitac_Tezky;
                default: return Majitel.Hrac;
            }
        }
        public static Minulost StringToMinulost(string minulost)
        {
            switch (minulost)
            {
                case "Rytíř": return Minulost.Rytir;
                case "Lovec": return Minulost.Lovec;
                case "Čaroděj": return Minulost.Carodej;
                default: return Minulost.Rytir;
            }
        }

        public static Trida StringToTrida(string trida)
        {
            switch (trida)
            {
                case "Válečník": return Trida.Bojovnik;
                case "Lučištník": return Trida.Lucistnik;
                case "Kouzelník": return Trida.Kouzelnik;
                default: return Trida.Bojovnik;
            }
        }

        public static void NastavButton(Efekty efekty, Button butt, Schopnost sch, Postava pos, int x, int y, int width, int height, SpriteFont font, Texture2D texture, Color buttonCol, Color textCol)
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
