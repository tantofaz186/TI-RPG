namespace SaveGame
{
    public static class VigenereCipher
    {
        // Chave utilizada para criptografar e descriptografar
        private const string encryptionKey = "TI_RPG";

        public static string Encrypt(string input)
        {
            string encryptedText = "";
            int keyIndex = 0;

            foreach (char currentChar in input)
            {
                int keyChar = encryptionKey[keyIndex % encryptionKey.Length];
                char encryptedChar = (char)(currentChar + keyChar);
                encryptedText += encryptedChar;
                keyIndex++;
            }

            return encryptedText;
        }

        public static string Decrypt(string input)
        {
            string decryptedText = "";
            int keyIndex = 0;

            foreach (char currentChar in input)
            {
                int keyChar = encryptionKey[keyIndex % encryptionKey.Length];

                char decryptedChar = (char)(currentChar - keyChar);
                decryptedText += decryptedChar;
                keyIndex++;
            }

            return decryptedText;
        }
    }
}