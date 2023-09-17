namespace SaveGame
{
    public interface ISaveable
    {
        public SaveData Save();
        public void Load(SaveData saveData);
    }
}
