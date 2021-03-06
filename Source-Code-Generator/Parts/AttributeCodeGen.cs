﻿using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace SourceCodeGenerator.Parts
{
    public class AttributeCodeGen: GeneratorBase
    {
        public string Name;
        public string Type;
        public string Key;
        public string Separator;

        public string Help;

        // ReSharper disable once InconsistentNaming
        public const string DefaultType = "string";

        public AttributeCodeGen(string name, string type = DefaultType, string separator = null, string help = null)
        {
            Name = string.Join("", name.Split('-').Select(FirstCharToUpper));
            Key = name;
            Type = type;
            Separator = separator;
            Help = help;
        }

        private string GetSeparator()
        {
            string result = null;
            if (!string.IsNullOrEmpty(Separator)) result = Separator;
            else if (Key == "srcset") result = ",";
            return result == null ? "" : $", \"{result}\"";
        }

        private string Method(string className) => $"    public {className} {Name}";

        public string Code(TagCodeGenerator tag)
        {
            var valueType = Type;
            var allMethods = new[]
            {
                MethodString(tag),
                MethodTyped(tag, valueType), // optional second signature with a int-type or something
                CodeForBooleanAttribute(tag), 
                CodeForSrcSetAttribute(tag),
                "" // empty, to ensure trailing enters in generated code
            };
            return string.Join("\n\n", allMethods.Where(sc => sc != null));
        }

        private string Method(TagCodeGenerator tag, string valueType) => 
            $"{Method(tag.ClassName)}({valueType} value) => this.Attr(\"{Key}\", value{GetSeparator()});";

        private string MethodString(TagCodeGenerator tag) => Method(tag, DefaultType);

        private string MethodTyped(TagCodeGenerator tag, string type) =>
            type == DefaultType 
                ? "" 
                : Method(tag, type);

        private string CodeForBooleanAttribute(TagCodeGenerator tag) =>
            IsBooleanAttribute()
                ? $"{Method(tag.ClassName)}() => this.Attr(\"{Key}\");"
                : null;

        private string CodeForSrcSetAttribute(TagCodeGenerator tag) =>
            Key != "srcset"
                ? null
                : $"{Method(tag.ClassName)}(int multiplier, string name) => {Name}(name + \" \" + multiplier + (multiplier > 8 ? \"w\" : \"x\"));";


        /// <summary>
        /// tells us if the desired attribute is a boolean
        /// this means that the attribute can be added by itself, without a value
        /// </summary>
        /// <returns></returns>
        public bool IsBooleanAttribute() => BooleanAttributes.Contains(Key);

        // got from here https://github.com/iandevlin/html-attributes/blob/master/boolean-attributes.json
        // ReSharper disable StringLiteralTypo
        public static string[] BooleanAttributes = {
            "allowfullscreen",
            "allowpaymentrequest",
            "async",
            "autofocus",
            "autoplay",
            "checked",
            "controls",
            "default",
            "defer",
            "disabled",
            "formnovalidate",
            "hidden",
            "ismap",
            "itemscope",
            "loop",
            "multiple",
            "muted",
            "nomodule",
            "novalidate",
            "open",
            "readonly",
            "required",
            "reversed",
            "selected",
            "typemustmatch"};
        // ReSharper restore StringLiteralTypo

    }

    // todo: maybe add enumerated attributes like 
    // https://github.com/iandevlin/html-attributes/blob/master/enumerated-attributes.json
}
