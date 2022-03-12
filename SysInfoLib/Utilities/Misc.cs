namespace SysInfoLib.Utilities
{
    internal static class Misc
    {
        ///<summary> Grep first line starts with a string from a string </summary>
        ///<param name="s"> String to grep </param>
        ///<param name="startString"> String to match </param>
        ///<returns> The first string line from specified file that matched given string </returns>
        public static async Task<string> GrepLineStartsWith(string s, string startString)
        {
            using (var sr = new StringReader(s))
            {
                string? line;
                while ((line = await sr.ReadLineAsync()) != null) {
                    if (line.StartsWith(startString)) {
                        return line;
                    }
                }
            }

            return "";
        }
    }
}
