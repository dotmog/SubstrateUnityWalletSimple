using SubstrateNetApi.Model.Types.Enum;
using System;
using System.Numerics;
using UnityEngine;

namespace Assets.Scripts
{
    // https://stackoverflow.com/questions/33295120/how-to-generate-gif-256-colors-palette

    public enum ColorMaps
    {
        Color256, Grey64,
        Rarity5
    }

    public class ColorSchema
    {
        public static Color MsgWarn = new Color(160 / 255f, 31 / 255f, 18 / 255f, 1f);
        public static Color Address = new Color(80 / 255f, 185 / 255f, 255 / 255f, 1f);
        public static Color Connect = new Color(60 / 255f, 174 / 255f, 121 / 255f, 1f);

        public static Color MogBoxStandard = new Color(80 / 255f, 185 / 255f, 255 / 255f, 1f);
        public static Color MogBoxCurrent = new Color(0 / 255f, 255 / 255f, 185 / 255f, 1f);
        public static Color MogBoxTarget = new Color(255 / 255f, 0 / 255f, 20 / 255f, 1f);
        public static Color MogBoxIsButton = new Color(0 / 255f, 255 / 255f, 185 / 255f, 150 / 255f);

        private static Color[] _colorMap256Array = null;

        private static Color[] _greyMap64Array = null;

        private static Color[] _rarity5Array = null;

        public static Color NumberSign(double value) => value > 0 ? Color.green : value < 0 ? Color.red : Color.gray;

        public static Color ColorCoinValue(BigInteger value)
        {
            return value > 0 ? Color.yellow : Color.gray;
        }

        public static Color ColorMap(int value, ColorMaps colorMap = ColorMaps.Color256)
        {
            switch (colorMap)
            {
                case ColorMaps.Color256:
                    _colorMap256Array ??= LoadColorMap(Color256);
                    return _colorMap256Array[value % _colorMap256Array.Length];

                case ColorMaps.Grey64:
                    _greyMap64Array ??= LoadColorMap(Grey64);
                    return _greyMap64Array[value % _greyMap64Array.Length];

                case ColorMaps.Rarity5:
                    _rarity5Array ??= LoadColorMap(Rarity5);
                    return _rarity5Array[value % _rarity5Array.Length];

                default:
                    _colorMap256Array ??= LoadColorMap(Color256);
                    return _colorMap256Array[value % _colorMap256Array.Length];
            }
        }

        private static Color[] LoadColorMap(string[] colors)
        {
            var colorMap = new Color[colors.Length];
            for (var i = 0; i < colors.Length; i++)
            {
                if (ColorUtility.TryParseHtmlString(colors[i], out var color))
                {
                    colorMap[i] = color;
                }
            }
            return colorMap;
        }

        private static readonly string[] Grey64 = {
            "#57595D", "#797A7D", "#949597", "#A9AAAC", "#BABBBD", "#C8C9CA", "#D3D4D5",
            "#DCDDDD", "#E3E4E4", "#E9E9E9", "#FFFFFF", "#46474A", "#38393B", "#2D2E2F",
            "#242526", "#1D1E1E", "#171818", "#121313", "#0E0F0F", "#0B0C0C", "#000000",
            "#000000", "#080808", "#101010", "#181818", "#202020", "#282828", "#303030",
            "#383838", "#404040", "#484848", "#505050", "#585858", "#606060", "#686868",
            "#696969", "#707070", "#787878", "#808080", "#888888", "#909090", "#989898",
            "#A0A0A0", "#A8A8A8", "#A9A9A9", "#B0B0B0", "#B8B8B8", "#BEBEBE", "#C8C8C8",
            "#D0D0D0", "#D8D8D8", "#E0E0E0", "#E8E8E8", "#F0F0F0", "#F5F5F5", "#F8F8F8"
        };

        private static readonly string[] Rarity5 = {
            "#AEAEAE", "#1EC332", "#1E45C3", "#C31EB4", "#C3491E"
        };

