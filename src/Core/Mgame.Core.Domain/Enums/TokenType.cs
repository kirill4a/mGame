using Mgame.Core.Domain.Base;

namespace Mgame.Core.Domain.Enums;

/// <summary>
/// Types (colors) of the game tokens
/// </summary>
public abstract class TokenType : EnumPattern
{
    public static TokenType Green = new GreenTokenType();
    public static TokenType Red = new GreenTokenType();
    public static TokenType Blue = new GreenTokenType();
    public static TokenType Yellow = new YellowTokenType();

    private TokenType(int value, string name, string hexColor) : base(value, name)
    {
        ColorHex = hexColor;
    }

    public string ColorHex { get; }

    sealed class GreenTokenType : TokenType
    {
        public GreenTokenType() : base(0, "green", "#228B22") { }
    }
    sealed class RedTokenType : TokenType
    {
        public RedTokenType() : base(1, "red", "#FF0000") { }
    }

    sealed class BlueTokenType : TokenType
    {
        public BlueTokenType() : base(2, "blue", "#0000FF") { }
    }

    sealed class YellowTokenType : TokenType
    {
        public YellowTokenType() : base(3, "yellow", "#FFFF00") { }
    }
}