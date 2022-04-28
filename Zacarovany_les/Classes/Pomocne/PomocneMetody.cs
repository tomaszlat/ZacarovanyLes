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
                case Druh.Bodnuti_Dykou:
                    return "Bodnutí dýkou";
                case Druh.Strelba_Lukem:
                    return "Střelba lukem";
                case Druh.Magicky_sip:
                    return "Magický šíp";
                case Druh.Uskok:
                    return "Úskok";
                case Druh.Uder_Holi:
                    return "Úder Holí";
                case Druh.Ledove_Kopi:
                    return "Ledové kopí";
                case Druh.Magicky_Stit:
                    return "Magický šťít";
                case Druh.Ohniva_Koule:
                    return "Ohnivá koule";
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
        
        public static void NastavButton(Button butt,Schopnost sch, Postava pos, int x, int y, int width, int height, SpriteFont font, Texture2D texture, Color buttonCol, Color textCol)
        {
            string text = "";
            switch (sch.Druh)
            {
                case Druh.Bodnuti_Dykou:
                    text = "Bodnutí dýkou";
                    break;
                case Druh.Bojovy_Pokrik:
                    text = "Bojový pokřik";
                    break;
                case Druh.Lahvicka_Many:
                    text = "Lahvička many [" + pos.Inventar.LahvickyMany + "]";
                    break;
                case Druh.Lahvicka_Zdravi:
                    text = "Lahvička zdraví [" + pos.Inventar.LahvickyZdravi + "]";
                    break;
                case Druh.Ledove_Kopi:
                    text = "Ledové kopí [" + sch.CenaMany + "]";
                    break;
                case Druh.Magicky_sip:
                    text = "Magický šíp [" + sch.CenaMany + "]";
                    break;
                case Druh.Magicky_Stit:
                    text = "Magický štít [" + sch.CenaMany + "]";
                    break;
                case Druh.Obrana_Stitem:
                    text = "Obrana štítem";
                    break;
                case Druh.Ohniva_Koule:
                    text = "Ohnivá koule [" + sch.CenaMany + "]";
                    break;
                case Druh.Regenerace:
                    text = "Regenerace [" + sch.CenaMany + "]";
                    break;
                case Druh.Strelba_Lukem:
                    text = "Střelba lukem";
                    break;
                case Druh.Uder_Holi:
                    text = "Úder holí";
                    break;
                case Druh.Uskok:
                    text = "Úskok";
                    break;
                case Druh.Utek:
                    text = "Útěk";
                    break;
                case Druh.Utok_Mecem:
                    text = "Útok mečem";
                    break;
                default:
                    text = "";
                    break;
            }
            butt.Position = new Vector2(x, y);
            butt.Texture = texture;
            butt.Text = text;
            butt.Druh = sch.Druh;
            if (sch.Cd > 0)
            {
                butt.Text += " (" + sch.Cd + ")";
            }
            if (sch.Cd > 0 || sch.CenaMany > pos.Mana)
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
