using DataContracts.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSwipesLibrary.ExtensionMethods
{
    public static class StreamExtensionMethods
    {
        public static string ToIndexStream(this Stream stream)
        {
            return String.Format("{0}_{1}",stream.Lang.ToLower(), stream.Text.ToLower());
        }

        public static Stream ToStream(this string indexStream)
        {
            var splits = indexStream.Split('_');
            return new Stream
            {
                Lang = char.ToUpper(splits[0][0]) + splits[0].Substring(1),
                Text = char.ToUpper(splits[1][0]) + splits[1].Substring(1)
            };
        }
    }
}