        private static readonly string[] Color256 = {
              "#B88183","#922329","#5A0007","#D7BFC2","#D86A78","#FF8A9A","#3B000A","#E20027"
             ,"#943A4D","#5B4E51","#B05B6F","#FEB2C6","#D83D66","#895563","#FF1A59","#FFDBE5"
             ,"#CC0744","#CB7E98","#997D87","#6A3A4C","#FF2F80","#6B002C","#A74571","#C6005A"
             ,"#FF5DA7","#300018","#B894A6","#FF90C9","#7C6571","#A30059","#DA007C","#5B113C"
             ,"#402334","#D157A0","#DDB6D0","#885578","#962B75","#A97399","#D20096","#E773CE"
             ,"#AA5199","#E704C4","#6B3A64","#FFA0F2","#6F0062","#B903AA","#C895C5","#FF34FF"
             ,"#320033","#DBD5DD","#EEC3FF","#BC23FF","#671190","#201625","#F5E1FF","#BC65E9"
             ,"#D790FF","#72418F","#4A3B53","#9556BD","#B4A8BD","#7900D7","#A079BF","#958A9F"
             ,"#837393","#64547B","#3A2465","#353339","#BCB1E5","#9F94F0","#9695C5","#0000A6"
             ,"#000035","#636375","#00005F","#97979E","#7A7BFF","#3C3E6E","#6367A9","#494B5A"
             ,"#3B5DFF","#C8D0F6","#6D80BA","#8FB0FF","#0045D2","#7A87A1","#324E72","#00489C"
             ,"#0060CD","#789EC9","#012C58","#99ADC0","#001325","#DDEFFF","#59738A","#0086ED"
             ,"#75797C","#BDC9D2","#3E89BE","#8CD0FF","#0AA3F7","#6B94AA","#29607C","#404E55"
             ,"#006FA6","#013349","#0AA6D8","#658188","#5EBCD1","#456D75","#0089A3","#B5F4FF"
             ,"#02525F","#1CE6FF","#001C1E","#203B3C","#A3C8C9","#00A6AA","#00C6C8","#006A66"
             ,"#518A87","#E4FFFC","#66E1D3","#004D43","#809693","#15A08A","#00846F","#00C2A0"
             ,"#00FECF","#78AFA1","#02684E","#C2FFED","#47675D","#00D891","#004B28","#8ADBB4"
             ,"#0CBD66","#549E79","#1A3A2A","#6C8F7D","#008941","#63FFAC","#1BE177","#006C31"
             ,"#B5D6C3","#3D4F44","#4B8160","#66796D","#71BB8C","#04F757","#001E09","#D2DCD5"
             ,"#00B433","#9FB2A4","#003109","#A3F3AB","#456648","#51A058","#83A485","#7ED379"
             ,"#D1F7CE","#A1C299","#061203","#1E6E00","#5EFF03","#55813B","#3B9700","#4FC601"
             ,"#1B4400","#C2FF99","#788D66","#868E7E","#83AB58","#374527","#98D058","#C6DC99"
             ,"#A4E804","#76912F","#8BB400","#34362D","#4C6001","#DFFB71","#6A714A","#222800"
             ,"#6B7900","#3A3F00","#BEC459","#FEFFE6","#A3A489","#9FA064","#FFFF00","#61615A"
             ,"#FFFFFE","#9B9700","#CFCDAC","#797868","#575329","#FFF69F","#8D8546","#F4D749"
             ,"#7E6405","#1D1702","#CCAA35","#CCB87C","#453C23","#513A01","#FFB500","#A77500"
             ,"#D68E01","#B79762","#7A4900","#372101","#886F4C","#A45B02","#E7AB63","#FAD09F"
             ,"#C0B9B2","#938A81","#A38469","#D16100","#A76F42","#5B4534","#5B3213","#CA834E"
             ,"#FF913F","#953F00","#D0AC94","#7D5A44","#BE4700","#FDE8DC","#772600","#A05837"
             ,"#EA8B66","#391406","#FF6832","#C86240","#29201D","#B77B68","#806C66","#FFAA92"
             ,"#89412E","#E83000","#A88C85","#F7C9BF","#643127","#E98176","#7B4F4B","#1E0200"
             ,"#9C6966","#BF5650","#BA0900","#FF4A46","#F4ABAA","#000000","#452C2C","#C8A1A1"
        };
    }
}
