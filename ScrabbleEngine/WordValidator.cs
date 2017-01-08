using System;
using System.Data.SqlServerCe;

namespace ScrabbleEngine
{
    internal static class WordValidator
    {
        private const string datasourceString = "datasource=";
        private const string commandFormattedString = "SELECT {0} FROM {1} WHERE {0}='{2}'";
        private static readonly string assemblyLocation = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;
        private static readonly string dictionaryConnectionString = datasourceString + assemblyLocation + Settings.dictionaryPathString;
        private static readonly SqlCeConnection dictionaryConnection = new SqlCeConnection(dictionaryConnectionString);

        internal static bool IsValid(string word)
        {
            WordValidator.dictionaryConnection.Open();
            SqlCeCommand selectWordCommand = new SqlCeCommand(String.Format(WordValidator.commandFormattedString, Settings.wordColumnNameString, Settings.wordTableNameString, word), dictionaryConnection);
            Object dictionaryQueryResult = selectWordCommand.ExecuteScalar();
            WordValidator.dictionaryConnection.Close();
            
            if (dictionaryQueryResult != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
